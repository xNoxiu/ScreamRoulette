using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;
    public PlayerMovement playerMovement;
    public CameraLook cameraLook;
    private bool isPaused;

    public GameObject leftPage;
    public GameObject rightPage;
    public GameObject[] pages;
    public int currentPage = 0;

    public Button nextButton;
    public Button prevButton;

    private bool isAnimating;

    

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
        playerMovement.enabled = false;
        cameraLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UpdatePages();
    }

    void CloseBook()
    {
        bookCanvas.SetActive(false);
        playerMovement.enabled = true;
        cameraLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }






    void UpdatePages()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        if (currentPage * 2 < pages.Length)
        {
            pages[currentPage * 2].SetActive(true); // Lewa strona
            if ((currentPage * 2) + 1 < pages.Length)
                pages[(currentPage * 2) + 1].SetActive(true); // Prawa strona
        }

        prevButton.interactable = currentPage > 0;
        nextButton.interactable = (currentPage * 2) + 2 < pages.Length;
    }

    public void NextPage()
    {
        if (isAnimating || currentPage >= (pages.Length / 2) - 1) return;
        isAnimating = true;

        GameObject rightPage = pages[(currentPage * 2) + 1];
        GameObject leftPage = pages[currentPage * 2];

        rightPage.transform.DORotate(new Vector3(0, -180, 0), 1f, RotateMode.LocalAxisAdd)
            .OnUpdate(() =>
            {
                UpdatePages();
            })
            .OnComplete(() =>
            {
                currentPage++;
                UpdatePages();
                isAnimating = false;
            });
    }

    public void PrevPage()
    {
        if (isAnimating || currentPage <= 0) return;
        isAnimating = true;

        GameObject leftPage = pages[currentPage * 2];
        GameObject rightPage = pages[(currentPage * 2) + 1];

        leftPage.transform.DORotate(new Vector3(0, 180, 0), 1f, RotateMode.LocalAxisAdd)
            .OnUpdate(() =>
            {
                UpdatePages();
            })
            .OnComplete(() =>
            {
                currentPage--;
                UpdatePages();
                isAnimating = false;
            });
    }



}
