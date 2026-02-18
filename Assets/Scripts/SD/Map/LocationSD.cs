using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationSD", menuName = "Scriptable Objects/LocationSD")]
public class LocationSD : IconSDBase
{
    public IReadOnlyList<ExplorationEvent> LocationEventList => locationEventList;
    public string StoryDescription => storyDescription;
    public Sprite MainImage => mainImage;

    [SerializeField] Sprite mainImage;
    [SerializeField][TextArea(1, 20)] string storyDescription;
    [SerializeField] List<ExplorationEvent> locationEventList = new List<ExplorationEvent>();

    private void OnValidate() {
        RenameAsset(ID, suffix:"_LocationSD");

        for (int i = 0; i < locationEventList.Count; i++) {
            var locationData = locationEventList[i];
            if (locationData.Level <= 0) {
                locationData.SetLevel(1);
            }
        }
    }
}

[Serializable]
public class ExplorationEvent
{
    public int Level => level;
    public int Weight => weight;
    public EncounterSD EncounterSD => encounterSD;

    [SerializeField] int level;
    [SerializeField] int weight;
    [SerializeField] EncounterSD encounterSD;

    public void SetLevel(int level) {
        this.level = level;
    }
}
