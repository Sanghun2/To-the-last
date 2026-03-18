using System;
using UnityEngine;

[Serializable]
public class StatusData
{
    public string ID => id;
    public Sprite Icon => icon;
    public int MaxStack => maxStack;


    [SerializeField] private string id;
    [SerializeField] Sprite icon;
    [SerializeField] int maxStack;

    public StatusData(StatusSD statusSD) {
        id = statusSD.ID;
        icon = statusSD.Image;
        maxStack = statusSD.MaxStack;
    }
}
