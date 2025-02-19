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
                transform.position = new Vector3(0f, transform.position.y, 0f); // Jeœli trzeba zresetowaæ pozycjê X i Z
                transform.rotation = Quaternion.Euler(0f, -90f, 0f); // Resetowanie rotacji kamery
                xRotation = 0f; // Resetowanie wartoœci X dla p³ynnego dzia³ania póŸniej
            }
            else
            {
                return; // Jeœli cutscenka trwa, wyjœcie z Update() bez wp³ywu na kamerê
            }
        }

        // Pobieranie wejœcia myszy
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Zmiana k¹ta obrotu kamery w osi X (góra-dó³)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Obrót kamery w osi X (góra-dó³)
        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0f);

        // Obrót ca³ego obiektu gracza w osi Y (lewo-prawo)
        player.Rotate(Vector3.up * mouseX);
    }
}
