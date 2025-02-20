using UnityEngine;

public class MeowTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play(); 
                Debug.Log("MEOW! D�wi�k KITTEEEEEYYYYYY odtworzony!");
            }
        }
    }
}
