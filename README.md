# PingIdentityApp

A demo ASP.NET Core 8 web application showcasing **Ping Identity OpenID Connect (OIDC) integration**, access control, and role-based UI. This project is built to demonstrate authentication, authorization, and user experience with modern IAM principles.

---

## Features

* **OIDC Authentication**: Login using Ping Identity with Authorization Code Flow.
* **Cookie Authentication**: Manages local sessions securely.
* **Logout Support**: Full OIDC logout with redirect.
* **Role/Group Claims**: Show user roles and manage access visually.
* **Employee-Only Page**: Example page restricted to users in a specific group.
* **Bootstrap 5 UI**: Modern, responsive design with red highlights (PingOne style).
* **Request Access UI**: Demo group access table with "Request" buttons.

---

## Demo Pages

| Page                  | Description                               |
| --------------------- | ----------------------------------------- |
| `/`                   | Home page                                 |
| `/Home/Access`        | Access page: shows groups and permissions |
| `/Home/EmployeesOnly` | Employees-only page; restricted access    |
| `/Account/Login`      | Triggers Ping Identity login              |
| `/Account/Logout`     | Logs out user from app and Ping Identity  |

---

## Setup

### 1. Clone the repo

```bash
git clone https://github.com/your-org/PingIdentityApp.git
cd PingIdentityApp
```

### 2. Configure `appsettings.json`

```json
"Authentication": {
  "DefaultScheme": "OpenIdConnect",
  "Schemes": {
    "OpenIdConnect": {
      "SignInScheme": "Cookies",
      "Authority": "https://auth.pingone.com/YOUR-TENANT-ID/as",
      "ClientId": "YOUR-CLIENT-ID",
      "ClientSecret": "YOUR-CLIENT-SECRET",
      "ClaimsIssuer": "https://auth.pingone.com/YOUR-TENANT-ID/as",
      "ResponseType": "code",
      "CallbackPath": "/signin-oidc",
      "SaveTokens": true,
      "Scope": [
        "openid",
        "profile",
        "email",
        "offline_access"
      ]
    }
  }
}
```

> Replace placeholders with your Ping Identity credentials.

* `offline_access` scope is required to enable refresh tokens.
* Make sure `SignedOutCallbackPath` or `SignedOutRedirectUri` matches your PingOne app Post Logout Redirect URI.

---

### 3. Run the app

```bash
dotnet run
```

Navigate to: `https://localhost:7255/`

* Click **Login** to sign in with Ping Identity.
* After login, you can access pages based on roles/groups.
* Click **Logout** to clear session and redirect to home.

---

## Project Structure

```
PingIdentityApp/
│
├─ Controllers/
│   ├─ HomeController.cs         # Home, Access, EmployeesOnly pages
│   └─ AccountController.cs      # Login/Logout actions
│
├─ Views/
│   ├─ Home/
│   │   ├─ Index.cshtml
│   │   ├─ Access.cshtml
│   │   └─ EmployeesOnly.cshtml
│   └─ Shared/_Layout.cshtml     # Navbar, Logout button
│
├─ wwwroot/                      # Static files, Bootstrap, JS, CSS
│
└─ Program.cs / AddAccessControl.cs # OIDC configuration extension
```

---

## Usage Notes

* Logout uses `/Account/Logout` **href link** with a red Bootstrap button.
* Employee-only pages use **role/group claims** for access demonstration.
* All UI is **Bootstrap 5 only**, no custom CSS needed for main functionality.

---

## Troubleshooting

* **Blank page on `/signout-oidc`**: This is normal; it’s an OIDC callback endpoint. The user should be redirected to `/` or `SignedOutRedirectUri`.
* **403 or `invalid_client`**: Check `ClientId`, `ClientSecret`, and allowed **Redirect URIs** in PingOne app configuration.
* **Blue text on Logout button**: Ensure you only use `btn btn-danger` — no extra `white` class required.

---

## References

* [Ping Identity OIDC Documentation](https://docs.pingidentity.com/)
* [Microsoft Docs: ASP.NET Core Authentication](https://docs.microsoft.com/aspnet/core/security/authentication/oidc)
* [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.3/getting-started/introduction/)

---