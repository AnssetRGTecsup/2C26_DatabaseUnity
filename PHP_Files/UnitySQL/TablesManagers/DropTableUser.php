<?php
require __DIR__ . '/../Utils/Connection.php';

$sql = "DROP TABLE IF EXISTS Users";

// Execute the SQL query
if ($conn->query($sql) === TRUE) {
    echo "Table 'Users' dropped successfully from database '" . DB_NAME . "'.";
} else {
    // If query fails, display error
    echo "Error dropping table: " . $conn->error;
}

// Close the database connection
$conn->close();

?>