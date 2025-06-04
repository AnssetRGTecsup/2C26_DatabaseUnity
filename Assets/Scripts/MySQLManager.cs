using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class MySQLManager
{
    //localhost:80 donde 80 es el número del puerto
    //UnitySQL es el folder o carpeta con los códidigos de php para conectarnos con nuestra base de datoss
    //Es muy diferente a "unitysql" que es el nombre de nuestra base de datos.
    readonly static string SERVER_URL = "localhost:80/UnitySQL";

    readonly static string CONNECTION_PHP = "Connection.php";
    readonly static string REGISTER_PHP = "RegisterUser2.php";
    readonly static string LOGIN_PHP = "LoginUser2.php";

    #region Connect
    public static IEnumerator ConnectDatabase()
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/{CONNECTION_PHP}";

        //UnityWebRequest request = UnityWebRequest.Post(REGISTER_USER_URL, jsonToSend)
        UnityWebRequest request = new UnityWebRequest(REGISTER_USER_URL, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"Error: {request.error}");
            yield return null;
        }
        else
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
        }
    }
    #endregion

        #region Register
    public static async Task<bool> RegisterUserTask(string email, string password, string username)
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/RegisterUser.php";

        return (await SendPostRequest(REGISTER_USER_URL, new Dictionary<string, string> {
            { "email", email },
            { "username", username },
            { "password", password }
        })).succes;   
    }

    public static IEnumerator RegisterUser(string username, string email, string password)
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/{REGISTER_PHP}";

        RegisterDataFormat registerData = new RegisterDataFormat(username, email, password);

        string jsonData = JsonUtility.ToJson(registerData);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        //UnityWebRequest request = UnityWebRequest.Post(REGISTER_USER_URL, jsonToSend)
        UnityWebRequest request = new UnityWebRequest(REGISTER_USER_URL, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"Error: {request.error}");
            yield return null;
        }
        else
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

            if (response.status == "error")
            {
                // Aquí puedes manejar el error de inicio de sesión
                Debug.LogError("Login failed: " + response.message);
            }
            else if (response.status == "success")
            {
                // Aquí puedes manejar la respuesta exitosa
                Debug.Log("Login successful: " + response.status);
            }
        }
    }
    #endregion

    #region LogIn
    public static async Task<(bool succes, string username)> LoginUserTask(string email, string password)
    {
        string LOGIN_URL = $"{SERVER_URL}/LoginUser.php";

        return await SendPostRequest(LOGIN_URL, new Dictionary<string, string> {
            { "email", email },
            { "password", password }
        });
    }

    public static IEnumerator LoginUser(string email, string password)
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/{LOGIN_PHP}";

        LoginData registerData = new LoginData(email, password);

        string jsonData = JsonUtility.ToJson(registerData);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        //UnityWebRequest request = UnityWebRequest.Post(REGISTER_USER_URL, jsonToSend)
        UnityWebRequest request = new UnityWebRequest(REGISTER_USER_URL, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.error != null ||
            !string.IsNullOrWhiteSpace(request.error) ||
            HasErrorMessage(request.downloadHandler.text) ||
            request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"Error: {request.error}");
            yield return null;
        }
        else
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

            if (response.status == "error")
            {
                // Aquí puedes manejar el error de inicio de sesión
                Debug.LogError("Login failed: " + response.message);
            }
            else if (response.status == "success")
            {
                // Aquí puedes manejar la respuesta exitosa
                Debug.Log("Login successful: " + response.status);
            }
        }
    }
    #endregion

    static async Task<(bool succes, string username)> SendPostRequest(string url, Dictionary<string, string> data)
    {
        using (UnityWebRequest req = UnityWebRequest.Post(url, data))
        {
            req.SendWebRequest();

            while (!req.isDone) await Task.Delay(100);

            Debug.Log(req.downloadHandler.text);
            Debug.Log(req.error);

            //When the task is done
            if (req.error != null || !string.IsNullOrWhiteSpace(req.error) 
                || HasErrorMessage(req.downloadHandler.text))
            {
                return (false, req.downloadHandler.text);
            }

            //On Succes
            return (true, req.downloadHandler.text);
        }
    }

    /*private static IEnumerator SendPostRequest(string url, Dictionary<string, string> data)
    {
        string json
    }*/

    static bool HasErrorMessage(string message) => int.TryParse(message, out var result);
}

public class DatabaseUser
{
    public string Email;
    public string Username;
    public string Password;
}

[System.Serializable]
public class RegisterDataFormat
{
    public string username;
    public string email;
    public string password;

    public RegisterDataFormat(string username, string email, string password)
    {
        this.username = username;
        this.email = email;
        this.password = password;
    }
}

[System.Serializable]
public class LoginData
{
    public string email;
    public string password;

    public LoginData(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}

[System.Serializable]
public class Response
{
    public string status;
    public string message;
}