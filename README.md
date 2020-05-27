# ASP-Template
ASP NET Core 3.1 template with basic functionality provided such as logging,storage and configuration provider

## Purpose:
I have created this template to be able to reuse in upcoming projects.

## Integrated functionality:
  - **Configuration** : Configuration provided by json file
  - **Logging** : Done via `Serilog` library , also a default logging template is provided
  - **Storage**:
     - SQL (SQL Server)
         - Generic Repository functionality for BASIC CRUD Access 
         - Database Context 
         - Sample model
     - NoSQL (Redis)
           - Basic CRUD Operations based on initial model
  - **Extensions**:
     - Encoding
        
