using System;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : UIBase
{
    public TurnUI TurnUI
    {
        get
        {
            if (_turnUI == null) {
                _turnUI = FindAnyObjectByType<TurnUI>(FindObjectsInactive.Include);
            }

            return _turnUI;
        }
    }

    [SerializeField] Image backgroundImage;
    [SerializeField] EntityUI playerUI;
    [SerializeField] EntityUI enemyUI;
    [SerializeField] TurnUI _turnUI;

    public override void InitUI() {
        if (IsInit) return;

        TurnUI.InitUI();

        _isInit = true;
    }

    internal void InitUI(Entity player, Entity enemy) {
        playerUI.InitEntity(player);
        enemyUI.InitEntity(enemy);
    }
}
