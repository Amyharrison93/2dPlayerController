using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PhysicsHandler : MonoBehaviour
{
    [field:SerializeField] private float Gravity = 10;
    [field:SerializeField] private float Weight;
    [field:SerializeField] private GameObject physicsObject;
    [field:SerializeField] private Vector3 previousPosition;
    [field:SerializeField] private Vector3 currentSpeed;
    [field:SerializeField] private string groundTag;
    [field:SerializeField] private string wallTag;
    private void Update()
    {
        ApplyInertia();
        //ApplyGravity();
        SetSpeed();
    }
    private void SetSpeed()
    {
        currentSpeed = physicsObject.transform.position-previousPosition;
        previousPosition = physicsObject.transform.position;
    }
    public void ApplyGravity()
    {
        physicsObject.transform.Translate(0,-Gravity*Time.deltaTime,0);
    }
    private void ApplyInertia()
    {
        Vector3 inertia = currentSpeed*0.9f;
        physicsObject.transform.Translate(inertia);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            HandleLanding(collision.gameObject);
        }
        if (collision.gameObject.CompareTag(wallTag))
        {
            //wall shit???
        }
    }
    private void HandleLanding(GameObject gameObject)
    {
        //snap above ground
        physicsObject.transform.position = new Vector3(
            physicsObject.transform.position.x,
            gameObject.transform.position.y+1,
            physicsObject.transform.position.z
        );
    }
}
