using UnityEngine;
using UnityEngine.UI;

public class RectRebuilder : MonoBehaviour
{
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

    [SerializeField] RectRebuilder[] childrenRects;
    private RectTransform _rect;

    public void Rebuild() {
        if (childrenRects == null || childrenRects.Length == 0) {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
            return;
        }

        for (int i = 0; i < childrenRects.Length; i++) {
            var child = childrenRects[i];
            child.Rebuild();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
    }
}
