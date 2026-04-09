using Unity.VisualScripting;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [field: SerializeField] public GameObject cameraFocus{get; private set;}
    [field: SerializeField] public float CameraMaxSpeed {get; private set;}
    [field: SerializeField] public float CameraOffset{get;private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position,cameraFocus.transform.position,CameraMaxSpeed*Time.deltaTime);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,CameraOffset);
    }
}
