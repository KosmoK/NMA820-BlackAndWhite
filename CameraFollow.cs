using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera camera;
    public GameObject followObject;
    public float lerpAgressiveness = 0.02f;
    public float maxCameraDistance = 10f;
    public float cameraHeightDistance = -10f;
    public Vector2 distanceVector = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
        transform.position = followObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the camera with lerp
        Vector3 followObjectPosition = followObject.transform.position;
        float t = lerpAgressiveness * Time.deltaTime;

        Vector3 lerpPosition = Vector3.Lerp(transform.position, followObjectPosition, t);
        distanceVector = new(lerpPosition.x - followObjectPosition.x, lerpPosition.y - followObjectPosition.y);

        // If the distance is too far, clamp it
        if (distanceVector.magnitude > maxCameraDistance)
        {
            distanceVector.Normalize();
            distanceVector = new(distanceVector.x * maxCameraDistance, distanceVector.y * maxCameraDistance);
            lerpPosition = new(followObjectPosition.x + distanceVector.x, followObjectPosition.y + distanceVector.y, cameraHeightDistance);
        }

        // Flip the camera's position by X/Y on FollowObject's Axis
        Vector2 flippedDistanceVector = distanceVector * -1;
        Vector3 newDistance = new(followObjectPosition.x + flippedDistanceVector.x, followObjectPosition.y + flippedDistanceVector.y, cameraHeightDistance);

        // Set the parent and child to follow
        camera.transform.position = new(newDistance.x, newDistance.y, cameraHeightDistance);
        transform.position = new(lerpPosition.x, lerpPosition.y, cameraHeightDistance);

    }
}
