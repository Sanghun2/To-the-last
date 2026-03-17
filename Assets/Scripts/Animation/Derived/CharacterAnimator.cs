using System;
using System.Data.Common;
using UnityEngine;

public class CharacterAnimator : AnimatorBase
{
    private string targetEntityID => entityUI.Entity?.EntityID ?? string.Empty;

    [SerializeField] Animator anim;
    private EntityUI entityUI;
    private bool isInit;
    private Action onApplyTime;

    public void Init() {
        if (isInit) return;

        var entityUI = GetComponentInParent<EntityUI>();
        if (entityUI != null) {
            this.entityUI = entityUI;
        }
        else {
            Debug.LogError($"entity ui is null");
        }

        isInit = true;
    }

    public override void Animate(Define.ActionAnimationType type, Action onApplyTime = null) {
        Init();
        this.onApplyTime = onApplyTime;

        ShowSprite(type);

        var parameter = ConvertType(type);
        anim.SetTrigger(parameter);
    }


    private void Awake() {
        Init();
        //
        //
    }

    private void ShowSprite(Define.ActionAnimationType type) {
        if (Managers.SD.TryGetSD(targetEntityID, out AnimationSpriteSD spriteSD)) {
            if (spriteSD.TryGetSprite(type, out Sprite sprite) == false) { Debug.LogError($"<color=red>no sprite of type ({type}) exist</color>"); return; }

            entityUI.SetImage(sprite);
        }
        else {
            Debug.LogError($"<color=red>({targetEntityID}) animatio sprite SD not exist</color>");
        }
    }
    private string ConvertType(Define.ActionAnimationType type) {

        switch (type) {
            case Define.ActionAnimationType.Default:
                return "Idle";
            case Define.ActionAnimationType.SwingAttack:
                return "Swing";
            case Define.ActionAnimationType.StabAttack:
                return "Stab";
            default:
                Debug.Log($"<color=orange>not defined type ({type})</color>");
                return "Idle";
        }
    }

    private void InvokeAction() {
        onApplyTime?.Invoke();
    }
}
