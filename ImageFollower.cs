using UnityEngine;

public class ImageFollower : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 lastCamPos;

    void Start()
    {
        lastCamPos = cameraTransform.position;
    }
    void Update()
    {
        Vector3 delta = cameraTransform.position - lastCamPos;
        transform.position -= delta;
        lastCamPos = cameraTransform.position;
    }
}
