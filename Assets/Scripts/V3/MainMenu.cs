using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("NewScene");
    }

    public void ExitGame()
    {
        Debug.Log("EXIT!");
        Application.Quit();
    }
}
