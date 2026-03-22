using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Character
{
    public CharacterData Data => data;
    public bool IsUnlocked => isUnlocked;

    [SerializeField] CharacterData data;
    [SerializeField] bool isUnlocked;
    
    public Character(CharacterData data) {
        this.data = data;
        if (data.IsDefaultCharacter) isUnlocked = true; 
    }
}
