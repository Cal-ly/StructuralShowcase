I would like you to guide me step-by-step through building a **full-stack B2B brewery ordering system** from scratch.
The overall project should be called **Brewery**. I work mainly Visual Studio 2022 for csharp in Developer Powershell 
I would like to take it one step at a time, to ask questions or add modifications.

### **Step 1. Database Design (Using MySQL database)**

- Set up a local **MySQL** database using **Entity Framework Core** with a **code-first approach**.
- All models should contain
 - A **string** `Id`, which should come from a generated GUID.
 - A `Validate` method for the relevant properties.
 - The methods `ToString()`, `Equals()` and `GetHashCode()`.
- Design the following tables and relationships:
  - **Customers**: Store customer information like `Name` and `Email` and `Orders`. Each `Customer` should only have one `User`.
  - **Users**: Handle authentication with fields for `Email` and `PasswordHash`. Each user is linked to a customer through a foreign key `CustomerId`. The `Role` should be an **enum**, to define role. The  Admins pre-register users, and customers log in to complete their own customer profile.
  - **Orders**: Store customer orders with fields like `OrderDate`, `Status`, `TotalAmount`. Each order is linked to a customer. The `Status` should be an **enum**, as defined later.
  - **OrderItems**: Store details about each item in the order (e.g., `BeverageId`, `Quantity`, `Price`), and link to beverages and orders.
  - **Beverages**: Store product information like `Name`, `Description`, `Price`, and `Size`. The `Size` should be an **enum** representing different sizes (bottles or cans).


```csharp
// SizeEnum.cs
public enum SizeEnum
{
    None = 0,           // Fallback value
    SmallBottle = 1,    // 25 cl
    MediumBottle = 2,   // 50 cl
    LargeBottle = 3,    // 150 cL
    XLargeBottle = 4,   // 200 cl
    SmallCan = 5,       // 33 cl
    MediumCan = 6,      // 50 cl
}

//StatusEnum.cs
public enum StatusEnum
{
    None = 0,
    Pending = 1,
    Recived = 2,
    Packing = 3,
    Shipped = 4,
    Completed = 5,
    Cancelled = 6,
}

//UserRoleEnum
public enum UserRoleEnum
{
    None = 0,
    Admin = 1,
    Manager = 2,
    Customer = 3
}

// Beverage.cs
public class Beverage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SizeEnum Size { get; set; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name)) throw new ArgumentException("Name is required.");
        if (Price <= 0) throw new ArgumentException("Price must be greater than zero.");
    }

    public override string ToString() => $"{Name} - {Size} - {Price}";
    public override bool Equals(object obj) => obj is Beverage other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}
```

- Define the relationships between these tables using **Fluent API** in **BreweryContext**.

### **Step 2. Back-End API (ASP.NET Core + Entity Framework Core, Hosted on Azure)**

- Use **Visual Studio 2022 Enterprise Edition** to develop the API.
- Create an **ASP.NET Core API** that:
  - Manages **authentication** using **JWT** (JSON Web Tokens) for users. Create an `AuthController` to handle user registration, login, and JWT token generation. Do not set Authorize restrictions yet.
  - Provides **CRUD operations** for customers, orders, and beverages via RESTful API endpoints.
  - Includes an **OrdersController** that allows customers to place orders, and admins to view and update order statuses.
  - Includes an **AnalyticsController** that provides business insights like:
    - **Top beverages**: Most popular products based on orders.
    - **Total sales**: Total sales across all customers.
    - **Top customers**: Highest-spending customers.
    - **Revenue Trends**: Monthly revenue reports.
    - **Sales by Product Size**: Track sales by bottle or can sizes.
  - **UsersController**: Allows admins to pre-register users, manage passwords, and delete users.


- Implement **error handling** and **input validation** (e.g., invalid email, order quantity).
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
│   ├── Beverage.cs
│   └── Enums/              # Enums for the models
│       ├── SizeEnum.cs
│       ├── StatusEnum.cs
│       └── UserRoleEnum.cs
│
├── Services/               # Services for handling business logic
│   ├── TokenService.cs
│   ├── AuthService.cs
│   └── AdminService.cs      # Service for handling admin functionality
│
├── Data/                   # Database context and migration files
│   └── BeverageContext.cs
│
├── Migrations/             # Automatically generated EF migrations
│
└── Program.cs              # Entry point and application configuration
```

### **3. Front-End (React, Hosted on Simply.com)**

- Build a **React** (check for latest version) front-end using **Visual Studio Code** to interact with the back-end API:
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
  - Use **Axios** (check for latest version) for making API requests, and configure it to include the JWT token in the request headers for authorized requests.
  - Use **Bootstrap** (check for latest version) to help style and design the app.
  
- Suggest a folder structure to the front-end app.
- Implement routing between pages using **React Router**.
- Host the React app on **Simply.com** alongside the MySQL database.

#### **Recommended Folder Structure (Front-End React)**:

```
src/
│
├── assets/                  # Static files, images, etc.
│
├── components/              # Shared components across pages
│   ├── Navbar.js            # Top navigation bar
│   ├── ProtectedRoute.js    # Route protection for JWT (initially unused)
│   └── LoadingSpinner.js    # Loader for async data fetching
│
├── pages/                   # Page components
│   ├── Login.js             # Login page
│   ├── CustomerDashboard.js # Customer dashboard
│   ├── AdminDashboard.js    # Admin dashboard
│   └── Beverages.js         # View for browsing beverages
│
├── services/                # API call abstractions
│   ├── api.js               # Axios instance with base URL
│   ├── authService.js       # Login, logout, token storage
│   ├── customerService.js   # Customer-related CRUD operations
│   ├── orderService.js      # Order-related CRUD operations
│   ├── beverageService.js   # Beverage-related CRUD operations
│   └── analyticsService.js  # Admin analytics API calls
│
├── App.js                   # Main app component and router setup
├── index.js                 # App entry point
├── App.css                  # Global styles
└── setupProxy.js            # Proxy for local dev to handle CORS with API
```

### **4. Authentication and Authorization**

- Implement **JWT-based authentication** on both the front-end and back-end:
  - Store the JWT token on the front-end and attach it to the `Authorization` header for each API request.
  - Protect routes and components that require authentication, like the dashboard and analytics pages.

### **5. Deployment**

- Deploy the **back-end API** on **Azure App Service**, connecting it to the **MySQL database** hosted on **Simply.com**.
- Deploy the **front-end React app** on **Simply.com**, ensuring that it communicates properly with the back-end API hosted on Azure.
  
### **6. Security Considerations**

- Ensure all API routes that deal with customer data, orders, and analytics are protected by JWT authorization, disregard this during development.
- Start by using **HTTP**. The use of **HTTPS** to secure data transmission and ensure that JWT tokens expire after a reasonable period of time will be added later.
- Secure sensitive data such as database credentials and JWT signing keys using environment variables or a key management system like **Azure Key Vault**.
