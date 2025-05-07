using UnityEngine;

public class DragToMove : MonoBehaviour
{
    [Header("SmoothDamp Settings")]
    public float maxSpeed = 5f;
    public float smoothTime = 0.2f;
    private Camera cam;
    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;
    private bool isMoving = false;
    private bool dragging = false;
    void Awake()
    {
        cam = Camera.main;
    }
    void Update()
    {
#if UNITY_EDITOR
        HandleMouse();
#else
        HandleTouch();
#endif

        if (isMoving)
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPos,
                ref velocity,
                smoothTime,
                maxSpeed
            );

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                isMoving = false;
                velocity = Vector3.zero;
            }
        }
    }

    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
                dragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
        if (Input.GetMouseButton(0) && dragging)
        {
            UpdateTarget(Input.mousePosition);
        }
    }
    private void HandleTouch()
    {
        if (Input.touchCount == 0) return;
        Touch t = Input.GetTouch(0);
        Vector2 sp = t.position;
        Vector3 wp = cam.ScreenToWorldPoint(sp);
        switch (t.phase)
        {
            case TouchPhase.Began:
                RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                    dragging = true;
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                if (dragging)
                    UpdateTarget(sp);
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                dragging = false;
                break;
        }
    }

    private void UpdateTarget(Vector2 screenPos)
    {
        Vector3 world = cam.ScreenToWorldPoint(screenPos);
        world.z = transform.position.z;
        targetPos = world;
        isMoving = true;
    }
}