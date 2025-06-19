<?php

/**
 *  EN XAMPP el username por default es 'root' y no hay contraseña
 * 
 * $variables -> pueden variar en tiempo de ejecución
 * $servername = "localhost";
 * $username = "root";
 * $password = "";
 * $dbname = "unitydatabase";
 * define('name', 'value') -> valores constantes invariables
 */

 define('DB_SERVER', 'localhost');
 define('DB_USERNAME', 'root');
 define('DB_PASSWORD', 'root');
 define('DB_NAME', 'unitysql');

// Create connection
$conn = new mysqli(DB_SERVER, DB_USERNAME, DB_PASSWORD, DB_NAME);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}else{
    //echo("Connection Established.<br>");
    //echo "Connected to MySQL successfully.<br>";
}
?>
