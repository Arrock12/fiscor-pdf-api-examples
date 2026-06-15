const baseUrl = "https://pdf.fiscor.am/api/v1";

export async function convertPdfToWord({ apiKey, file }) {
  const form = new FormData();
  form.append("file", file, file.name);

  const createResponse = await fetch(`${baseUrl}/jobs/pdf-to-word`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${apiKey}`,
    },
    body: form,
  });

  const createBody = await createResponse.json();
  if (!createResponse.ok) {
    throw new Error(createBody.error ?? "Could not create conversion job.");
  }

  const jobId = createBody.jobId;
  await waitForCompletion(apiKey, jobId);

  const downloadResponse = await fetch(`${baseUrl}/jobs/${jobId}/download`, {
    headers: {
      Authorization: `Bearer ${apiKey}`,
    },
  });

  if (!downloadResponse.ok) {
    throw new Error("Could not download conversion result.");
  }

  return {
    jobId,
    blob: await downloadResponse.blob(),
  };
}

async function waitForCompletion(apiKey, jobId) {
  for (let attempt = 1; attempt <= 60; attempt++) {
    const response = await fetch(`${baseUrl}/jobs/${jobId}`, {
      headers: {
        Authorization: `Bearer ${apiKey}`,
      },
    });

    const body = await response.json();
    if (!response.ok) {
      throw new Error(body.error ?? "Could not read job status.");
    }

    if (body.status === "Completed") {
      return;
    }

    if (body.status === "Failed") {
      throw new Error(body.failureReason ?? "Conversion job failed.");
    }

    await delay(Math.min(2 + attempt, 10) * 1000);
  }

  throw new Error(`Job ${jobId} did not complete in time.`);
}

function delay(ms) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

// Browser usage:
//
// const { blob } = await convertPdfToWord({
//   apiKey: "YOUR_API_KEY",
//   file: document.querySelector("input[type=file]").files[0],
// });
//
// const link = document.createElement("a");
// link.href = URL.createObjectURL(blob);
// link.download = "output.docx";
// link.click();
