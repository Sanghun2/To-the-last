using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "EndingSD", menuName = "Scriptable Objects/EndingSD")]
public class EndingSD : ImageSDBase
{
    public Sprite IconImage => Image;
    public string Text => displayText;

    public Define.EndingType EndingType => endingType;

    [SerializeField] Define.EndingType endingType;
}
