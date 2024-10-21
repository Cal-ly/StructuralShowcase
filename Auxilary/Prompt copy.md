I would like you to guide me step-by-step through building a **full-stack B2B brewery ordering system** from scratch.
I would like to take it one step at a time, to ask questions or add modifications.
The system should include:

### **1. Database Design (Using MySQL Hosted on Simply.com)**

- Set up a **MySQL** database hosted on **Simply.com** using **Entity Framework Core** with a **code-first approach**.
- Design the following tables and relationships:
  - **Customers**: Store customer information like `Name` and `Email`. Each `Customer` should only have one `User`.
  - **Users**: Handle authentication with fields for `Email` and `PasswordHash`. Each user is linked to a customer through a foreign key `CustomerId`. Admins pre-register users, and customers log in to complete their own customer profile.
  - **Orders**: Store customer orders with fields like `OrderDate`, `Status`, `TotalAmount`. Each order is linked to a customer.
  - **OrderItems**: Store details about each item in the order (e.g., `BeverageId`, `Quantity`, `Price`), and link to beverages and orders.
  - **Beverages**: Store product information like `Brand`, `Description`, `Price`, and `Size`. The `Size` should be an **enum** representing different sizes (bottles or cans).

```csharp
// SizeEnum.cs
public enum SizeEnum
{
    SmallBottle = 1,    // 25 cl
    MediumBottle = 2,   // 50 cl
    LargeBottle = 3,    // 150 cL
    XLargeBottle = 4,   // 200 cl
    SmallCan = 5,       // 33 cl
    MediumCan = 6,      // 50 cl
}

// Beverage.cs
public class Beverage
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SizeEnum Size { get; set; }

    public void Validate()
    {
        // Validation logic for Id, Name, Description, Price, and Size
    }
}
```

- Define the relationships between these tables using **Fluent API** in **BeverageContext**.

### **2. Back-End API (ASP.NET Core + Entity Framework Core, Hosted on Azure)**

- Use **Visual Studio 2022 Enterprise Edition** to develop the API.
- Create an **ASP.NET Core API** that:
  - Manages **authentication** using **JWT** (JSON Web Tokens) for users. Create an `AuthController` to handle user registration, login, and JWT token generation.
  - Provides **CRUD operations** for customers, orders, and beverages via RESTful API endpoints.
  - Uses **AutoMapper** to map between **DTOs** and **entities** where appropriate. This simplifies the communication between the front-end and back-end.
  - Includes an **OrdersController** that allows customers to place orders, and admins to view and update order statuses.
  - Includes an **AnalyticsController** that provides business insights like:
    - **Top beverages**: Most popular products based on orders.
    - **Total sales**: Total sales across all customers.
    - **Top customers**: Highest-spending customers.
    - **Revenue Trends**: Monthly revenue reports.
    - **Sales by Product Size**: Track sales by bottle or can sizes.
  - **UsersController**: Allows admins to pre-register users, manage passwords, and delete users.

#### **AutoMapper Example**:

```csharp
// BeverageDTO.cs
public class BeverageDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public SizeEnum Size { get; set; }
}

// AutoMapper Profile
public class BeverageProfile : Profile
{
    public BeverageProfile()
    {
        CreateMap<Beverage, BeverageDTO>();
        CreateMap<BeverageDTO, Beverage>();
    }
}
```

- Implement **error handling** and **input validation** (e.g., invalid email, order quantity).
- Deploy the API on **Azure App Service** and link it to the MySQL database hosted on Simply.com.
- Use the Visual Studio 2022 "Manage User Secrets" to store secrets, so they won't be pushed to GitHub.

#### **Recommended Folder Structure (Back-End API)**:

```
BreweryAPI/
│
├── Controllers/            # Controllers for handling HTTP requests
│   ├── AuthController.cs
│   ├── CustomersController.cs
│   ├── OrdersController.cs
│   ├── BeveragesController.cs
│   ├── UsersController.cs
│   └── AnalyticsController.cs
│
├── Models/                 # Database models and entities
│   ├── Customer.cs
│   ├── User.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   └── Beverage.cs
│
├── Services/               # Services for handling business logic
│   ├── TokenService.cs
│   ├── AuthService.cs
│   └── AdminService.cs      # Service for handling admin functionality
│
├── Data/                   # Database context and migration files
│   └── BeverageContext.cs
│
├── DTOs/                   # Data Transfer Objects for API input/output
│   └── BeverageDTO.cs
│   └── RegisterDto.cs
│   └── LoginDto.cs
│   └── UserDTO.cs           # DTO for user management
│
├── Migrations/             # Automatically generated EF migrations
│
├── Profiles/               # AutoMapper profiles
│   └── BeverageProfile.cs
│   └── UserProfile.cs       # Profile for mapping users to DTOs
│
└── Program.cs              # Entry point and application configuration
```

### **3. Front-End (React, Hosted on Simply.com)**

- Build a **React** front-end using **Visual Studio Code** to interact with the back-end API:
  - **Login Page**: Allows users to log in and receive a JWT token, stored in `localStorage` for future API requests.
  - **Customer Dashboard**: Allows customers to:
    - Browse available beverages.
    - Place orders using an order form.
    - View order history with status updates.
  - **Admin Dashboard**: Allows admins to:
    - View all orders.
    - Update order statuses (e.g., mark an order as `Shipped`).
    - CRUD customers and users.
    - CRUD beverages.
    - Access analytics data like top customers, total sales, and revenue trends.
  - Use **Axios** for making API requests, and configure it to include the JWT token in the request headers for authorized requests.
  
- Implement routing between pages using **React Router**.
- Host the React app on **Simply.com** alongside the MySQL database.

#### **Recommended Folder Structure (Front-End React)**:

```
src/
│
├── components/    # Reusable React components
│   ├── Customer.js
│   ├── OrderForm.js
│   ├── Navbar.js
│
├── pages/         # Full-page components
│   ├── Home.js
│   ├── Login.js
│   ├── Orders.js
│   └── Analytics.js
│
├── services/      # API calls and JWT management
│   ├── api.js
│   └── auth.js
│
├── context/       # (Optional) React context for global state
│   └── AuthContext.js
│
├── App.js         # Main component for route configuration
└── index.js       # Entry point for the React application
```

### **4. Authentication and Authorization**

- Implement **JWT-based authentication** on both the front-end and back-end:
  - Store the JWT token on the front-end and attach it to the `Authorization` header for each API request.
  - Protect routes and components that require authentication, like the dashboard and analytics pages.

### **5. Deployment**

- Deploy the **back-end API** on **Azure App Service**, connecting it to the **MySQL database** hosted on **Simply.com**.
- Deploy the **front-end React app** on **Simply.com**, ensuring that it communicates properly with the back-end API hosted on Azure.
  
### **6. Security Considerations**

- Ensure all API routes that deal with customer data, orders, and analytics are protected by JWT authorization.
- Use **HTTPS** to secure data transmission and ensure that JWT tokens expire after a reasonable period of time.
- Secure sensitive data such as database credentials and JWT signing keys using environment variables or a key management system like **Azure Key Vault**.
