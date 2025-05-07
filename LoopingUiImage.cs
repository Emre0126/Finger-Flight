using UnityEngine;

public class InfiniteUIBackground : MonoBehaviour
{
    public RectTransform[] backgrounds;
    public Camera mainCamera;
    void Start()
    {
        Canvas.ForceUpdateCanvases();
    }
    void Update()
    {
        float camLeft = mainCamera.transform.position.x - (mainCamera.orthographicSize * mainCamera.aspect);
        foreach (var bg in backgrounds)
        {
            float bgRightEdge = bg.position.x + GetWorldWidth(bg) / 2f;
            if (bgRightEdge < camLeft)
            {
                ShiftAllBackgroundsRight();
                break;
            }
        }
    }
    void ShiftAllBackgroundsRight()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float shift = GetWorldWidth(backgrounds[i]);
            backgrounds[i].position += new Vector3(shift, 0f, 0f);
        }
    }
    float GetWorldWidth(RectTransform rt)
    {
        return rt.rect.width * rt.lossyScale.x;
    }
}
