using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageSD", menuName = "Scriptable Objects/ImageSD")]
public class IconSD : ImageSDBase
{
    private void OnValidate() {
        RenameAsset(ID, suffix:"_ImageSD");
    }
}
