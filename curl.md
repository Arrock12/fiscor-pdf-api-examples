# curl Examples

Set your API key once:

```bash
export FISCOR_PDF_API_KEY="YOUR_API_KEY"
export FISCOR_PDF_BASE_URL="https://pdf.fiscor.am/api/v1"
```

## Discover operations

```bash
curl "$FISCOR_PDF_BASE_URL/operations" \
  -H "Authorization: Bearer $FISCOR_PDF_API_KEY"
```

## Convert PDF to Word

Create a job:

```bash
curl -X POST "$FISCOR_PDF_BASE_URL/jobs/pdf-to-word" \
  -H "Authorization: Bearer $FISCOR_PDF_API_KEY" \
  -F "file=@./input.pdf"
```

Poll status:

```bash
curl "$FISCOR_PDF_BASE_URL/jobs/JOB_ID_FROM_CREATE_RESPONSE" \
  -H "Authorization: Bearer $FISCOR_PDF_API_KEY"
```

Download result:

```bash
curl -L "$FISCOR_PDF_BASE_URL/jobs/JOB_ID_FROM_CREATE_RESPONSE/download" \
  -H "Authorization: Bearer $FISCOR_PDF_API_KEY" \
  -o output.docx
```

## Merge PDFs

```bash
curl -X POST "$FISCOR_PDF_BASE_URL/jobs/merge-pdf" \
  -H "Authorization: Bearer $FISCOR_PDF_API_KEY" \
  -F "files=@./first.pdf" \
  -F "files=@./second.pdf"
```

## Compress PDF

```bash
curl -X POST "$FISCOR_PDF_BASE_URL/jobs/compress-pdf" \
  -H "Authorization: Bearer $FISCOR_PDF_API_KEY" \
  -F "file=@./input.pdf"
```

