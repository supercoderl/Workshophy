# WorkshopHub - Installation & Setup Guide

## 📋 Table of Contents
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Services Setup](#services-setup)
- [Database Setup](#database-setup)
- [Running the Application](#running-the-application)
- [Troubleshooting](#troubleshooting)

## 🔧 Prerequisites

Before installing WorkshopHub, ensure you have the following installed on your system:

- **.NET 8.0 SDK** or later
- **SQL Server** (Express, Developer, or full version)
- **Redis Server** 
- **RabbitMQ Server**
- **Git** (for cloning the repository)

## 📦 Installation

### 1. Clone the Repository
```bash
git clone https://github.com/your-org/workshophub.git
cd workshophub
```

### 2. Restore Dependencies
```bash
dotnet restore
```

## ⚙️ Configuration

### 1. Database Connection String
Update the `DefaultConnection` in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER_NAME;Initial Catalog=WorkshopHub;Persist Security Info=True;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=True;"
  }
}
```

**Replace:**
- `YOUR_SERVER_NAME` with your SQL Server instance name (e.g., `localhost`, `.\SQLEXPRESS`)
- `YOUR_USERNAME` with your SQL Server username
- `YOUR_PASSWORD` with your SQL Server password

### 2. Complete appsettings.json Configuration

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=WorkshopHub;Persist Security Info=True;User ID=sa;Password=YourPassword123!;Encrypt=True;Trust Server Certificate=True;MultipleActiveResultSets=True;"
  },
  "Auth": {
    "Issuer": "WorkshopHub.com",
    "Audience": "WorkshopHub.com",
    "Secret": "sD3v061gf8BxXgmxcHssasjdlkasjd87439284)@#(*"
  },
  "RedisHostName": "localhost:6379",
  "RabbitMQ": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest",
    "Enabled": "True"
  },
  "Aikido": {
    "AikidoToken": "your-aikido-security-token-here"
  },
  "PayOS": {
    "BaseUrl": "https://workshophub.com/payment",
    "ApiKey": "your-payos-api-key",
    "ClientID": "your-payos-client-id",
    "ChecksumKey": "your-payos-checksum-key"
  },
  "Smtp": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "EnableSsl": true,
    "FromEmail": "your-email@gmail.com",
    "FromName": "WorkshopHub Support"
  }
}
```

## 🔨 Services Setup

### Redis Installation & Setup

#### Windows (using Chocolatey)
```powershell
# Install Chocolatey if not already installed
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))

# Install Redis
choco install redis-64
```

#### Windows (using MSI)
1. Download Redis for Windows from [Microsoft's GitHub repository](https://github.com/microsoftarchive/redis/releases)
2. Install the MSI package
3. Redis will start automatically as a Windows service

#### macOS
```bash
# Using Homebrew
brew install redis
brew services start redis
```

#### Linux (Ubuntu/Debian)
```bash
sudo apt update
sudo apt install redis-server
sudo systemctl start redis-server
sudo systemctl enable redis-server
```

### RabbitMQ Installation & Setup

#### Windows
1. Download and install Erlang from [Erlang Solutions](https://www.erlang.org/downloads)
2. Download RabbitMQ from [RabbitMQ Downloads](https://www.rabbitmq.com/download.html)
3. Install RabbitMQ
4. Enable the management console:
```cmd
rabbitmq-plugins enable rabbitmq_management
```

#### macOS
```bash
# Using Homebrew
brew install rabbitmq
brew services start rabbitmq

# Enable management plugin
rabbitmq-plugins enable rabbitmq_management
```

#### Linux (Ubuntu/Debian)
```bash
# Add RabbitMQ signing key
curl -fsSL https://github.com/rabbitmq/signing-keys/releases/download/2.0/rabbitmq-release-signing-key.asc | sudo apt-key add -

# Add RabbitMQ repository
echo 'deb [arch=amd64] https://packagecloud.io/rabbitmq/rabbitmq-server/ubuntu/ focal main' | sudo tee /etc/apt/sources.list.d/rabbitmq.list

# Install RabbitMQ
sudo apt update
sudo apt install rabbitmq-server

# Start and enable RabbitMQ
sudo systemctl start rabbitmq-server
sudo systemctl enable rabbitmq-server

# Enable management plugin
sudo rabbitmq-plugins enable rabbitmq_management
```

### Service Verification

#### Check Redis
```bash
redis-cli ping
# Should return: PONG
```

#### Check RabbitMQ
- Access the management console at `http://localhost:15672`
- Default credentials: `guest` / `guest`

## 🗄️ Database Setup

### 1. Create Database
Connect to SQL Server and run:
```sql
CREATE DATABASE WorkshopHub;
```

### 2. Install Entity Framework Tools
If you don't have Entity Framework tools installed:
```bash
dotnet tool install --global dotnet-ef
```

### 3. Create and Run Migrations for All DbContexts

The application uses three separate DbContexts that need to be configured:

#### 3.1. ApplicationDbContext (Main Application Tables)
```bash
# Add migration for main application context
dotnet ef migrations add InitialCreate --context ApplicationDbContext --output-dir Migrations/ApplicationDb

# Update database
dotnet ef database update --context ApplicationDbContext
```

#### 3.2. DomainNotificationStoreDbContext (Domain Notifications)
```bash
# Add migration for domain notification context
dotnet ef migrations add InitialCreate --context DomainNotificationStoreDbContext --output-dir Migrations/DomainNotificationDb

# Update database
dotnet ef database update --context DomainNotificationStoreDbContext
```

#### 3.3. EventStoreDbContext (Event Store)
```bash
# Add migration for event store context
dotnet ef migrations add InitialCreate --context EventStoreDbContext --output-dir Migrations/EventStoreDb

# Update database
dotnet ef database update --context EventStoreDbContext
```

### 4. Verify All Migrations
After running all three migrations, verify the database contains all necessary tables:

```sql
-- Connect to WorkshopHub database and check tables
USE WorkshopHub;
GO

-- List all tables to verify migration success
SELECT TABLE_SCHEMA, TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_SCHEMA, TABLE_NAME;
```

### 5. Alternative: Run All Migrations at Once
You can create a batch script to run all migrations:

**Windows (run-migrations.bat):**
```batch
@echo off
echo Creating migrations for all DbContexts...

echo.
echo Adding ApplicationDbContext migration...
dotnet ef migrations add InitialCreate --context ApplicationDbContext --output-dir Migrations/ApplicationDb

echo.
echo Adding DomainNotificationStoreDbContext migration...
dotnet ef migrations add InitialCreate --context DomainNotificationStoreDbContext --output-dir Migrations/DomainNotificationDb

echo.
echo Adding EventStoreDbContext migration...
dotnet ef migrations add InitialCreate --context EventStoreDbContext --output-dir Migrations/EventStoreDb

echo.
echo Updating database with all migrations...
dotnet ef database update --context ApplicationDbContext
dotnet ef database update --context DomainNotificationStoreDbContext
dotnet ef database update --context EventStoreDbContext

echo.
echo All migrations completed successfully!
pause
```

**Linux/macOS (run-migrations.sh):**
```bash
#!/bin/bash
echo "Creating migrations for all DbContexts..."

echo ""
echo "Adding ApplicationDbContext migration..."
dotnet ef migrations add InitialCreate --context ApplicationDbContext --output-dir Migrations/ApplicationDb

echo ""
echo "Adding DomainNotificationStoreDbContext migration..."
dotnet ef migrations add InitialCreate --context DomainNotificationStoreDbContext --output-dir Migrations/DomainNotificationDb

echo ""
echo "Adding EventStoreDbContext migration..."
dotnet ef migrations add InitialCreate --context EventStoreDbContext --output-dir Migrations/EventStoreDb

echo ""
echo "Updating database with all migrations..."
dotnet ef database update --context ApplicationDbContext
dotnet ef database update --context DomainNotificationStoreDbContext
dotnet ef database update --context EventStoreDbContext

echo ""
echo "All migrations completed successfully!"
```

Make the script executable (Linux/macOS):
```bash
chmod +x run-migrations.sh
./run-migrations.sh
```

## 📧 Email Configuration (SMTP)

For Gmail SMTP, you'll need to:

1. **Enable 2-Factor Authentication** on your Google account
2. **Generate an App Password**:
   - Go to Google Account settings
   - Security → 2-Step Verification → App passwords
   - Generate a password for "Mail"
3. **Use the App Password** in the `Smtp.Password` field

## 💳 Payment Integration (PayOS)

To configure PayOS payment integration:

1. Register at [PayOS Developer Portal](https://payos.vn)
2. Create a new application
3. Copy the API credentials to your configuration:
   - `ApiKey`: Your PayOS API key
   - `ClientID`: Your PayOS client ID  
   - `ChecksumKey`: Your PayOS checksum key

## 🚀 Running the Application

### Development Mode
```bash
dotnet run
```

### Production Mode
```bash
dotnet publish -c Release
cd bin/Release/net8.0/publish
dotnet WorkshopHub.dll
```

### Using Docker (Optional)
```bash
# Build the image
docker build -t workshophub .

# Run with docker-compose (if available)
docker-compose up -d
```

## 🌐 Accessing the Application

Once running, the application will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`

## 🔍 Troubleshooting

### Common Issues

#### Database Connection Issues
- Verify SQL Server is running
- Check connection string parameters
- Ensure the database exists
- Verify user permissions
- **Ensure all three DbContext migrations are applied**

#### Migration Issues
If you encounter migration errors:

```bash
# Check current migration status for each context
dotnet ef migrations list --context ApplicationDbContext
dotnet ef migrations list --context DomainNotificationStoreDbContext  
dotnet ef migrations list --context EventStoreDbContext

# Remove a migration if needed (run for each context)
dotnet ef migrations remove --context ApplicationDbContext

# Force update if database is out of sync
dotnet ef database update --context ApplicationDbContext --force
dotnet ef database update --context DomainNotificationStoreDbContext --force
dotnet ef database update --context EventStoreDbContext --force
```

#### Redis Connection Issues
```bash
# Check if Redis is running
redis-cli ping

# Start Redis service (Windows)
net start Redis

# Start Redis service (Linux/macOS)
sudo systemctl start redis-server
# or
brew services start redis
```

#### RabbitMQ Connection Issues
```bash
# Check RabbitMQ status
sudo systemctl status rabbitmq-server

# Restart RabbitMQ
sudo systemctl restart rabbitmq-server
```

#### Email Issues
- Verify SMTP credentials
- Ensure "Less secure app access" is enabled for Gmail (or use App Password)
- Check firewall settings for SMTP ports

### Logs
Check application logs in:
- Console output during development
- Log files in `/logs` directory (if configured)
- Windows Event Viewer (for Windows services)

## 📝 Environment Variables (Alternative Configuration)

Instead of modifying `appsettings.json`, you can use environment variables:

```bash
# Database
export ConnectionStrings__DefaultConnection="your-connection-string"

# Redis
export RedisHostName="localhost:6379"

# RabbitMQ
export RabbitMQ__Host="localhost"
export RabbitMQ__Username="guest"
export RabbitMQ__Password="guest"

# SMTP
export Smtp__Username="your-email@gmail.com"
export Smtp__Password="your-app-password"
```

## 🆘 Support

If you encounter issues:

1. Check the [Troubleshooting](#troubleshooting) section
2. Review application logs
3. Verify all services are running
4. Check firewall and network settings
5. Create an issue in the project repository

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.