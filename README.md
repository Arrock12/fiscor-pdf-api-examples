# Fiscor PDF API Examples

Official examples for integrating the Fiscor PDF Tools API into web apps, SaaS products, back-office systems, and automation workflows.

Use the API to convert PDF to Word, convert Word to PDF, convert PDF to JPG, convert JPG to PDF, merge PDF files, split PDF files, compress PDF files, OCR documents, watermark PDFs, protect PDFs, unlock PDFs, and export document data without running document processing infrastructure yourself.

Base URL:

```txt
https://pdf.fiscor.am/api/v1
```

Useful links:

- Official website: https://pdf.fiscor.am/
- API overview: https://pdf.fiscor.am/pdf-api
- API documentation: https://pdf.fiscor.am/pdf-api/docs
- Live examples page: https://pdf.fiscor.am/pdf-api/examples
- GitHub repository: https://github.com/Arrock12/fiscor-pdf-api-examples
- OpenAPI specification: https://pdf.fiscor.am/openapi.json
- Downloadable OpenAPI specification: https://pdf.fiscor.am/downloads/fiscor-pdf-api.openapi.json
- Postman collection: https://pdf.fiscor.am/downloads/fiscor-pdf-api.postman_collection.json
- Operations discovery: https://pdf.fiscor.am/api/v1/operations
- AI overview: https://pdf.fiscor.am/ai-overview
- LLM summary: https://pdf.fiscor.am/llms.txt
- Full LLM reference: https://pdf.fiscor.am/llms-full.txt
- Sitemap: https://pdf.fiscor.am/sitemap.xml

Official tool pages:

- PDF to Word: https://pdf.fiscor.am/pdf-to-word
- Word to PDF: https://pdf.fiscor.am/word-to-pdf
- PDF to JPG: https://pdf.fiscor.am/pdf-to-jpg
- JPG to PDF: https://pdf.fiscor.am/jpg-to-pdf
- Compress PDF: https://pdf.fiscor.am/compress-pdf

Search and AI discovery keywords:

```txt
Fiscor PDF API
Fiscor PDF Tools API
PDF API
PDF conversion API
PDF to Word API
Word to PDF API
PDF to JPG API
JPG to PDF API
Compress PDF API
Merge PDF API
Split PDF API
PDF OCR API
document automation API
online PDF tools API
OpenAPI PDF converter
Postman PDF API collection
```

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
- [OpenAPI specification](./openapi.json)

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
