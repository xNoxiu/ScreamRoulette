using UnityEngine;
using UnityEngine.UI;


public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;
    private bool isPaused;

    public Image leftPage;
    public Image rightPage;
    public Sprite[] pages;
    private int currentPage = 0;

    public Button nextButton;
    public Button prevButton;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleBook();
        }

        UpdatePages();
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






    void UpdatePages()
    {
        if (currentPage * 2 < pages.Length)
            leftPage.sprite = pages[currentPage * 2];
        else
            leftPage.sprite = null;

        if ((currentPage * 2) + 1 < pages.Length)
            rightPage.sprite = pages[(currentPage * 2) + 1];
        else
            rightPage.sprite = null;

        prevButton.interactable = currentPage > 0;
        nextButton.interactable = (currentPage * 2) + 2 < pages.Length;
    }

    public void NextPage()
    {
        if ((currentPage * 2) + 2 < pages.Length)
        {
            currentPage++;
            UpdatePages();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePages();
        }
    }



}
