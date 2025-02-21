using UnityEngine;
using System.Collections;

public class MoveDoor : MonoBehaviour
{
    [SerializeField] private AudioSource doorAudio;

    private Vector3 startPos; 
    private Vector3 targetPos; 
    private bool isOpen = false; 
    public float moveSpeed = 2f; 

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + new Vector3(-0.8f, 0, 0);
    }

    void OnMouseDown()
    {
        if (!isOpen)
        {
            StartCoroutine(MoveDoorSmoothly(targetPos));
        }
        else
        {
            StartCoroutine(MoveDoorSmoothly(startPos)); 
        }
        isOpen = !isOpen;
    }

    IEnumerator MoveDoorSmoothly(Vector3 target)
    {
        float time = 0;
        Vector3 start = transform.position;
        doorAudio.Play();

        while (time < 1)
        {
            transform.position = Vector3.Lerp(start, target, time);
            time += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = target; 
    }
}
