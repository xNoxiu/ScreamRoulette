using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    private bool isOptionsOpen;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void ToggleOptions()
    {
        bool isActive = options.activeSelf; 
        options.SetActive(!isActive);

        Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isActive;
    }

}
