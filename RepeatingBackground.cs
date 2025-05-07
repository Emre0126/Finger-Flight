using UnityEngine;

public class BackgroundFollower : MonoBehaviour
{
    public float scrollSpeed = 2f;
    private CameraMover cameraMover;

    void Start()
    {
        cameraMover = Camera.main.GetComponent<CameraMover>();
    }

    void Update()
    {
        if (cameraMover != null && cameraMover.ShouldMove)
        {
            transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
        }
    }
}
