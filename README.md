# 🛠 Workflow Engine – .NET Core 9 (CQRS + DDD)

A modular, scalable **Workflow Engine** developed in **C# (.NET Core 9)** for managing dynamic e-service transactions involving both **customers** and **employees**. It supports runtime task assignment to **Active Directory groups** and external users, built with clean architectural patterns for high maintainability and extensibility.

---

## ✨ Key Features

- **Runtime Workflow Management**: Create, configure, and trigger workflow instances dynamically.
- **Task Assignment System**:
  - Assign tasks to **Active Directory groups** dynamically at runtime.
  - Involve **customers** in workflows through authenticated API interactions.
- **CQRS Pattern**: 
  - Segregates read and write responsibilities for scalability and performance.
- **DDD Structure**:
  - Enforces separation of concerns via layered domain models and aggregates.
- **API-First Design**:
  - Exposes secure, versioned RESTful APIs for workflows and task interactions.
- **Caching Integration**:
  - Optimized for performance with configurable **in-memory or distributed caching**.

---

## 🧩 Architecture Overview

