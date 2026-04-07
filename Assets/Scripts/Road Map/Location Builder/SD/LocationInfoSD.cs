using BilliotGames;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationIconSD", menuName = "Scriptable Objects/LocationIconSD")]
public class LocationInfoSD : ImageSDBase
{
    public Sprite IconImage => iconImage;
    public string StoryDescription => storyDescription;

    [SerializeField][TextArea(1, 50)] string storyDescription;
    [SerializeField] Sprite iconImage;
}
