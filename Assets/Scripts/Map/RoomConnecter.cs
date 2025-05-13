using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 방과 방을 연결하고 복도 프리팹을 배치하는 클래스
public class RoomConnecter : MonoBehaviour
{
    // 두 방의 위치와 프리팹을 받아 복도를 생성하고 타일 제거까지 수행
    public static void TryCreateCorridor(Vector2 start, Vector2 end, RoomTemplate roomA, RoomTemplate roomB, GameObject corridorHorizontalPrefab, GameObject corridorVerticalPrefab)
    {
        if (roomA == null || roomB == null) return; // 방이 null이면 연결 생략

        Vector2 dir = end - start; // 시작과 끝 위치를 기준으로 방향 벡터 계산
        Vector2 mid = (start + end) / 2f; // 복도 위치는 두 방 사이의 중간 지점

        GameObject prefab;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) // 가로 방향 복도
        {
            mid.x = Mathf.Round(mid.x); // 격자 정렬
            mid.y = Mathf.Round(mid.y);
            prefab = corridorHorizontalPrefab;
        }
        else // 세로 방향 복도
        {
            mid.x = Mathf.Round(mid.x);
            mid.y = Mathf.Round(mid.y);
            prefab = corridorVerticalPrefab;
        }

        Vector2Int dirInt = Vector2Int.RoundToInt(dir.normalized); // 방향 정규화

        // 연결 조건 확인: 양쪽 문이 열려 있어야 복도 생성
        if ((dirInt.x == 1 && roomA.openRight && roomB.openLeft) ||
            (dirInt.x == -1 && roomA.openLeft && roomB.openRight) ||
            (dirInt.y == 1 && roomA.openTop && roomB.openBottom) ||
            (dirInt.y == -1 && roomA.openBottom && roomB.openTop))
        {
            GameObject corridor = Instantiate(prefab, mid, Quaternion.identity); // 복도 생성
            roomA.ClearEntranceTiles(dir);    // 방 A에서 입구 타일 제거
            roomB.ClearEntranceTiles(-dir);   // 방 B에서 반대 방향 입구 타일 제거
        }
    }
}

// RoomTemplate 확장 기능 정의
public static class RoomTemplateExtensions
{
    // 주어진 방향으로 입구 타일을 제거
    public static void ClearEntranceTiles(this RoomTemplate room, Vector2 direction)
    {
        const int corridorWidth = 3;            // 복도 폭
        const int corridorOffsetStart = 2;      // 방 중심에서 입구까지 거리

        TilemapCollider2D wallCollider = room.GetComponentInChildren<TilemapCollider2D>();
        if (wallCollider == null) return;

        Tilemap tilemap = wallCollider.GetComponent<Tilemap>();
        if (tilemap == null) return;

        Vector2Int dir = Vector2Int.RoundToInt(direction.normalized);
        Vector3Int centerCell = tilemap.WorldToCell(room.transform.position); // 방 위치를 셀 좌표로 변환

        if (dir.x != 0) // 가로 방향
        {
            int xTarget = centerCell.x + (dir.x > 0 ? 6 : 0); // 오른쪽 또는 왼쪽 끝
            for (int y = centerCell.y + corridorOffsetStart; y <= centerCell.y + corridorOffsetStart + (corridorWidth - 1); y++)
            {
                TryClear(new Vector3Int(xTarget, y, 0), tilemap); // 해당 셀의 타일 제거
            }
        }
        else if (dir.y != 0) // 세로 방향
        {
            int yTarget = centerCell.y + (dir.y > 0 ? 7 : 0); // 위쪽 또는 아래쪽 끝
            for (int x = centerCell.x + corridorOffsetStart; x <= centerCell.x + corridorOffsetStart + (corridorWidth - 1); x++)
            {
                TryClear(new Vector3Int(x, yTarget, 0), tilemap); // 해당 셀의 타일 제거
            }
        }
    }

    // 특정 셀의 타일이 존재하면 제거
    private static void TryClear(Vector3Int cell, Tilemap tilemap)
    {
        if (tilemap.HasTile(cell))
        {
            tilemap.SetTile(cell, null);
        }
    }
}
