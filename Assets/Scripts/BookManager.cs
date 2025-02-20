using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;


public class BookManager : MonoBehaviour
{
    public GameObject bookCanvas;
    public PlayerMovement playerMovement;
    public CameraLook cameraLook;
    private bool isPaused;

    public GameObject[] pages;
    public int currentPage = 0;

    public GameObject nextButton;
    public GameObject prevButton;

    private bool isAnimating;

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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

        prevButton.SetActive(currentPage > 0);
        nextButton.SetActive((currentPage * 2) + 2 < pages.Length);
    }

    public void NextPage()
    {
        if (isAnimating || currentPage >= (pages.Length / 2) - 1) return;
        isAnimating = true;

        GameObject rightPage = pages[(currentPage * 2) + 1];

        GameObject nextLeftPage = pages[(currentPage + 1) * 2];
        GameObject nextRightPage = pages[((currentPage + 1) * 2) + 1];
        nextLeftPage.SetActive(true);
        nextRightPage.SetActive(true);

        rightPage.transform.SetSiblingIndex(pages.Length);

        rightPage.transform.DORotate(new Vector3(0, 180, 0), 1f, RotateMode.LocalAxisAdd)
            .OnUpdate(() =>
            {
                RearrangePages();
            })
            .OnComplete(() =>
            {
                currentPage++;
                UpdatePages();
                RearrangePages();
                rightPage.transform.rotation = Quaternion.identity;
                isAnimating = false;
            });
    }

    public void PrevPage()
    {
        if (isAnimating || currentPage <= 0) return;
        isAnimating = true;

        GameObject leftPage = pages[currentPage * 2];

        GameObject prevRightPage = pages[((currentPage - 1) * 2) + 1];
        GameObject prevLeftPage = pages[(currentPage - 1) * 2];
        prevLeftPage.SetActive(true);
        prevRightPage.SetActive(true);

        prevLeftPage.transform.SetSiblingIndex(pages.Length - 1);
        prevRightPage.transform.SetSiblingIndex(pages.Length);

        leftPage.transform.SetSiblingIndex(pages.Length);

        leftPage.transform.DORotate(new Vector3(0, -180, 0), 1f, RotateMode.LocalAxisAdd)
            .OnStart(() =>
            {
                RearrangePages();
            })
            .OnUpdate(() =>
            {
                leftPage.transform.SetSiblingIndex(pages.Length);
            })
            .OnComplete(() =>
            {
                currentPage--;
                RearrangePages();
                UpdatePages();
                leftPage.transform.rotation = Quaternion.identity;
                isAnimating = false;
            });
    }

    void RearrangePages()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].transform.SetSiblingIndex(i);
        }

        if (currentPage * 2 < pages.Length)
        {
            GameObject leftPage = pages[currentPage * 2];
            leftPage.transform.SetSiblingIndex(pages.Length - 1);
        }

        if ((currentPage * 2) + 1 < pages.Length)
        {
            GameObject rightPage = pages[(currentPage * 2) + 1];
            rightPage.transform.SetSiblingIndex(pages.Length);
        }

    }



}
