using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public PlayerMovement player; 
    public CameraLook cameraLook;
    public GameObject book;
    public float cutsceneDuration = 5f;


    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        player.enabled = false;
        cameraLook.enabled = false;

        yield return new WaitForSeconds(cutsceneDuration);

        player.enabled = true;
        cameraLook.enabled = true;

        Destroy(book);

    }
}
