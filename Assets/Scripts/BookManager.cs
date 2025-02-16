using UnityEngine;
using UnityEngine.UI;


public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;
    private bool isPaused;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleBook();
        }
    } 

    void ToggleBook()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            OpenBook();
        }
        else
        {
            CloseBook();
        }
    }

    void OpenBook()
    {
        bookCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseBook()
    {
        bookCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



}
