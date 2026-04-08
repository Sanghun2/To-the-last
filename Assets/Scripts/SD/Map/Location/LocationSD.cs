using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "LocationSD", menuName = "Scriptable Objects/LocationSD")]
public class LocationSD : ImageSDBase, IEquatable<LocationSD>
{
    public string StoryDescription => locationInfo.StoryDescription;
    public Sprite MainImage => locationInfo.Image;
    public Sprite IconImage => locationInfo.IconImage;

    public IReadOnlyList<EncounterInfo> LocationEventList => locationEventList;
    public Vector2 AnchoredPosition => anchoredPosition;
    public float Distance => distance;
    public LocationSD NextLocation => nextLocation;


    [SerializeField] LocationInfoSD locationInfo;
    [SerializeField][TextArea(1, 20)] string storyDescription;
    [SerializeField] Vector2 anchoredPosition;
    [SerializeField] List<EncounterInfo> locationEventList = new List<EncounterInfo>();
    [SerializeField] LocationSD nextLocation;
    private float distance;

    protected override void OnValidate() {
        base.OnValidate();

        for (int i = 0; i < locationEventList.Count; i++) {
            var locationData = locationEventList[i];
            if (locationData.Level <= 0) {
                locationData.SetLevel(1);
            }
        }
    }

    internal void SetAnchoredPosition(Vector2 targetPos) {
#if UNITY_EDITOR
        Undo.RecordObject(this, "Change Location Position");
#endif

        anchoredPosition = targetPos;

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
    }

    internal void SetDistance(float dist) {
        distance = dist;
    }

    public bool Equals(LocationSD other) {
        if (other == null) {
            Debug.Log($"other is null");
            return false;
        }

        return ID.Equals(other.ID);
    }
    public override bool Equals(object obj){
        if (obj == null) {
            Debug.Log($"other is null");
            return false;
        }
        if (obj is LocationSD other) {
            return Equals(other);
        }
        else {
            Debug.LogError($"wrong type");
            return false;
        }
    }
    public override int GetHashCode() {
        return base.GetHashCode();
    }
}

[Serializable]
public class EncounterInfo
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
