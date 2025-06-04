using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SimpleManagerDatabase : MonoBehaviour
{
    private readonly string SERVER_URL = "localhost:80/unitydatabase/";
    private readonly string CREATE_PHP = "TablesManagers/CreateTableUsers.php";
    private readonly string DROP_PHP = "TablesManagers/DropTableUser.php"; 

    public void OnClickCreateTable()
    {
        StartCoroutine(CreateTable());
    }

    public void OnClickDropTable()
    {
        StartCoroutine(DropTable());
    }

    IEnumerator CreateTable()
    {
        string CREATE_USER_PHP = $"{SERVER_URL}/{CREATE_PHP}";

        UnityWebRequest request = new UnityWebRequest(CREATE_USER_PHP);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    IEnumerator DropTable()
    {
        string DROP_USER_PHP = $"{SERVER_URL}/{DROP_PHP}";

        UnityWebRequest request = new UnityWebRequest(DROP_USER_PHP);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
