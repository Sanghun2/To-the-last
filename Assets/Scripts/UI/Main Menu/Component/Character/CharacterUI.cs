using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : UIBase, IPool
{
    public bool IsActive => IsOpened;

    [SerializeField] Image characterImage;
    [SerializeField] CharacterSelectionButton selectionButton;

    public void InitUI(CharacterData data) {
        characterImage.sprite = data.CharacterImage;
        selectionButton.InitCharacter(data.CharacterID);
    }


    #region Pool

    public void Activate() {
        OpenUI();
    }
    public void Init() {
        if (IsInit) return;

        InitUI();

        _isInit = true;
    }
    public void Return() {
        CloseUI();
    }

    #endregion
}
