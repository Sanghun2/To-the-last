using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    public string CharacterID => id;
    public string Name => name;
    public string Description => description;
    public Sprite CharacterImage => characterImage;
    public IReadOnlyList<string> Features => features;
    public bool IsDefaultCharacter => isDefaultCharacter;

    private string id;
    private string name;
    private string description;
    private Sprite characterImage;
    private string[] features;
    private bool isDefaultCharacter;

    public CharacterData(
        string id, 
        string displayText, 
        string description, 
        Sprite image, 
        string[] features,
        bool isDefaultCharacter) {
        this.id = id;
        this.name = displayText;
        this.description = description;
        this.characterImage = image;
        this.features = features;
        this.isDefaultCharacter = isDefaultCharacter;
    }
}
