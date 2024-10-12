using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    [SerializeField] PlayerAnimatorManager playerAnimator;
    public float moveSpeed = 5f;
    public Camera mainCamera;
    public Vector2 movementBounds; // Limit the movement area

    [SerializeField] private Vector2 moveInput;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
           || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

        MovePlayer();
        RotateTowardsMouse();
        
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;

        // Limit player movement within the bounds
        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, -movementBounds.x, movementBounds.x);
        newPosition.z = Mathf.Clamp(newPosition.z, -movementBounds.y, movementBounds.y);
        transform.position = newPosition;

        playerAnimator.HandleAnimation(moveInput);
    }    

    void RotateTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0; // Keep player rotation only in the x-z plane
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
