<?php
require __DIR__ . '/../Utils/Connection.php';

// SQL query to create the 'Users' table
// - id: Auto-incrementing primary key for unique identification
// - nickname: VARCHAR(50) to store user nicknames, cannot be NULL, must be unique
// - email: VARCHAR(100) to store user emails, cannot be NULL, must be unique
// - password: VARCHAR(255) to store hashed passwords (use a strong hash like bcrypt), cannot be NULL
// - created_at: TIMESTAMP to record when the user was created, defaults to current timestamp
$sql = "CREATE TABLE Users (
    id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    nickname VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
)";

// Execute the SQL query
if ($conn->query($sql) === TRUE) {
    echo "Table 'Users' created successfully in database '" . DB_NAME . "'.";
} else {
    // If query fails, display error
    echo "Error creating table: " . $conn->error;
}

// Close the database connection
$conn->close();

?>
