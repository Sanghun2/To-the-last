using System;
using UnityEngine;
using UnityEngine.UI;

public class IconFloatingText : FloatingText
{
    [SerializeField] Image iconImage;

    public override FloatingText ShowText(in FloatingTextContext context) {
        SetIconImage(context);
        base.ShowText(context);
        return this;
    }

    private void SetIconImage(FloatingTextContext context) {
        iconImage.sprite = context.Icon;
        iconImage.gameObject.SetActive(context.Icon != null);
    }

    protected override void CreateAnimationTarget(ref AnimationTarget target) {
        base.CreateAnimationTarget(ref target);
        target.icon = iconImage;
    }
}
