# CRUD web-app in ASP\.NET  
**Software Systems Architectures and Design assignments**

## Overview  
This repository contains assignments completed for the *Software Systems Architectures and Design* course at Vilnius University during Autumn 2023 semester. The focus is on designing and integrating a CRUD (Create,Read,Update,Delete) web application using architectural patterns and REST APIs.

---

## Description of the assignments  

### LAB1A  
#### Requirements:  
- **Architectural Pattern**: Implement a website based on one of the following architectural patterns: MVC, MVU, or MVVM.  
- **Features**:  
  - CRUD (Create, Read, Update, Delete) functionality for a single business entity.  
  - The business entity must have at least **4 editable properties**.  
- **Validation**:  
  - Validation must be implemented for all editable properties in **create** and **update** scenarios.  
- **Dependency Injection (DI) and Inversion of Control (IoC)**:  
  - Demonstrate their usage within the application.  

---

### 2. LAB1B  
#### Requirements:  
- **Features**:  
  - CRUD functionality (Create, Read, Update, Delete) for a business entity via an API.  
  - The API must support **4 HTTP verbs** (GET, POST, PUT, DELETE).  
  - The business entity must have at least **4 editable properties**.  
- **Testing**:  
  - Unit tests must be written for all public API contracts, with **100% coverage**.  

---

### 3. LAB1C  
#### Requirements:  
- **Database Layer**:  
  - Implement a database layer using **ORM** or **plain SQL** with the **Repository Pattern**.  
- **Features**:  
  - CRUD functionality (Create, Read, Update, Delete) for a business entity.  
  - The business entity must have at least **4 editable properties**.  
- **Integration**:  
  - Integrate the LAB1A, LAB1B and database layer into a cohesive system.  
  - The website must use the API to manipulate data, and the API must use the database layer to persist data.  