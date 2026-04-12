using System;
using System.Text;
using UnityEngine;
public class LocationTester : MonoBehaviour
{
    [SerializeField] Vector2 gridIntervals;
    [SerializeField] Vector2Int gridSize; // 가로/세로 칸 수
    [SerializeField] Color gridColor = new Color(0f, 1f, 0f, 0.3f);
    [SerializeField] Color pointColor = Color.yellow;
    [SerializeField] float pointRadius = 0.1f;
    [Space]
    [SerializeField] RectTransform testTarget;
    [SerializeField] int testIndex;
    private Vector2[] anchoredPoints;

#if UNITY_EDITOR

    public void LogGridPoints() {
        anchoredPoints = GetGridAnchoredPoints(GetComponent<RectTransform>());
        

    }
    public void SetToTestPosition() {
        if (testTarget == null) { Debug.LogError($"test target is null"); return; }
        var testPoint = anchoredPoints[testIndex];
        testTarget.anchoredPosition = testPoint;
    }


    public Vector3[] GetGridPoints() {
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
    public Vector2[] GetGridAnchoredPoints(RectTransform parentRect) {
        Vector3[] worldPoints = GetGridPoints();
        Vector2[] anchoredPoints = new Vector2[worldPoints.Length];

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
                out anchoredPoints[i]
            );
        }
        return anchoredPoints;
    }


    private void OnDrawGizmos() {
        RectTransform rectTr = GetComponent<RectTransform>();
        if (rectTr == null) return;

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

        Gizmos.color = pointColor;
        for (int y = 0; y <= gridSize.y; y++) {
            for (int x = 0; x <= gridSize.x; x++) {
                float posX = center.x - wHalfW + x * wIntervalX;
                float posY = center.y - wHalfH + y * wIntervalY;
                Gizmos.DrawSphere(new Vector3(posX, posY, center.z), wIntervalX * 0.05f);
            }
        }
    }

#endif
}