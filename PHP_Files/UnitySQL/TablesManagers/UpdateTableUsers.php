<?php
require __DIR__ . '/../Utils/Connection.php';

// SQL query to create the 'Users' table
// - id: Auto-incrementing primary key for unique identification
// - nickname: VARCHAR(50) to store user nicknames, cannot be NULL, must be unique
// - email: VARCHAR(100) to store user emails, cannot be NULL, must be unique
// - password: VARCHAR(255) to store hashed passwords (use a strong hash like bcrypt), cannot be NULL
// - created_at: TIMESTAMP to record when the user was created, defaults to current timestamp

//$UserNickname = $_POST["username"];
//$UserPassword = $_POST["email"];
//$Username = $_POST["password"];

$UserNickname = 'username1';
$UserPassword = 'password';
$UserEmail = 'email1@email.com';

echo $UserEmail;
echo $UserPassword;
echo $Username;

$sql = "INSERT into Users 
    (nickname, email, password)
    VALUES ('".$UserNickname."', '".$UserEmail."', MD5('".$UserPassword."'));
";

$LoginResult;

try
    {
        $LoginResult = $conn->query($sql);

        if($LoginResult == false)
        {
            die("22"); //2 = Query Failed
            //-- Primarey Key may have been violated
        }
    }
    catch(Exception $e)
    {
        echo "Error creating table: " . $e;

        die("30"); //2 = Query Failed
    }

if ($LoginResult === TRUE){
    echo "Table 'Users' updated successfully in database '" . DB_NAME . "'.";
}else{
    die("55"); //55 = User has not registered
}

// Close the database connection
$conn->close();

?>
