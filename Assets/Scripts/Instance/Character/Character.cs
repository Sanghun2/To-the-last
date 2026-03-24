using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Character
{
    public CharacterData Data => data;
    public bool Locked => locked;

    [SerializeField] CharacterData data;
    [SerializeField] bool locked;
    
    public Character(CharacterData data) {
        this.data = data;
        locked = !data.IsDefaultCharacter;
    }
}
