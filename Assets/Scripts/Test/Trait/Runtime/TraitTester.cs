using System;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed class TraitTester : MonoBehaviour
{
    [SerializeField] TraitSD[] testTraits;
    [SerializeField] int testPoint;

    public void ShowTraitList() {
        var ui = Managers.UI.OpenUI<TraitSelectionUI>();
        var traitList = testTraits.Select(t => new Trait(t.ToData())).ToList();
        traitList.Sort((x,y) => x.Data.ID.CompareTo(y.Data.ID));
        ui.InitTraitList(traitList);
    }

    public void ShowSelectTraits() {
        var ui = Managers.UI.OpenUI<TraitSelectionUI>();
        var results = ui.GetSelectedTraits();

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"선택된 특성");
        for (int i = 0; i < results.Count; i++) {
            sb.AppendLine($"{results[i].Data.DisplayText}");
        }
        Debug.Log(sb.ToString());
    }

    public void InitTraitPoint() {
        Managers.Trait.InitTraitPoint();
    }
    public void SetTraitPoint() {
        var ui = Managers.UI.OpenUI<TraitSelectionUI>();
        ui.UpdateTraitPointText(testPoint);
    }
}
