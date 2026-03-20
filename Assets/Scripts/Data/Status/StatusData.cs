using System;
using UnityEngine;

[Serializable]
public class StatusData : DataBase
{
    public Sprite Icon => icon;
    public int MaxStack => maxStack;


    [SerializeField] private string id;
    [SerializeField] Sprite icon;
    [SerializeField] int maxStack;

    public StatusData(StatusSD statusSD) : base(statusSD.ID) {
        icon = statusSD.Image;
        maxStack = statusSD.MaxStack;
    }

    public StatusData(string id) : base(id) {

    }
}
