<?php
    $servername = "localhost";
    $DbUsername = 'root';
    $DbPassword = 'root';
    $dbname = "unitysql";

    //Create Connection to database
    $conn = new mysqli($servername, $DbUsername, $DbPassword, $dbname);

    //Check connection
    if($conn->connect_error)
    {
        die("1"); //1 = connection to database failed
    }

    $UserEmail = $_POST["email"];
    $UserPassword = MD5($_POST["password"]);
    $Username = $_POST["username"];

    //Querying the databsae to check if the users email is already in the db
    $RegisterUserQuery = "INSERT INTO users VALUES('".$UserEmail."','".$Username."','".$UserPassword."');";

    try
    {
        $RegisterUserResult = $conn->query($RegisterUserQuery);

        if($RegisterUserResult == false)
        {
            die("22"); //2 = Query Failed
            //-- Primarey Key may have been violated
        }
    }
    catch(Exception $e)
    {
        die("30"); //2 = Query Failed
    }

    //Success
    
    $conn->close();
?>