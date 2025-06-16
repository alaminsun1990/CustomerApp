# CustomerApp – Customer Management System

This solution includes:
- `CustomerApi` – ASP.NET Core Web API for managing customers
- `CustomerCli` – Console application that consumes the API
- `CustomerApi.Tests` – xUnit project for API integration testing

---

## ✅ Prerequisites

- Visual Studio 2022+ with .NET 8 SDK
- Docker Desktop (optional)
- Git + GitHub (for CI/CD)
- Kubernetes CLI (optional: `kubectl`, Minikube)

---

## 📁 Project Structure

```
CustomerApp/
│
├── CustomerApi/           # Main ASP.NET Core Web API
├── CustomerCli/           # Console app that calls the API
├── CustomerApi.Tests/     # xUnit integration tests
├── k8s/                   # Kubernetes manifests
├── .github/workflows/     # GitHub Actions CI/CD
└── CustomerApp.sln  # Visual Studio Solution
```

---

## 🚀 Running in Visual Studio

1. Open `CustomerApp.sln`
2. Right-click solution → **Set Startup Projects...**
3. Choose **Multiple Startup Projects**:
   - `CustomerApi` → Start
   - `CustomerCli` → Start
4. Press **F5** or click **Start**

---

## 🧪 Running Tests

- In Visual Studio: Open **Test Explorer** → Run All Tests
- Or from CLI:
```bash
dotnet test CustomerApi.Tests
```

---

## 🐳 Docker Commands

```bash
docker build -t customer-api ./CustomerApi
docker run -p 8080:80 customer-api
```

---

## ☸️ Kubernetes

```bash
kubectl apply -f k8s/deployment.yaml
kubectl apply -f k8s/service.yaml
```

---

## 🔁 CLI App Usage (`CustomerCli`)

Interactive menu:

```
1. View all customers
2. Add customer
3. Delete customer
4. Update customer
5. Exit
```

Launch CLI manually:
```bash
dotnet run --project CustomerCli
```

---

## 🔄 API Usage Examples (curl)

### ▶️ Create a Customer
```bash
curl -X POST http://localhost:8080/api/customers \
-H "Content-Type: application/json" \
-d '{ "firstName": "John", "lastName": "Doe", "email": "john@example.com", "phoneNumber": "1234567890" }'
```

### 📋 Get All Customers
```bash
curl http://localhost:8080/api/customers
```

### 🔍 Get One Customer
```bash
curl http://localhost:8080/api/customers/{id}
```

### ✏️ Update Customer
```bash
curl -X PUT http://localhost:8080/api/customers/{id} \
-H "Content-Type: application/json" \
-d '{ "id": "{id}", "firstName": "Jane", "lastName": "Smith", "email": "jane@example.com", "phoneNumber": "9999999999" }'
```

### ❌ Delete Customer
```bash
curl -X DELETE http://localhost:8080/api/customers/{id}
```

---

## ✅ Technologies Used

- ASP.NET Core 8 Web API
- Entity Framework Core
- xUnit Testing
- Serilog Logging (JSON)
- Swagger (OpenAPI)
- Docker & Kubernetes
- GitHub Actions CI/CD
- Console Client via HttpClient

---

## 📬 Contact

For questions or contributions, please open an issue or PR.
