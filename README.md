# 📟 Customer Management System with Azure Functions & Cosmos DB

This project is a customer management backend built with .NET 8, Minimal APIs, Azure Cosmos DB, and Azure Functions. It supports CRUD operations on customer data and notifies the responsible sales representative via email when a customer is added or updated.

---

## 🚀 Features

* 🌐 Minimal API with clean endpoint structure
* 💂 Cosmos DB for data persistence
* 📧 Azure Function that sends email notifications on customer create/update
* ✉️ Email delivery using SendGrid
* 🔍 Search customers by name or sales rep
* 🧪 Swagger UI for testing

---

## 📁 Project Structure

```bash
CustomerManagementApp/            # Main Web API project
│
├── Data/
│   ├── Entities/                 # Customer and SalesRep models
│   ├── Dtos/                     # Data transfer objects (CustomerCreateDto, etc.)
│   ├── Interface/                # Interfaces for DB and email services
│   ├── Repos/                    # CosmosDbService and EmailNotificationService
│   └── Extensions/              # Swagger config extensions
│
├── Endpoints/                    # Minimal API route definitions
├── Program.cs                    # API startup (clean & minimal)
├── appsettings.json              # Cosmos DB and other settings

CustomerNotificationFunction/     # Azure Function project
│
├── Services/
│   └── SendGridEmailService.cs   # Sends email using ISendGridClient
│
├── CustomerCreatedHandler.cs     # Cosmos DB trigger function
├── Program.cs                    # Function startup & DI
├── local.settings.json           # Function config with API keys
```

---

## ⚖️ Technologies Used

* .NET 8
* Azure Cosmos DB (SQL API)
* Azure Functions (Isolated Worker Model)
* SendGrid (Email service)
* MailKit / Mailtrap (optional for testing)
* Swagger / OpenAPI

---

## ⚙️ Prerequisites

* .NET SDK 8.0 or later
* Visual Studio 2022
* Azure Subscription (for Cosmos DB + Function deployment)
* SendGrid account (free plan works for testing)

---

## 💠 Setup Instructions

### ✅ Web API

1. Clone the repo.
2. Open `CustomerManagementApp` in Visual Studio.
3. Add your Cosmos DB credentials to `appsettings.json`:

```json
"CosmosDb": {
  "ConnectionString": "YOUR_CONNECTION_STRING",
  "DatabaseName": "CustomerDB",
  "ContainerName": "Customers"
}
```

4. Run the API project (`F5`)
5. Use Swagger at `http://localhost:<port>/swagger`

---

### ✅ Azure Function

1. Go to the `CustomerNotificationFunction` project
2. Open `local.settings.json` and fill in:

```json
"Values": {
  "CosmosDbConnection": "YOUR_COSMOS_DB_CONNECTION_STRING",
  "SendGridApiKey": "YOUR_SENDGRID_API_KEY",
  "SendGridFromEmail": "YOUR_VERIFIED_EMAIL"
}
```

3. Set the function project as Startup
4. Run (`F5`) — logs will show on new/updated documents

---

## 🧪 Example: Create a Customer (POST)

```json
{
  "name": "Hany Magdy",
  "title": "CTO",
  "phone": "123-456-7890",
  "email": "customer@example.com",
  "address": "123 Tech Lane, Stockholm",
  "responsibleSalesRep": {
    "name": "John Doe",
    "phone": "987-654-3210",
    "email": "salesrep@example.com"
  }
}
```

---

## 📨 How Email Notifications Work

* Cosmos DB trigger function runs on document change
* Function reads the new customer document
* Sends email to the `ResponsibleSalesRep.Email` using SendGrid
* Logs success or error in the console

---

## ✅ How to Test Email

* Use your verified email as both sender and recipient for guaranteed delivery
* For safe dev testing: switch to **Mailtrap**

---

## ☁️ Deployment (Optional)

1. Deploy `CustomerManagementApp` to Azure App Service or Container App
2. Deploy `CustomerNotificationFunction` to Azure Functions
3. Store secrets in Azure Configuration / Key Vault

---

## 📄 License

MIT License — free to use, share, and modify.

---

## 👨‍💼 Author

**Hany Magdy**
LinkedIn / GitHub / Email (if applicable)

```
```
