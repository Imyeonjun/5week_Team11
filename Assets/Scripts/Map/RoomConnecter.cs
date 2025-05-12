using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomConnecter : MonoBehaviour
{
    public static void TryCreateCorridor(Vector2 start, Vector2 end, RoomTemplate roomA, RoomTemplate roomB, GameObject corridorHorizontalPrefab, GameObject corridorVerticalPrefab)
    {
        if (roomA == null || roomB == null) return;

        Vector2 dir = end - start;
        Vector2 mid = (start + end) / 2f;

        GameObject prefab;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            mid.x = Mathf.Round(mid.x);
            mid.y = Mathf.Round(mid.y);
            prefab = corridorHorizontalPrefab;
        }
        else
        {
            mid.x = Mathf.Round(mid.x);
            mid.y = Mathf.Round(mid.y);
            prefab = corridorVerticalPrefab;
        }

        // 연결 조건 확인 후 복도 생성 및 타일 제거
        Vector2Int dirInt = Vector2Int.RoundToInt(dir.normalized);

        if ((dirInt.x == 1 && roomA.openRight && roomB.openLeft) ||
            (dirInt.x == -1 && roomA.openLeft && roomB.openRight) ||
            (dirInt.y == 1 && roomA.openTop && roomB.openBottom) ||
            (dirInt.y == -1 && roomA.openBottom && roomB.openTop))
        {
            GameObject corridor = Instantiate(prefab, mid, Quaternion.identity);
            roomA.ClearEntranceTiles(dir);
            roomB.ClearEntranceTiles(-dir);
        }
    }
}


public static class RoomTemplateExtensions
{
    public static void ClearEntranceTiles(this RoomTemplate room, Vector2 direction)
    {
        const int corridorWidth = 3;
        const int corridorOffsetStart = 2;

        TilemapCollider2D wallCollider = room.GetComponentInChildren<TilemapCollider2D>();
        if (wallCollider == null) return;

        Tilemap tilemap = wallCollider.GetComponent<Tilemap>();
        if (tilemap == null) return;

        Vector2Int dir = Vector2Int.RoundToInt(direction.normalized);
        Vector3Int centerCell = tilemap.WorldToCell(room.transform.position);

        if (dir.x != 0)
        {
            int xTarget = centerCell.x + (dir.x > 0 ? 6 : 0);
            for (int y = centerCell.y + corridorOffsetStart; y <= centerCell.y + corridorOffsetStart + (corridorWidth - 1); y++)
            {
                TryClear(new Vector3Int(xTarget, y, 0), tilemap);
            }
        }
        else if (dir.y != 0)
        {
            int yTarget = centerCell.y + (dir.y > 0 ? 7 : 0);
            for (int x = centerCell.x + corridorOffsetStart; x <= centerCell.x + corridorOffsetStart + (corridorWidth - 1); x++)
            {
                TryClear(new Vector3Int(x, yTarget, 0), tilemap);
            }
        }
    }

    private static void TryClear(Vector3Int cell, Tilemap tilemap)
    {
        if (tilemap.HasTile(cell))
        {
            tilemap.SetTile(cell, null);
        }
    }
}

