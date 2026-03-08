using BilliotGames;
using UnityEngine;

public class Effect
{
    public enum OperatorType
    {
        Add,
        Multiply,
    }
    public enum ValueType
    {
        Scala,
        Percent,
    }
}


public abstract class EffectSD : SDBase
{  
    
    //public enum TargetType {
    //    None,

    //    // 기본 stat
    //    Hp,
    //    Hunger,
    //    Thirst,
    //    Mental,
    //    Temperture,

    //    // 확장 stat
    //    Strength,
    //    Agility,
    //    Focus,

    //    // 전투
    //    Attack, // 무기의 공격력
    //    Defense,

    //    Damage,  // 데미지 적용을 위해 적용되는 데미지
    //    Dodge, 
    //    Charge,
    //}
    //public enum ApplyType {
    //    Instant,
    //    Delay,
    //}

    //public TargetType TargetType_ => targetType;
    //public ApplyType ApplyType_ => applyType;

    //[SerializeField] TargetType targetType;
    //[SerializeField] protected ApplyType applyType;

    public abstract void ApplyEffect(Entity caster, Entity target);
    protected bool IsValid(Entity caster, Entity target) {
        if (caster == null || target == null) { Debug.Log($"entity null. caster null? {caster == null}, target null? {target == null}"); return false; }

        return true;
    }

    protected virtual void OnValidate() {
        RenameAsset(ID, suffix:"_EffectSD");
    }
}
