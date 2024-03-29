
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed = 2.0f;
     [SerializeField]
    private float jumpHeight = 1.0f;
     [SerializeField]
    private float gravityValue = -9.81f;
     [SerializeField]
    private float rotationSpeed = 4f;

    private Transform cameraMain;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    private Player playerInput;

    private Transform child;

    void Awake()
    {
        playerInput = new Player();
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }
    void OnDisable()
    {
        playerInput.Disable();
    }
    private void Start()
    {
        cameraMain = Camera.main.transform;
        child = transform.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (playerInput.PlayerMain.Jump.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movementInput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, cameraMain.localEulerAngles.y,child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, rotation, Time.deltaTime * rotationSpeed);

        }
    }
}
