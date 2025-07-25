# Serverless SaleWatcher
 
[![Deploy to Azure](https://img.shields.io/badge/Deploy-Azure-blue?logo=windows)](https://portal.azure.com)
![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)
![License](https://img.shields.io/badge/License-MIT-green)
![Last Commit](https://img.shields.io/github/last-commit/Ciaran-Codes/ServerlessSaleWatcher)

Track product prices and get notified when they drop — built entirely with Azure serverless architecture and Blazor WebAssembly.

## Project Overview

This project demonstrates a modern, event-driven serverless application using:

- ✅ **Azure Functions** (.NET 8 Isolated)
- ✅ **Azure Table Storage**
- ✅ **Azure Queue Storage** (submission + processing queues)
- ✅ **Logic Apps** with **SendGrid** for notifications
- ✅ **GitHub Actions** for push-to-deploy CI/CD

---

## Current Status

| Feature                             | Status     |
|------------------------------------|------------|
| Product submission endpoint         | ✅ Working |
| Submission model + validation       | ✅ Working |
| Local testing via Postman           | ✅ Verified |
| Table Storage integration           | ✅ Working |
| Queue Storage (input/output)        | ✅ Working |
| Queue-triggered processing function | ✅ Working |
| Logic App for email notifications   | ✅ Working |
| CI/CD pipeline to Azure             | ✅ Working |
| Unsubscribe endpoint                | ✅ Working |

---

## Submit a Product (POST)
Enqueues product details for further processing

**Endpoint:**  
`POST /api/SubmitProduct`

**Body:**  
```json
{
  "productUrl": "https://example.com/product",
  "email": "user@example.com"
}
```

**Response:**  
`200 OK`

---

## Unsubscribe (GET)
Unsubscribe a user from price alerts for a product.

**Endpoint:**
`GET /api/unsubscribe?email={email}&url={url}`

**Response:**
Plaintext confirmation message.

---

## Workflow Overview

1. Submission stored in Table Storage.
2. Enqueued to Queue Storage.
3. Queue-triggered function handles processing.
4. Logic App sends notification if conditions are met.
5. Optional unsubscribe flow removes matching table entry.

---

## Tech Stack

- Azure Functions (.NET 8 Isolated)
- Azure Table & Queue Storage
- Logic Apps + SendGrid
- CI/CD with GitHub Actions

---

## Price Checking Logic

Each URL is routed via an IPriceChecker implementation.

- Amazon currently supported (mocked for testing).
- Easily extendable via the factory pattern for additional retailers.

---

## License

This project is licensed under the [MIT License](LICENSE.txt).
