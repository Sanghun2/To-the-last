using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class ImageUI : UIBase
{
    [SerializeField] Image image;

    public void SetImage(Sprite image) {
        this.image.sprite = image;
    }

    private void Reset() {
        if (image == null) {
            image = GetComponentInChildren<Image>();
        }
    }
}
