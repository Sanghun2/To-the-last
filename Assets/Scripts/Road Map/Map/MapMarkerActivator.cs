using System;
using System.Text;
using BilliotGames;
using UnityEngine;

public sealed class MapMarkerActivator : TypeRegistry<MarkerUIBase, MarkerUIContainerBase>
{
    public MapMarkerActivator() {
        InitMarkerContainers();
    }

    private void InitMarkerContainers() {
        MarkerUIContainerBase[] markerUIContainers = GameObject.FindObjectsByType<MarkerUIContainerBase>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < markerUIContainers.Length; i++) {
            var container = markerUIContainers[i];
            Register(container.MarkerUIType, container);
        }

#if UNITY_EDITOR && TEST
        StringBuilder sb = new StringBuilder().AppendLine("등록된 Map Marker Containers 목록");
        foreach (var container in markerUIContainers) {
            sb.AppendLine($"{container.MarkerUIType.Name} - {container.GetType()}");
        }
        Debug.Log(sb.ToString());
#endif
    }
}
