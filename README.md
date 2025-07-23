# Serverless SaleWatcher

Track product prices and get notified when they drop — built entirely with Azure serverless architecture and Blazor WebAssembly.

## Project Overview

This project demonstrates a modern serverless application using:

- ✅ Azure Functions (.NET 8 Isolated)
- ✅ Azure Table Storage
- ✅ Azure Queue Storage
- ✅ Logic Apps (SendGrid integration for notifications)
- ✅ Blazor WebAssembly (SignalR real-time UI)
- ✅ GitHub Actions for CI/CD

---

## Current Status

| Feature                     | Status     |
|----------------------------|------------|
| HTTP Trigger for product submission | ✅ Working |
| Data model for submissions          | ✅ Working |
| Local testing via Postman           | ✅ Verified |
| Table Storage integration           | ⏳ Next Step |
| Queue Storage integration           | ⏳ Coming Soon |
| Logic App email flow                | ⏳ Planned |
| Blazor WASM frontend (SignalR)      | ⏳ Planned |

---

## Submit a Product (POST)

**URL:**  
`POST /api/SubmitProduct`

**Body:**  
```json
{
  "productUrl": "https://example.com/product",
  "email": "user@example.com"
}
```

**Response:**  
A simple 200 OK confirmation.

---

## Tech Stack

- **.NET 8 Azure Functions** – Isolated process model
- **Azure Storage (Table + Queue)** – Simple, cost-effective persistence
- **Azure Logic Apps** – Email alerts using SendGrid
- **Blazor WebAssembly + SignalR** – Real-time updates and dashboards
- **GitHub Actions** – Push-to-deploy CI/CD pipeline

---

## License

This project is licensed under the [MIT License](LICENSE).
