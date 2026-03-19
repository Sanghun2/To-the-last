using UnityEngine;

public class Story 
{
    [SerializeField] StoryData storyData;

    public Story(StoryData data) {
        storyData = data;
    }
}
