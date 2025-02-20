using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerClickHandler
{
    public string sceneNameOne = "swiatynia";

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneNameOne);
    }
}
