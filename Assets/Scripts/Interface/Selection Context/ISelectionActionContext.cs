using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

public interface ISelectionActionContext
{
    SelectActionContext Create(SelectionSD selectionSD);
}