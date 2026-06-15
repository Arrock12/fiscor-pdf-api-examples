# Fiscor PDF API Examples

Ready-to-use examples for integrating the Fiscor PDF API into web apps, SaaS products, back-office systems, and automation workflows.

Use the API to convert, merge, split, compress, OCR, watermark, protect, unlock, and export PDF files without running document processing infrastructure yourself.

Base URL:

```txt
https://pdf.fiscor.am/api/v1
```

Useful links:

- API overview: https://pdf.fiscor.am/pdf-api
- API documentation: https://pdf.fiscor.am/pdf-api/docs
- Live examples page: https://pdf.fiscor.am/pdf-api/examples
- GitHub repository: https://github.com/Arrock12/fiscor-pdf-api-examples
- Operations discovery: https://pdf.fiscor.am/api/v1/operations

Suggested GitHub topics:

```txt
pdf-api
pdf-converter
pdf-to-word
word-to-pdf
pdf-ocr
pdf-compression
document-automation
saas-api
postman-collection
csharp
javascript
```

## Authentication

Use an API key in the `Authorization` header:

```txt
Authorization: Bearer YOUR_API_KEY
```

Do not commit real API keys. Use environment variables in local development and secrets in production.

## Basic workflow

1. Discover supported operations with `GET /operations`.
2. Upload one or more files to `POST /jobs/{operation}`.
3. Poll `GET /jobs/{jobId}` until the job is completed.
4. Download the result from `GET /jobs/{jobId}/download`.

## Quick start

```bash
export FISCOR_PDF_API_KEY="YOUR_API_KEY"
export FISCOR_PDF_BASE_URL="https://pdf.fiscor.am/api/v1"

curl "$FISCOR_PDF_BASE_URL/operations" \
  -H "Authorization: Bearer $FISCOR_PDF_API_KEY"
```

## Examples

- [curl examples](./curl.md)
- [C# HttpClient example](./csharp/FiscorPdfApiExample.cs)
- [JavaScript fetch example](./javascript/fiscor-pdf-api-example.js)
- [Postman collection](./postman/fiscor-pdf-api.postman_collection.json)
- [Postman environment](./postman/fiscor-pdf-api.postman_environment.json)

## Common operation slugs

```txt
pdf-to-word
word-to-pdf
pdf-to-excel
pdf-ocr
merge-pdf
split-pdf
compress-pdf
remove-pages
rotate-pdf
protect-pdf
unlock-pdf
add-watermark
sign-pdf
pdf-to-jpg
jpg-to-pdf
compress-jpg
base64-encode
base64-decode
```

Use `/operations` as the source of truth because available tools and metadata can change.

## Production notes

- Jobs are asynchronous. Always poll job status before downloading.
- File size, page count, monthly usage, and operation-specific limits depend on the account plan.
- Use retry with backoff for polling and transient network failures.
- Treat failed jobs as terminal unless the response suggests a client-side input issue that can be corrected.
- Store API keys securely and rotate them if exposed.
