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

    //$UserEmail = "correo@gmail.com";
    //$UserPassword = "25f9e794323b453885f5181f1b624d0b";

    //Querying the databsae to check if the users email is already in the db
    $LoginQuery = "SELECT username FROM users WHERE email = '".$UserEmail."' AND password = '".$UserPassword."';";

    //SELECT username FROM users WHERE email = 'correo@gmail.com' AND PASSWORD = '25f9e794323b453885f5181f1b624d0b'

    try
    {
        $LoginResult = $conn->query($LoginQuery);

        if($LoginResult == false)
        {
            die("22"); //2 = Query Failed
            //-- Primarey Key may have been violated
        }
    }
    catch(Exception $e)
    {
        die("30"); //2 = Query Failed
    }

    //echo($LoginResult->num_rows);

    if($LoginResult->num_rows > 0)
    {
        //Echo user name
        //$row = mysql_fetch_array($LoginResult);
        $row = $LoginResult->fetch_assoc();

        echo($row["username"]);
    }else
    {
        die("55"); //55 = User has not registered
    }


    //Success
    
    $conn->close();
?>