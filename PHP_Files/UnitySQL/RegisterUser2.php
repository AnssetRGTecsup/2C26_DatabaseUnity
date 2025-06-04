<?php
    require 'Connection.php';
    
    header("Access-Control-Allow-Origin: *");
    header("Content-Type: application/json");

    echo "Trying to access";

    //Check connection
    if($conn->connect_error)
    {
        die("1"); //1 = connection to database failed
    }

    /*$data = json_decode(file_get_contents("php://input"), true);
    $UserEmail = $data["email"];
    $UserPassword = $data["password"];
    $Username = $data["username"];*/

    $UserEmail = 'email1@email.com';
    $UserPassword = 'password';
    $Username = 'username1';

    echo $UserEmail;
    echo $UserPassword;
    echo $Username;

    //Querying the databsae to check if the users email is already in the db
    //$RegisterUserQuery = "INSERT INTO users VALUES('".$UserEmail."','".$Username."','".$UserPassword."');";

    $stmt = $conn->prepare("CALL insert_new_user(?, ?, ?)");

    echo "\nQuery Created\n";

    $stmt->bind_param('sss', $Username, $UserEmail, $UserPassword);

    echo "Parameters Assigned\n";

    $stmt->execute();

    echo "Query Executed\n";

    $result = $stmt->get_result();

    //echo "Query Finished";

    //echo $result;

    if ($result) {
        echo json_encode(["status" => "success", "message" => "Usuario Creado"]);
    } else {
        echo json_encode(["status" => "error", "message" => "Problema de Query"]);
    }

    /*try
    {
        $RegisterUserResult = $conn->query($stmt);

        echo "Trying Query";

        if($RegisterUserResult == false)
        {
            echo json_encode(["status" => "error", "message" => "Usuario ya creado"]);
            die("22"); //2 = Query Failed
            //-- Primarey Key may have been violated
        }else{
            echo json_encode(["status" => "success", "message" => "Usuario Creado"]);
            echo "Success";
        }
    }
    catch(Exception $e)
    {
        echo "Not working Query";

        die("30"); //2 = Query Failed
        echo json_encode(["status" => "error", "message" => "Problema de Query"]);
    }*/

    echo "\nEnding Process\n";
    //Success
    
    $stmt->close();
    $conn->close();
?>