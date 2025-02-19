using UnityEngine;

public class CutscenePlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float timeElapsed = 0f; // Zmienna do mierzenia czasu
    private bool canMove = false; // Okreœla, czy gracz mo¿e siê poruszaæ

    void Update()
    {
        timeElapsed += Time.deltaTime; // Zwiêkszamy czas o ka¿d¹ klatkê

        // Sprawdzamy, czy minê³y 5 sekund od startu gry
        if (timeElapsed >= 5f)
        {
            canMove = true; // Po 5 sekundach gracz mo¿e siê poruszaæ
        }

        if (!canMove) return; // Je¿eli gracz nie mo¿e siê poruszaæ, wychodzimy z Update()

        // Sprawdzamy, czy gracz jest na ziemi
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ustalanie prêdkoœci spadania na ziemi
        }

        // Pobieranie wejœcia gracza (ruch w lewo/prawo oraz do przodu/ty³u)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Ruch w przestrzeni
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Skok
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime; // Dodawanie grawitacji
        controller.Move(velocity * Time.deltaTime); // Ruch z uwzglêdnieniem grawitacji
    }
}
