using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AspectRatioPreserver : MonoBehaviour
{
    private void Awake()
    {
        var rectTransform = GetComponent<RectTransform>();
        var deltaY = (Screen.width - Screen.height) / 2f;
        
        rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, -deltaY);
        rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, deltaY);
    }
}

