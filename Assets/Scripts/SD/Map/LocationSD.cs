using System;
using System.Collections.Generic;
using BilliotGames;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "LocationSD", menuName = "Scriptable Objects/LocationSD")]
public class LocationSD : IconSDBase
{
    public IReadOnlyList<ExplorationEvent> LocationEventList => locationEventList;
    public string StoryDescription => storyDescription;
    public Sprite MainImage => mainImage;
    public Vector2 AnchoredPosition => anchoredPosition;
    public float Distance => distance;

    [SerializeField] Sprite mainImage;
    [SerializeField][TextArea(1, 20)] string storyDescription;
    [SerializeField] Vector2 anchoredPosition;
    [SerializeField] List<ExplorationEvent> locationEventList = new List<ExplorationEvent>();
    private float distance;

    private void OnValidate() {
        RenameAsset(ID, suffix:"_LocationSD");

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
