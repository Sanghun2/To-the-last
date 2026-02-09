using BilliotGames;
using UnityEngine;

public class ItemUIContainer : ListContainerBase<SimpleItemUI>
{
    public override SimpleItemUI GetObj() {
        for (int i = 0; i < contentList.Count; i++) {
            var content = contentList[i];
            if (!content.IsActive) {
                return content;
            }
        }

        return CreateObj();
    }

    public override bool TryGetObj(int index, out SimpleItemUI content) {
        if (0 <= index && index < ContentCount) {
            content = contentList[index];
            return true;
        }

        content = null;
        return false;
    }
}
