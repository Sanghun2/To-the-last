using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationSD", menuName = "Scriptable Objects/LocationSD")]
public class LocationSD : IconSDBase
{
    public IReadOnlyList<LocationData> LocationDataList => locationDataList;

    [SerializeField][TextArea(1, 20)] string storyDescription;
    [SerializeField] List<LocationData> locationDataList = new List<LocationData>();

    private void OnValidate() {
        RenameAsset(ID, suffix:"_LocationSD");

        for (int i = 0; i < locationDataList.Count; i++) {
            var locationData = locationDataList[i];
            if (locationData.Level <= 0) {
                locationData.SetLevel(1);
            }
        }
    }
}

[Serializable]
public class LocationData
{
    public int Level => level;
    public int Weight => weight;

    [SerializeField] int level;
    [SerializeField] int weight;
    [SerializeField] EncounterSD encounterSD;

    public void SetLevel(int level) {
        this.level = level;
    }
}
