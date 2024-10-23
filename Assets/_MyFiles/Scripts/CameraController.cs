using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; 
    private float cameraDistance = 3f; 
    private float cameraHeight = 1f;
    private float cameraRotationSpeed = 5f;
    private float minCameraAngle = -45f;
    private float maxCameraAngle = 45f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void LateUpdate()
    {
        // Calculate camera position
        Vector3 cameraPosition = playerTransform.position + new Vector3(0, cameraHeight, -cameraDistance);

        // Rotate camera based on mouse input
        rotationX += Input.GetAxis("Mouse X") * cameraRotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * cameraRotationSpeed;

        // Clamp camera angle to prevent flipping
        rotationY = Mathf.Clamp(rotationY, minCameraAngle, maxCameraAngle);

        // Calculate camera rotation
        Quaternion cameraRotation = Quaternion.Euler(-rotationY, rotationX, 0);

        // Calculate camera position based on rotation
        Vector3 offset = cameraRotation * new Vector3(0, 0, -cameraDistance);
        transform.position = playerTransform.position + new Vector3(0, cameraHeight, 0) + offset;

        // Set camera rotation
        transform.rotation = cameraRotation;
    }
}