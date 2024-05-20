using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private Vector3 minBoundary;
    [SerializeField] private Vector3 maxBoundary;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundary.x, maxBoundary.x);
        newPosition.z = Mathf.Clamp(newPosition.z, minBoundary.z, maxBoundary.z);

        transform.position = newPosition;
    }
}
