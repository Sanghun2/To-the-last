using System;
using UnityEngine;

public sealed class NPCTester : MonoBehaviour
{
    [SerializeField] NPCSDBase testNPCSD;

#if UNITY_EDITOR
    public void Test_ActiveNPC() {
        Managers.NPC.TryActivateNPC(testNPCSD, out NPCBase targetNPC);
        var tradeNPC = targetNPC as TradeNPC;
        var locationData = new LocationData(
            testNPCSD.ID,
            testNPCSD.CategoryID,
            testNPCSD.DisplayName,
            testNPCSD.Description,
            tradeNPC.AnchoredPosition,
            testNPCSD.MainImage,
            testNPCSD.IconImage
            );
        Managers.Location.TryRegisterLocation(new TradeNPCLocation(tradeNPC, locationData));
    }
#endif
}
