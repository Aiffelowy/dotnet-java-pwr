# Lab API

I implemented a API client and database storage that interacts with a public API (`jsonplaceholdertypicodecom`) The goal is to fetch posts and their comments and cache them in a local SQLite database

## Classes

---

### ApiError Struct

Represents an error encountered during an API or database operation

**Properties:**

- `string msg`: A message describing the error

**Methods:**

- `ApiError(string msg)`: Constructs the error with a custom message
- `ApiError(Exception e)`: Extracts the message from an exception
- `string ToString()`: Returns the error message

---

### DbManager Class

Handles database interactions and schema definition

**Properties:**

- `DbSet<Post> posts`: Table of posts
- `DbSet<Comment> comments`: Table of comments

**Methods:**

- `DbManager()`: Ensures the database is created
- `OnConfiguring(DbContextOptionsBuilder optionsBuilder)`: Configures the SQLite database path

---

### PostJson Class

Class used for deserializing JSON post data from the API

**Properties:**

- `int userId`: ID of the user
- `string title`: Title of the post
- `string body`: Body content of the post

---

### Post Class

Represents a post

**Properties:**

- `int plsWork`: Mapping to the post ID from the API
- `int userId`: ID of the user
- `int id`: Primary key 
- `string title`: Title of the post
- `string body`: Body content of the post

**Methods:**

- `string ToString()`: Returns a formatted string with the post’s title and body

---

### Comment Class

Represents a comment related to a specific post

**Properties:**

- `int id`: Primary key
- `int postId`: Foreign key referencing a post
- `string name`: Name of the comment author
- `string email`: Email of the comment author
- `string body`: Body content of the comment

**Methods:**

- `string ToString()`: Returns a formatted string with the comment’s name, email, and body

---

### ApiWrapper Class

Main class handling fetching, caching, and returning posts and comments

**Properties:**

- `HttpClient httpclient`: Used for API communication
- `DbManager database`: Handles SQLite database storage
- `string url`: Base URL of the API

**Methods:**

- `ApiWrapper()`: Initializes the HTTP client and the local database

**Private Methods:**

- `Task<Result<Post, ApiError>> fetch_post(int post_id)`: Fetches a post from the API 
- `Task<Result<List<Comment>, ApiError>> fetch_comments(int post_id)`: Fetches comments from the API
- `Task<Result<Nothing, ApiError>> db_add_post(Post post)`: Adds a post to the database
- `Task<Result<Nothing, ApiError>> db_add_comments(List<Comment> comments)`: Adds multiple comments to the database

**Public Methods:**

- `Task<Result<Post, ApiError>> get_post(int post_id)`: Returns a post from the database if cached, otherwise fetches from API and stores it
- `Task<Result<List<Comment>, ApiError>> get_comments(int post_id)`: Same logic as `get_post`, but for comments
