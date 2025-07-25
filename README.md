# Serverless SaleWatcher

Track product prices and get notified when they drop — built entirely with Azure serverless architecture and Blazor WebAssembly.

## Project Overview

This project demonstrates a modern serverless application using:

- ✅ Azure Functions (.NET 8 Isolated)
- ✅ Azure Table Storage
- ✅ Azure Queue Storage (message enqueue + trigger function)
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
| Table Storage integration           | ✅ Working |
| Queue Storage integration           | ✅ Working |
| Queue Trigger Function              | ✅ Working |
| Logic App email flow                | ✅ Working |
| CI/CD Pipeline                      | ⏳ Next up |
| Blazor WASM frontend (SignalR)      | ⏳ Planned |

---

## Submit a Product (POST)
Enqueues product details for further processing

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

## Event-Driven Processing

Once a product is submitted, it is:
1. Stored in Azure Table Storage
2. Queued via Azure Queue Storage
3. Picked up by a Queue Trigger Function for processing (e.g., price check, notifications)

---

## Tech Stack

- **.NET 8 Azure Functions** – Isolated process model
- **Azure Storage (Table + Queue)** – Simple, cost-effective persistence
- **Azure Logic Apps** – Email alerts using SendGrid
- **Blazor WebAssembly + SignalR** – Real-time updates and dashboards
- **GitHub Actions** – Push-to-deploy CI/CD pipeline

---

## Price Checking Logic

Each product is routed through a store-specific implementation of `IPriceChecker`.
Currently supported: `Amazon` (mocked).

The factory pattern allows for scalable resolution based on the product URL.

---

## License

This project is licensed under the [MIT License](LICENSE).
