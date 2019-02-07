using UnityEngine;

[RequireComponent(typeof(RectTransform), typeof(Collider2D))]
public class Collider2DRaycastFilter : MonoBehaviour, ICanvasRaycastFilter
{
    private Collider2D myCollider;
    private RectTransform rectTransform;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        rectTransform = GetComponent<RectTransform>();
    }
    
    public bool IsRaycastLocationValid(Vector2 screenPos, Camera eventCamera)
    {
        var isInside = RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            screenPos,
            eventCamera,
            out var worldPoint
        );
        if (isInside)
            isInside = myCollider.OverlapPoint(worldPoint);
        return isInside;
    }
}