using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class UserDataHandler : MonoBehaviour
{
    public string Nickname;
    public string Email;
    public string Password;

    private readonly string SERVER_URL = "localhost:80/unitysql/";
    private readonly string CREATE_PHP = "DatabaseOperations/Users/CreateUser.php";
    private readonly string READ_PHP = "DatabaseOperations/Users/ReadUser.php";
    private readonly string UPDATE_PHP = "DatabaseOperations/Users/UpdateUser.php";
    private readonly string DELETE_PHP = "DatabaseOperations/Users/DeleteUser.php";

    public void OnClickCreateUser()
    {
        StartCoroutine(CreateUser());
    }

    public void OnClickReadUser()
    {
        StartCoroutine(ReadeUser());
    }

    public void OnClickUpdateUser()
    {
        //StartCoroutine(UpdateTable());
    }

    public void OnClickDeleteUser()
    {
        //StartCoroutine(DropTable());
    }

    private IEnumerator CreateUser()
    {
        string CREATE_USER_PHP = $"{SERVER_URL}/{CREATE_PHP}";

        User newUser = new User(Nickname, Email, Password);

        string jsonData = JsonUtility.ToJson(newUser);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(CREATE_USER_PHP, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Create User Completed!");
            Debug.Log(request.downloadHandler.text);
        }
    }

    private IEnumerator ReadeUser()
    {
        string READ_USER = $"{SERVER_URL}/{READ_PHP}";

        User newUser = new User(Nickname, Email, Password);

        string jsonData = JsonUtility.ToJson(newUser);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(READ_USER, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Read User Completed!");
            Debug.Log(request.downloadHandler.text);
            UserResponse response = JsonUtility.FromJson<UserResponse>(request.downloadHandler.text);
            if (response.success)
            {
                Debug.Log("Read successful: " + response.data.ToString());
            }
            else
            {
                Debug.LogError("Read failed: " + response.message);
            }
        }
    }
}

[System.Serializable]
public class User
{
    public string nickname;
    public string email;
    public string password;

    public User(string nickname, string email, string password)
    {
        this.nickname = nickname;
        this.email = email;
        this.password = password;
    }
}

[System.Serializable]
public class UserResponse
{
    public bool success;
    public string message;
    public UserData data;
}

[System.Serializable]
public class UserData
{
    public string id;
    public string nickname;
    public string email;
    public string password_hash;
    public string created_at;

    public override string ToString()
    {
        return $"id: {id}, nickname: {nickname}, email: {email}, password_hash: {password_hash}, created_at: {created_at}";
    }
}