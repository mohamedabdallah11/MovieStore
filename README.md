
Movie Store 🎥
An MVC-based web application for managing a movie store, built with C# and ASP.NET. The project includes robust features like CRUD operations, user authentication, movie management, search, and pagination.

🚀 Features
Admin Panel: Manage movies, genres, and users.
Movie Management: Add, edit, delete, and view movies with detailed information.
User Authentication: Register, login, change password, and manage sessions securely.
Search and Filter: Search movies by title or genre, with filters for easy discovery.
Pagination: Efficiently browse through large movie catalogs.
Genre Management: Manage and assign genres to movies.
Responsive Views: User-friendly UI with separate views for admin and general users.
🛠️ Technologies Used
Framework: ASP.NET (C#)
Database: SQL Server (or your preferred database)
ORM: Entity Framework
Frontend: Razor Views, HTML5, CSS3
Libraries/Tools:
AutoMapper (for mapping DTOs)
Pagination library (if any)
Other .NET built-in libraries
📂 Project Structure
Controllers:

AdminController: Admin-specific actions for managing users, movies, and genres.
DashboardController: Dashboard overview for admins.
GenreController: Manage genres and their relationships with movies.
MovieController: CRUD operations for movies.
HomeController: Public-facing home page and general information.
UserAuthenticationController: Handles registration, login, and user session management.
Models (Domain):

Genre: Represents movie genres.
Movie: Represents movies with details like title, genre, and release year.
User: Represents users, including roles and authentication data.
MovieGenre: Manages many-to-many relationships between movies and genres.
DTOs:

ChangePasswordDto: Handles user password updates.
LoginDto: Processes login requests.
StatusCodeDto: Uniform response codes for API actions.
RegistrationDto: Manages user registration.
MovieListVM: View model for listing movies with pagination and filters.
Repositories:

FileService: Manages file uploads and handling.
GenreService: CRUD operations for genres.
MovieService: CRUD and additional features for movies.
UserAuthenticationService: User registration, login, and session management.
Views:

Admin and User views for:
CRUD operations on movies, genres, and users.
Search and filter functionality.
Pagination support for movie listings.
📖 How to Run
Clone the Repository:

bash
Copy code
git clone https://github.com/mohamedabdallah11/MovieStore.git
cd moviestore
Setup the Database:

Configure the connection string in appsettings.json to point to your SQL Server instance.
Run migrations (if any) or use the provided .sql scripts to set up the database schema.
Build and Run the Project:

Open the project in Visual Studio.
Restore dependencies via NuGet.
Build the solution and run the application.
Access the Application:

Visit http://localhost:<port> in your browser.
Use default admin credentials if provided.
🌟 Features in Detail
Movie CRUD:

Add movies with fields like title, description, genre, and release date.
Edit and update movie information.
Delete movies.
Genres:

Create and manage genres.
Assign multiple genres to a single movie.
Search & Pagination:

Search movies by title or genre using a search box.
Browse movies with efficient pagination for better performance.
User Management:

Register and log in users securely.
Change passwords and manage user sessions.
📌 Future Enhancements
Add movie ratings and user reviews.
Implement advanced filters (e.g., release year, popularity).
Enable movie trailers and image uploads.
Enhance the UI for better user experience.
🤝 Contributing
Contributions are welcome! Feel free to fork the repository and submit pull requests.
