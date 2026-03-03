using UnityEngine;

public class Player2dClass : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] public ForceReceiver forceReceiver;
    [SerializeField] private Rigidbody rb;
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void HandleMovement(Vector2 input)
    {
        
    }
    public void Jump()
    {
        rb.AddForce(0,jumpForce,0, ForceMode.Impulse);
    }
    public void Dash()
    {
        
    }
}
