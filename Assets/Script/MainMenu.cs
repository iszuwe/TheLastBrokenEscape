
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void StartGame()
    {
        // Replace "YourGameSceneName" with the name of your actual game scene
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
