using UnityEngine;

public interface ISelectionActionContext
{
    SelectionActionContext Create(SelectionSD selectionSD);
}
