using UnityEngine;

[CreateAssetMenu(fileName = "NPCSD", menuName = "Scriptable Objects/NPC/NPCSD")]
public class NPCSD : NPCSDBase
{
    private void OnValidate() {
        RenameAsset(ID, suffix:"_NPCSD");
    }
}
