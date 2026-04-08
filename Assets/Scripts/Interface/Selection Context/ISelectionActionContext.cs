using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public interface ISelectionActionContext
{
    SelectActionContextBase Create(SelectionSDBase selectionSD);
}