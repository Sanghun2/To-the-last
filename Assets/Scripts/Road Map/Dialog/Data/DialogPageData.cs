using System.Collections.Generic;
using UnityEngine;

public class DialogPageData
{
    public enum State
    {
        Waiting,
        InProgress,
        Complete,
    }

    public string ID => id;
    public Sprite TalkerImage => talkerImage;
    public string TalkerName => talkerName;
    public string Description => description;
    public IReadOnlyList<SelectionContext> Selections => selections;
    public int MaxProgress { get; }
    public State CurrentState => currentState;


    [SerializeField] Sprite talkerImage;
    [SerializeField] string talkerName;
    [SerializeField] string description;
    private string id;
    private IReadOnlyList<SelectionContext> selections;
    private State currentState;

    public DialogPageData(
        string id,
        Sprite talkerImage,
        string talkerName,
        string description,
        IReadOnlyList<SelectionSDContext> selections) {

        this.id = id;
        this.talkerImage = talkerImage;
        this.talkerName = talkerName;
        this.description = description;
        this.selections = ConvertSelections(selections);
        MaxProgress = selections.Count;
    }

    private IReadOnlyList<SelectionContext> ConvertSelections(IReadOnlyList<SelectionSDContext> selections) {
        var list = new List<SelectionContext>();
        for (int i = 0; i < selections.Count; i++) {
            SelectionSDContext selection = selections[i];
            if (Managers.Select.TryBuildSelectionContext(selection, out var selectionContext)) {
                list.Add(selectionContext);
            }
        }

        return list;
    }
}