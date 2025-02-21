using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("----Clips----")]
    public AudioClip bookOpening;
    public AudioClip bookClosing;
    public AudioClip pageFlip;
    public AudioClip doorSlide;

    public AudioSource audioSourceScream;

    public bool dontscream;
    public bool scream;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(dontscream)
            {
                audioSourceScream.Stop();
            }
            else if (scream)
            {
                audioSourceScream.Play();
            }
        }
    }
}
