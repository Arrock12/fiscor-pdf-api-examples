using System.Net.Http.Headers;
using System.Text.Json;

var apiKey = Environment.GetEnvironmentVariable("FISCOR_PDF_API_KEY")
             ?? throw new InvalidOperationException("Set FISCOR_PDF_API_KEY.");
var baseUrl = Environment.GetEnvironmentVariable("FISCOR_PDF_BASE_URL")
              ?? "https://pdf.fiscor.am/api/v1";

using var http = new HttpClient { BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/") };
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

var inputPath = args.Length > 0 ? args[0] : "input.pdf";
var outputPath = args.Length > 1 ? args[1] : "output.docx";

var jobId = await CreatePdfToWordJobAsync(http, inputPath);
Console.WriteLine($"Created job: {jobId}");

await WaitForCompletionAsync(http, jobId);
await DownloadResultAsync(http, jobId, outputPath);

Console.WriteLine($"Saved result: {outputPath}");

static async Task<string> CreatePdfToWordJobAsync(HttpClient http, string inputPath)
{
    await using var fileStream = File.OpenRead(inputPath);
    using var form = new MultipartFormDataContent();
    using var fileContent = new StreamContent(fileStream);
    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
    form.Add(fileContent, "file", Path.GetFileName(inputPath));

    using var response = await http.PostAsync("jobs/pdf-to-word", form);
    var body = await response.Content.ReadAsStringAsync();
    response.EnsureSuccessStatusCode();

    using var json = JsonDocument.Parse(body);
    return json.RootElement.GetProperty("jobId").GetString()
           ?? throw new InvalidOperationException("Response did not include jobId.");
}

static async Task WaitForCompletionAsync(HttpClient http, string jobId)
{
    for (var attempt = 1; attempt <= 60; attempt++)
    {
        using var response = await http.GetAsync($"jobs/{jobId}");
        var body = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        using var json = JsonDocument.Parse(body);
        var status = json.RootElement.GetProperty("status").GetString();

        if (string.Equals(status, "Completed", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        if (string.Equals(status, "Failed", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Job failed: {body}");
        }

        await Task.Delay(TimeSpan.FromSeconds(Math.Min(2 + attempt, 10)));
    }

    throw new TimeoutException($"Job {jobId} did not complete in time.");
}

static async Task DownloadResultAsync(HttpClient http, string jobId, string outputPath)
{
    using var response = await http.GetAsync($"jobs/{jobId}/download");
    response.EnsureSuccessStatusCode();

    await using var output = File.Create(outputPath);
    await response.Content.CopyToAsync(output);
}
