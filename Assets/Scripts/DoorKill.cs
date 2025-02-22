using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class DoorKill : MonoBehaviour
{
    public GameObject wrongText;
    public bool kill;
    public bool nextScene;

    private void OnTriggerEnter(Collider other)
    {
        if(kill)
        {
            if (other.CompareTag("Player"))
            {
                wrongText.SetActive(true);
                StartCoroutine(GameOverScene());
            }
        }
        else if (nextScene)
        {
            StartCoroutine(HanakoScene());
        }
    }

    IEnumerator GameOverScene()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(6);
    }

    IEnumerator HanakoScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(5);
    }


}
