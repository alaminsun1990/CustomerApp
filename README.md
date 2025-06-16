# CustomerApp – Customer Management System

This solution includes:
- `CustomerApi` – ASP.NET Core Web API for managing customers
- `CustomerCli` – Console application that consumes the API
- `CustomerApi.Tests` – xUnit project for API integration testing

---

## ✅ Prerequisites

- Visual Studio 2022 or later (with **.NET 8 SDK** and **ASP.NET & web development** workload)
- Docker Desktop (optional, for containerization)
- Git (for cloning and pushing to GitHub)

---

## 📁 Project Structure

```
CustomerApp/
│
├── CustomerApi/           # Main ASP.NET Core Web API
│   ├── Controllers/
│   ├── Models/
│   ├── Data/
│   ├── Program.cs
│   └── Dockerfile
│
├── CustomerCli/           # Console app that calls the API
│   └── Program.cs
│
├── CustomerApi.Tests/     # xUnit integration tests
│
├── k8s/                   # Kubernetes deployment YAML
│   └── deployment.yaml
│
├── .github/workflows/     # GitHub Actions CI pipeline
│   └── ci.yml
│
└── CustomerApp.sln  # Visual Studio Solution
```

---

## 🚀 Running the Application in Visual Studio

### 🔹 Step 1: Open the Solution
- Open `CustomerApp.sln` in Visual Studio.

### 🔹 Step 2: Set Multiple Startup Projects
1. Right-click the **solution** → Select **Set Startup Projects...**
2. Choose `Multiple startup projects`
3. Set both:
   - `CustomerApi` → `Start`
   - `CustomerCli` → `Start`
4. Click **OK**

### 🔹 Step 3: Configure Web API URL
1. Right-click `CustomerApi` → **Properties**
2. Go to the **Debug** tab
3. Ensure **App URL** is set to:
   ```
   http://localhost:5000
   ```
4. Also verify in `launchSettings.json`:
   ```json
   "applicationUrl": "http://localhost:8080"
   ```

### 🔹 Step 4: Run the Solution
- Press **F5** or click **Start**
- API will start on `http://localhost:8080`
- Swagger opens automatically in browser
- CLI output appears in Console

---

## 🧪 Running Tests

1. Right-click `CustomerApi.Tests` → Run Tests
2. Or open Test Explorer → Run All Tests

---

## 🐳 Docker (Optional)

```bash
docker build -t customer-api ./CustomerApi
docker run -p 8080:80 customer-api
```

---

## ☸️ Kubernetes (Optional)

1. Install Minikube or Kind
2. Deploy with:
   ```bash
   kubectl apply -f k8s/deployment.yaml
   ```

---

## 🔄 GitHub Actions CI/CD

GitHub Actions is set up under `.github/workflows/ci.yml`.  
It builds, tests, and validates on every push to `main`.

---

## ✅ Technologies Used

- ASP.NET Core 8 Web API
- Entity Framework Core
- SQL Server
- xUnit Testing
- Serilog for Logging
- Swagger (OpenAPI)
- Docker
- Kubernetes (Minikube-compatible)
- GitHub Actions CI/CD

---

## 📬 Contact

If you need help running the solution or deploying it, feel free to raise an issue or send a message.
