using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using static System.Net.WebRequestMethods;

public class CredentialsManager : MonoBehaviour
{
    [Header("Register")]
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField userlInput;
    [SerializeField] private TMP_InputField passwordInput;

    private void Start()
    {
     //   StartCoroutine(MySQLManager.ConnectDatabase());
    }

    public async void OnRegisterPressed()
    {
        if(string.IsNullOrEmpty(emailInput.text) || emailInput.text.Length < 5)
        {
            Debug.Log("Please enter a valid Email.");
            return;
        }

        if (string.IsNullOrEmpty(userlInput.text) || userlInput.text.Length < 5)
        {
            Debug.Log("Please enter a valid Username.");
            return;
        }

        if (string.IsNullOrEmpty(passwordInput.text) || passwordInput.text.Length < 5)
        {
            Debug.Log("Please enter a valid Password.");
            return;
        }

        /*if(await MySQLManager.RegisterUserTask(emailInput.text, passwordInput.text, userlInput.text))
        {
            Debug.Log("Succes");
        }
        else
        {
            Debug.Log("Error at Register");
        }*/

        StartCoroutine(MySQLManager.RegisterUser(userlInput.text, emailInput.text, passwordInput.text));
    }

    public async void OnLoginPressed()
    {
        if (string.IsNullOrEmpty(emailInput.text) || emailInput.text.Length < 5)
        {
            Debug.Log("Please enter a valid Email.");
            return;
        }

        if (string.IsNullOrEmpty(passwordInput.text) || passwordInput.text.Length < 5)
        {
            Debug.Log("Please enter a valid Password.");
            return;
        }

        /*(bool success, string username) = await MySQLManager.LoginUserTask(emailInput.text, passwordInput.text);

        if (success)
        {
            Debug.Log($"Succesfully {username} Logged In");
        }
        else
        {
            Debug.Log("Failed to Log In");
        }*/

        StartCoroutine(MySQLManager.LoginUser(emailInput.text, passwordInput.text));
    }

}

public class Login : MonoBehaviour
{
    public string url = "http://localhost/prueba.php";
    public InputField emailInputField;
    public InputField passwordInputField;
    public Button loginButton;
    [SerializeField] LoginData loginData;
    [SerializeField] Response response;


    void Start()
    {
        // Asegúrate de que el botón tenga asignado el evento de clic
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    void OnLoginButtonClicked()
    {
        loginData.email = emailInputField.text;
        loginData.password = passwordInputField.text;
        this.loginButton.interactable = false;
        //print(loginData.email + " " + loginData.password);
        StartCoroutine(TryLogin());
    }

    IEnumerator TryLogin()
    {
        string jsonData = JsonUtility.ToJson(loginData);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            this.loginButton.interactable = true;
        }
        else
        {
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
            if (response.status == "error")
            {
                // Aquí puedes manejar el error de inicio de sesión
                Debug.LogError("Login failed: " + response.message);
                this.loginButton.interactable = true;
            }
            else if (response.status == "success")
            {
                // Aquí puedes manejar la respuesta exitosa
                Debug.Log("Login successful: " + response.status);
                // Puedes cargar una nueva escena o realizar otra acción
            }
        }
    }
}
