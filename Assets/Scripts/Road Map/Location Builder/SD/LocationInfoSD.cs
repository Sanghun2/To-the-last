using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationIconSD", menuName = "Scriptable Objects/LocationIconSD")]
public class LocationInfoSD : ImageSDBase
{
    public Sprite IconImage => iconImage;

    [SerializeField] Sprite iconImage;
}
