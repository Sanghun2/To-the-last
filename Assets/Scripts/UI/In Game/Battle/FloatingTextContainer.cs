using System;
using UnityEngine;

public class FloatingTextContainer : ListContainerBase<FloatingText>
{
    [SerializeField] ColorData[] textColors;

    public void ShowText(FloatingTextContext context) {
        FloatingText textObj = GetObj();
        textObj.SetColor(GetColor(context.TextType));
        textObj.ShowText(context);
    }

    private Color GetColor(FloatingText.TextType textType) {
        return textColors[(int)textType].Color;
    }

    private void Reset() {
        var enumValues = (FloatingText.TextType[])Enum.GetValues(typeof(FloatingText.TextType));
        var enumCount = enumValues.Length;
        if (textColors == null || textColors.Length != enumCount) {
            textColors = new ColorData[enumCount];

            for (int i = 0; i < textColors.Length; i++) {
                var target = enumValues[i];
                textColors[i] = new ColorData(target.ToString());
            }
        }
    }
}

[Serializable]
public class ColorData
{
    public string ColorID => colorID;
    public Color Color => color;

    [SerializeField] string colorID;
    [SerializeField] Color color;

    public ColorData(string id) {
        colorID = id;
    }
}
