using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;
    private bool isPaused;

    public GameObject leftPage;
    public GameObject rightPage;
    public GameObject[] pages;
    public int currentPage = 0;

    public Button nextButton;
    public Button prevButton;

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleBook();
            UpdatePages();
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
        //Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseBook()
    {
        bookCanvas.SetActive(false);
        //Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }






    void UpdatePages()
    {
        Debug.Log("upupupup");

        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        if (currentPage * 2 < pages.Length)
        {
            Debug.Log("aaaa");
            leftPage = pages[currentPage * 2];
            leftPage.SetActive(true); 

            if ((currentPage * 2) + 1 < pages.Length)
            {
                rightPage = pages[(currentPage * 2) + 1];
                rightPage.SetActive(true); 
            }
            else
            {
                rightPage.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Brak strony do wyœwietlenia!");
        }

        //prevButton.interactable = currentPage > 0;
        //nextButton.interactable = (currentPage * 2) + 2 < pages.Length;
    }

    public void NextPage()
    {
        if (currentPage >= (pages.Length / 2) - 1) return;

        Debug.Log("leeeeel");
        rightPage.transform.DORotate(new Vector3(0, -180, 0), 2f, RotateMode.LocalAxisAdd)
            .OnComplete(() =>
            {
                Debug.Log("bbbbb");
                currentPage++; 
                UpdatePages();
                Debug.Log("cccc");


                rightPage.transform.rotation = Quaternion.identity;
                Debug.Log("Strona zmieniona na: " + currentPage);
            });
    }

    public void PrevPage()
    {
        if (currentPage <= 0) return; //?????????????

        Debug.Log("leeeeel");
        leftPage.transform.DORotate(new Vector3(0, 180, 0), 2f, RotateMode.LocalAxisAdd)
            .OnComplete(() =>
            {
                currentPage--;
                UpdatePages();

                leftPage.transform.rotation = Quaternion.identity;
                Debug.Log("Strona zmieniona na: " + currentPage);
            });
    }



}
