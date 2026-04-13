using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class MarkerGridGenerator : MonoBehaviour
{
    [SerializeField] bool drawGizmos;
    [SerializeField] Vector2Int gridSize; // 가로/세로 칸 수
    [SerializeField] RectTransform targetRect;

    private List<Vector2> markerPoints = new List<Vector2>();
    [SerializeField] List<int> cachedLevels = new List<int>();

    private List<int> availablePoints = new List<int>();
    private HashSet<int> usingPoints = new HashSet<int>();

    public void InitGrid(int startPoint) {
        if (targetRect == null) { Debug.LogError("<color=red>target rect is null</color>"); return; }

        markerPoints.Clear();
        usingPoints.Clear();

        markerPoints = GetGridAnchoredPoints(targetRect);
        cachedLevels = CalculateLevels(markerPoints, startPoint);
        availablePoints = Enumerable.Range(0,markerPoints.Count).ToList();

#if UNITY_EDITOR && TEST
        LogMarkerPos();
#endif
    }

    private List<int> CalculateLevels(List<Vector2> markerPoints, int startPoint) {
        int total = (gridSize.x + 1) *(gridSize.y + 1);
        List<int> levels = new List<int>(new int[total]);
        for (int i = 0; i < total; i++) levels[i] = -1;

        Queue<int> queue = new Queue<int>();
        levels[startPoint] = 0;
        queue.Enqueue(startPoint);

        while (queue.Count > 0) {
            int current = queue.Dequeue();
            int cx = current % (gridSize.x + 1);
            int cy = current / (gridSize.x + 1);

            // 상하좌우 이웃
            (int dx, int dy)[] dirs = { (1, 0), (-1, 0), (0, 1), (0, -1) };
            foreach (var (dx, dy) in dirs) {
                int nx = cx + dx;
                int ny = cy + dy;
                if (nx < 0 || nx > gridSize.x || ny < 0 || ny > gridSize.y) continue;

                int neighborIndex = ny * (gridSize.x + 1) + nx;
                if (levels[neighborIndex] != -1) continue;

                levels[neighborIndex] = levels[current] + 1;
                queue.Enqueue(neighborIndex);
            }
        }

        return levels;
    }

    public (int markerIndex, Vector2 point)? GetRandomAvailableGrid() {
        if (availablePoints.Count == 0) return null;

        int randomPointIndex = Random.Range(0, availablePoints.Count);
        int markerIndex = availablePoints[randomPointIndex];

        if (TryGetGridOrRandom(markerIndex, out var point)) {
            return (point.index, point.point);
        }

        return null;
    }
    public bool TryGetGridOrRandom(int markerIndex, out (int index, Vector2 point) result) {
        int index = availablePoints.FindIndex(x => x == markerIndex);
        if (index != -1) {
            availablePoints.RemoveAt(index);
            usingPoints.Add(markerIndex);
            result = (markerIndex, markerPoints[markerIndex]);
            return true;
        }

        // 캐싱된 levels에서 같은 레벨 탐색
        int targetLevel = cachedLevels[markerIndex];
        List<int> sameLevelAvailable = availablePoints
            .Where(i => cachedLevels[i] == targetLevel)
            .ToList();

        if (sameLevelAvailable.Count == 0) { result = default; return false; }

        int randomMarkerIndex = sameLevelAvailable[Random.Range(0, sameLevelAvailable.Count)];
        int removeIndex = availablePoints.FindIndex(x => x == randomMarkerIndex);
        availablePoints.RemoveAt(removeIndex);
        usingPoints.Add(randomMarkerIndex);
        result = (randomMarkerIndex, markerPoints[randomMarkerIndex]);
        return true;
    }

    public void ReturnGrid(int markerIndex) {
        if (usingPoints.Remove(markerIndex)) {
            availablePoints.Add(markerIndex);
        }
    }

    private List<Vector2> GetGridAnchoredPoints(RectTransform parentRect) {
        Vector3[] worldPoints = GetGridPoints();
        List<Vector2> anchoredPoints = new List<Vector2>();

        // Canvas의 카메라를 가져옴 (Screen Space - Camera면 renderCamera, Overlay면 null)
        Canvas canvas = parentRect.GetComponentInParent<Canvas>();
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

        for (int i = 0; i < worldPoints.Length; i++) {
            // 월드 → 스크린
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, worldPoints[i]);

            // 스크린 → 로컬(앵커드)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                screenPoint,
                cam,
                out Vector2 point
            );

            anchoredPoints.Add(point);
        }
        return anchoredPoints;
    }
    private Vector3[] GetGridPoints() {
        RectTransform rectTr = GetComponent<RectTransform>();
        if (rectTr == null) return Array.Empty<Vector3>();

        Vector3[] corners = new Vector3[4];
        rectTr.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        Vector3 center = (bottomLeft + topRight) * 0.5f;

        float totalW = topRight.x - bottomLeft.x;
        float totalH = topRight.y - bottomLeft.y;

        float wIntervalX = totalW / gridSize.x;
        float wIntervalY = totalH / gridSize.y;
        float wHalfW = totalW * 0.5f;
        float wHalfH = totalH * 0.5f;

        Vector3[] points = new Vector3[(gridSize.x + 1) * (gridSize.y + 1)];
        int index = 0;
        for (int y = 0; y <= gridSize.y; y++) {
            for (int x = 0; x <= gridSize.x; x++) {
                float posX = center.x - wHalfW + x * wIntervalX;
                float posY = center.y - wHalfH + y * wIntervalY;
                points[index++] = new Vector3(posX, posY, center.z);
            }
        }
        return points;
    }


    private void Reset() {
        if (targetRect == null) targetRect = GetComponent<RectTransform>();
    }
    private void OnDrawGizmos() {
        if (!drawGizmos) return;
        RectTransform rectTr = GetComponent<RectTransform>();
        if (rectTr == null) return;

        var gridColor = Color.green;
        // RectTransform의 4개 월드 코너 가져오기
        Vector3[] corners = new Vector3[4];
        rectTr.GetWorldCorners(corners);
        // corners[0] = 좌하단, [1] = 좌상단, [2] = 우상단, [3] = 우하단

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        Vector3 center = (bottomLeft + topRight) * 0.5f;

        float totalW = topRight.x - bottomLeft.x;
        float totalH = topRight.y - bottomLeft.y;

        // interval을 RectTransform 크기 기준 비율로 변환
        float wIntervalX = totalW / gridSize.x;
        float wIntervalY = totalH / gridSize.y;
        float wHalfW = totalW * 0.5f;
        float wHalfH = totalH * 0.5f;

        Gizmos.color = gridColor;

        for (int y = 0; y <= gridSize.y; y++) {
            float posY = center.y - wHalfH + y * wIntervalY;
            Gizmos.DrawLine(
                new Vector3(center.x - wHalfW, posY, center.z),
                new Vector3(center.x + wHalfW, posY, center.z)
            );
        }

        for (int x = 0; x <= gridSize.x; x++) {
            float posX = center.x - wHalfW + x * wIntervalX;
            Gizmos.DrawLine(
                new Vector3(posX, center.y - wHalfH, center.z),
                new Vector3(posX, center.y + wHalfH, center.z)
            );
        }

        var pointColor = Color.yellow;
        Gizmos.color = pointColor;
        for (int y = 0; y <= gridSize.y; y++) {
            for (int x = 0; x <= gridSize.x; x++) {
                float posX = center.x - wHalfW + x * wIntervalX;
                float posY = center.y - wHalfH + y * wIntervalY;
                Gizmos.DrawSphere(new Vector3(posX, posY, center.z), wIntervalX * 0.05f);
            }
        }
    }

    private void LogMarkerPos() {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("point list");
        for (int i = 0; i < markerPoints.Count; i++) {
            var point = markerPoints[i];
            sb.AppendLine($"idx:{i}, point:{point}");
        }
        Debug.Log(sb.ToString());
    }

}
