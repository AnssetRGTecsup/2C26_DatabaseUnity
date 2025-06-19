<?php
    require __DIR__ . '/../../Utils/Connection.php';
    header("Access-Control-Allow-Origin: *");
    header("Content-Type: application/json");
    
    $response = [
        'success' => false,
        'message' => '',
        'data' => null
    ];
    
    $data = json_decode(file_get_contents("php://input"), true);

    //$UserEmail = $data["email"];
    $UserEmail = "test_email@gmail.com";

    $stmt = $conn->prepare("CALL Read_User(?)");

    if($stmt == false){
        $response['message'] = "Error preparing statement: ". $conn->error;
        
        $conn->close();

        echo json_encode($response);

        exit();
    }else{
        //echo "<br>Procedure Prepared<br>";
    }

    $stmt->bind_param("s", $UserEmail);

    //echo "Parameters Prepared<br>";

    $execute_result = $stmt->execute();

    //echo "Executed<br>";

    if($execute_result == true){

        //echo "TRUE RESULT<br>";

        $result = $stmt->get_result();

        //echo "GET RESULT<br>";
        
        if($result->num_rows > 0){

            //echo "EXISTS EMAIL <br>";

            $user_data = $result->fetch_assoc();
            $response['success'] = true;
            $response['message'] = "User data retrieved succesfully";
            $response['data'] =  $user_data;

            //echo $user_data;
        }else{
            //echo "DOES NOT EXIST EMAIL <br>";

            $response['message'] = "No user found with email: " . $UserEmail;
        }

        $result->free();
        
        //echo "New 'Users' successfully created in database '" . DB_NAME . "'.";
    }else if($execute_result == false){

        //echo "FALSE RESULT<br>";

        $response['message'] = "Error calling stored procedure: " . $stmt->error;

        $stmt->close();
        $conn->close();

        echo json_encode($response);

        exit();
    }else{

        //echo "AEA RESULT<br>";

        $response['message'] = "Unexpected result from execute: " . var_export($execute_result, true) . "<br>";
        
        $stmt->close();
        $conn->close();

        echo json_encode($response);

        exit();
    }

    $stmt->close();
    $conn->close();

    echo json_encode($response);
?>