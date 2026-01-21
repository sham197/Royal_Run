using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float xClamp = 3f;
    [SerializeField] private float zClamp = 3f;
    
    private Vector2 _movement;

    public void Move(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var currentPosition = rb.transform.position;
        var moveDirection = new Vector3(_movement.x, 0f, _movement.y);
        var newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        
        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);
        
        rb.MovePosition(newPosition);
    }
    
    
}