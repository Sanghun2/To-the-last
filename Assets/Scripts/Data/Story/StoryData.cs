using UnityEngine;

public class StoryData
{
    public string ID;

    [SerializeField] string id;

    public StoryData(string id) {
        this.id = id;
    }
}
