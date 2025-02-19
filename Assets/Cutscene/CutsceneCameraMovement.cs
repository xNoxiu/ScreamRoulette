using UnityEngine;

public class CutsceneCameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform player;

    private float xRotation = 0f;
    private float timeElapsed = 0f;
    private bool canLook = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canLook)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= 5f)
            {
                canLook = true;

                // Resetowanie pozycji i rotacji kamery
                transform.position = new Vector3(0f, transform.position.y, 0f); // Je�li trzeba zresetowa� pozycj� X i Z
                transform.rotation = Quaternion.Euler(0f, -90f, 0f); // Resetowanie rotacji kamery
                xRotation = 0f; // Resetowanie warto�ci X dla p�ynnego dzia�ania p�niej
            }
            else
            {
                return; // Je�li cutscenka trwa, wyj�cie z Update() bez wp�ywu na kamer�
            }
        }

        // Pobieranie wej�cia myszy
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Zmiana k�ta obrotu kamery w osi X (g�ra-d�)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Obr�t kamery w osi X (g�ra-d�)
        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0f);

        // Obr�t ca�ego obiektu gracza w osi Y (lewo-prawo)
        player.Rotate(Vector3.up * mouseX);
    }
}
