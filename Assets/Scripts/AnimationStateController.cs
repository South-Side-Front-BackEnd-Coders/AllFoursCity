using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;   
    int isJumpingHash;    
    private Player playerInput;
    Vector2 currentMovement;
    bool movementPressed;
    bool forwardPressed;
    bool jumpPressed;
    void Awake()
    {
          playerInput = new Player();

        playerInput.PlayerMain.Move.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();                       
            
            movementPressed =  currentMovement.y > 0 || currentMovement.y < -0 || currentMovement.x > 0 || currentMovement.x < -0 ;
        };

        playerInput.PlayerMain.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();      

        isWalkingHash = Animator.StringToHash("isWalking");    
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }

    void handleMovement () {
        bool isWalking = animator.GetBool(isWalkingHash);      
        bool isJumping = animator.GetBool(isJumpingHash);
        
        if (movementPressed && !isWalking) {
            animator.SetBool(isWalkingHash, true);
        } 
        if (!movementPressed  && isWalking) {
            animator.SetBool("isWalking", false);
        }      

        if (jumpPressed && !isJumping) {
            animator.SetBool(isJumpingHash, true);
        }
        if (!jumpPressed && isJumping) {
            animator.SetBool(isJumpingHash, false);
        }

        
    }
    void OnEnable()
    {
        playerInput.PlayerMain.Enable();
    }
    void OnDisable()
    {
        playerInput.PlayerMain.Disable();
    }
}
