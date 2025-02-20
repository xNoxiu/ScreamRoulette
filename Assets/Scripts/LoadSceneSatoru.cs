using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneSatoru : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Satoru");
        }
    }
}