using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageSD", menuName = "Scriptable Objects/ImageSD")]
public class ImageSD : IconSDBase
{
    private void OnValidate() {
        RenameAsset(ID, suffix:"_ImageSD");
    }
}
