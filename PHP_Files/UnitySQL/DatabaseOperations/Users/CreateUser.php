<?php
    require __DIR__ . '/../../Utils/Connection.php';

    
    $data = json_decode(file_get_contents("php://input"), true);

    $UserNickname = $data["nickname"];
    $UserEmail = $data["email"];
    $UserPassword = $data["password"];
    
    /*
    $UserNickname = 'username1';
    $UserPassword = 'password';
    $UserEmail = 'email1@email.com';
    echo $UserNickname . "<br>";
    echo $UserEmail . "<br>";
    echo $UserPassword . "<br>";
    */

    $stmt = $conn->prepare("CALL Create_User(?, ?, ?)");

    if($stmt == false){
        die("Error preparing statement: ". $conn->error);
    }else{
        echo "<br>Procedure Prepared<br>";
    }

    $stmt->bind_param("sss", $UserNickname, $UserEmail, $UserPassword);

    echo "Parameters Prepared<br>";

    $execute_result = $stmt->execute();

    if($execute_result == true){
        echo "New 'Users' successfully created in database '" . DB_NAME . "'.";
    }else if($execute_result == false){
        echo "Error calling stored procedure: " . $stmt->error;
    }else{
        echo "Unexpected result from execute: " . var_export($execute_result, true) . "<br>";
    }

    $stmt->close();
    $conn->close();

    /*
    WITHOUT PROCEDURE
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
    $conn->close();*/
?>