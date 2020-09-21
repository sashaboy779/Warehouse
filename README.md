# 🚚 Warehouse
The Warehouse is a simple console application that was made for my coursework. It allows to manage the goods, categories and suppliers. Also, it has a searching feature.  

## ⚙ Prerequisites
Before you begin, ensure you have met the following requirements:
* You have a Windows machine
* You have installed [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Tech Stack
.NET Framework, Microsoft SQL Server

## 🗍 Architecture 
The application is separated into 3 different layers (**three-tier architecture**):
* **DAL** (data access layer) - provides simplified access to data by implementing a repository pattern
* **BLL** (business logic layer) - contains the service logic and coordinates data between the **PL** and **DAL**
* **PL** (presentation layer) - displays and receives data to and from the user

Also, there are additional projects:
* **Entities** - contains classes that are used in the application scope
* **BLL/DAL.Test** - this two was created for Unit Testing. Each test method follows the **Arrange-Act-Assert** pattern. As a testing libraries xUnit and Moq are selected.

