using System;
using UnityEngine;

public sealed class NPCTester : MonoBehaviour
{
    [SerializeField] NPCSDBase testNPCSD;

#if UNITY_EDITOR
    public void Test_ActiveNPC() {
        Managers.NPC.TryActivateNPC(testNPCSD);
    }
#endif
}
