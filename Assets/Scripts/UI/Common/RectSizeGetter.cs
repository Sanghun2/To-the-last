using UnityEngine;

public class RectSizeGetter : MonoBehaviour
{
    public float Width => Rect.rect.width;
    public float Height => Rect.rect.height;
    private RectTransform Rect
    {
        get
        {
            if (_rect == null) {
                _rect = GetComponent<RectTransform>();
            }

            return _rect;
        }
    }


    [SerializeField] bool logSize;
    private RectTransform _rect;


    private void OnEnable() {
        if (logSize) {
            Debug.Log($"[Test] w? {Width} h? {Height}");
        }
    }
}
