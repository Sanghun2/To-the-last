using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientRewardSD", menuName = "Scriptable Objects/Reward/IngredientRewardSD")]
public class IngredientRewardSD : RewardSDBase
{
    public IReadOnlyList<Type> AllowTypes => allowTypes;

    private List<Type> allowTypes = new List<Type>() {
        typeof(ItemSD),
    };
}
