using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilityContentSD", menuName = "Scriptable Objects/Content/UtilityContentSD")]
public class UtilityContentSD : TimeBasedSD
{
    public IReadOnlyList<Effect> Effects => effects;

    [SerializeField] Effect[] effects;

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_UtilityContentSD");
    }
}
