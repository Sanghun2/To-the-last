using BilliotGames;
using UnityEngine;

public abstract class NPCSDBase : SDBase
{
    public string CategoryID => categoryList != null && categoryList.Count > 0 ? categoryList[0].ID : string.Empty;
    public Sprite IconImage => iconImage;
    public Sprite MainImage => mainImage;

    [SerializeField] Sprite iconImage;
    [SerializeField] Sprite mainImage;
}