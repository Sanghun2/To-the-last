using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AnimationSpriteSD", menuName = "Scriptable Objects/AnimationSpriteSD")]
public class AnimationSpriteSD : SDBase, ISerializationCallbackReceiver
{
    public string EntityID => targetEntitySD.ID;

    [SerializeField] EntitySDBase targetEntitySD;
    [SerializeField] SpriteData[] spriteDataArr = new SpriteData[] { new SpriteData() };
    private Dictionary<Define.ActionAnimationType, Sprite> spriteDict = new();

    public bool TryGetSprite(Define.ActionAnimationType type, out Sprite sprite) {
        if (spriteDict.TryGetValue(type,out sprite)) {
            return true;
        }

        return false;
    }

    protected virtual void OnValidate() {
        CheckAssetName();
        CheckSpriteDataValidation();
    }

    private void CheckSpriteDataValidation() {
        if (targetEntitySD == null) return;
        for (int i = 0; i < spriteDataArr.Length; i++) {
            var data = spriteDataArr[i];
            if (data.Sprite == null) {
                if (data.Type == Define.ActionAnimationType.Default) {
                    data.SetSprite(targetEntitySD.Image);
                }
                else {
                    Debug.LogError($"<color=orange>({EntityID})</color> sprite SD <color=orange>({data.Type})</color> sprite is required");
                }
            }
        }
    }
    private void CheckAssetName() {
        if (targetEntitySD == null) return;

        string targetID = targetEntitySD.ID;
        if (targetID.Equals(id)) return;

        id = targetID;
        RenameAsset(ID, suffix: "_AnimationSpriteSD");       
    }


    public void OnBeforeSerialize() {
        //Debug.Log("before serialize");
    }
    public void OnAfterDeserialize() {
        spriteDict.Clear();
        for (int i = 0; i < spriteDataArr.Length; i++) {
            var data = spriteDataArr[i];
            spriteDict.Add(data.Type, data.Sprite);
        }
    }
}

[Serializable]
public class SpriteData
{
    public Define.ActionAnimationType Type => type; 
    public Sprite Sprite => sprite;

    [SerializeField] Define.ActionAnimationType type;
    [SerializeField] Sprite sprite;

    public void SetSprite(Sprite sprite) {
        this.sprite = sprite;
    }
}
