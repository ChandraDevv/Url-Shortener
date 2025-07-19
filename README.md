# URL Shortener

A simple ASP.NET Core Web API for shortening URLs.

## Features

- Shorten long URLs to a short, shareable link
- Retrieve the original URL from a shortened link
- In-memory storage (no database required)
- Ready for extension

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Running the Project

```sh
dotnet watch run
```

The API will be available at `http://localhost:5093` (or the port shown in your terminal).

### Example Endpoints

#### Shorten a URL

- **POST** `/shorten`
- **Body:** Plain string (e.g. `"https://example.com"`)

#### Retrieve Original URL

- **GET** `/expand?shortUrl=http://anya.ly/ABC123`

### Project Structure

- `Controllers/UrlController.cs` — API endpoints
- `Services/UrlShortnerService.cs` — URL shortening logic
- `Program.cs` — App startup and configuration
