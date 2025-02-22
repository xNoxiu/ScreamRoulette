using UnityEngine;

public class MeowTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public float volumeMultiplier = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.volume = 1f * volumeMultiplier;
                audioSource.Play(); 
            }
        }
    }
}
