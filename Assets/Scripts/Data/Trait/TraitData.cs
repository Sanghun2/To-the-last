using UnityEngine;

public class TraitData : BaseData
{
    public string DisplayText => displayText;
    public string Descripion => description;
    public Sprite IconImage => iconImage;
    public int Cost => cost;

    [SerializeField] string displayText;
    [SerializeField] string description;
    [SerializeField] Sprite iconImage;
    [SerializeField] int cost;

    public TraitData(string id, string displayText,string description, Sprite iconImage, int cost) : base(id) {
        this.displayText = displayText;
        this.description = description;
        this.iconImage = iconImage;
        this.cost = cost;
    }
}
