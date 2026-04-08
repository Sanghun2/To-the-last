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

    public IReadOnlyList<EssentialEncounterInfo> EssentialLocationEventList => essentialLocationEventList;
    public Vector2 AnchoredPosition => anchoredPosition;
    public float Distance => distance;
    public LocationSD[] NextLocation => nextLocations;

    public string CategoryID => categoryList.Count > 0 ? categoryList[0].ID : null;

    [SerializeField] LocationInfoSD locationInfo;
    [SerializeField][TextArea(1, 20)] string storyDescription;
    [SerializeField] Vector2 anchoredPosition;
    [SerializeField] List<EssentialEncounterInfo> essentialLocationEventList = new List<EssentialEncounterInfo>();
    [SerializeField] LocationSD[] nextLocations;
    private float distance;

    protected override void OnValidate() {
        base.OnValidate();

        for (int i = 0; i < essentialLocationEventList.Count; i++) {
            var locationData = essentialLocationEventList[i];
            if (locationData.Index <= 0) {
                locationData.SetIndex(1);
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


