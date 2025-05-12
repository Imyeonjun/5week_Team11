using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// ��� ���� �����ϰ� ���� �������� ��ġ�ϴ� Ŭ����
public class RoomConnecter : MonoBehaviour
{
    // �� ���� ��ġ�� �������� �޾� ������ �����ϰ� Ÿ�� ���ű��� ����
    public static void TryCreateCorridor(Vector2 start, Vector2 end, RoomTemplate roomA, RoomTemplate roomB, GameObject corridorHorizontalPrefab, GameObject corridorVerticalPrefab)
    {
        if (roomA == null || roomB == null) return; // ���� null�̸� ���� ����

        Vector2 dir = end - start; // ���۰� �� ��ġ�� �������� ���� ���� ���
        Vector2 mid = (start + end) / 2f; // ���� ��ġ�� �� �� ������ �߰� ����

        GameObject prefab;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) // ���� ���� ����
        {
            mid.x = Mathf.Round(mid.x); // ���� ����
            mid.y = Mathf.Round(mid.y);
            prefab = corridorHorizontalPrefab;
        }
        else // ���� ���� ����
        {
            mid.x = Mathf.Round(mid.x);
            mid.y = Mathf.Round(mid.y);
            prefab = corridorVerticalPrefab;
        }

        Vector2Int dirInt = Vector2Int.RoundToInt(dir.normalized); // ���� ����ȭ

        // ���� ���� Ȯ��: ���� ���� ���� �־�� ���� ����
        if ((dirInt.x == 1 && roomA.openRight && roomB.openLeft) ||
            (dirInt.x == -1 && roomA.openLeft && roomB.openRight) ||
            (dirInt.y == 1 && roomA.openTop && roomB.openBottom) ||
            (dirInt.y == -1 && roomA.openBottom && roomB.openTop))
        {
            GameObject corridor = Instantiate(prefab, mid, Quaternion.identity); // ���� ����
            roomA.ClearEntranceTiles(dir);    // �� A���� �Ա� Ÿ�� ����
            roomB.ClearEntranceTiles(-dir);   // �� B���� �ݴ� ���� �Ա� Ÿ�� ����
        }
    }
}

// RoomTemplate Ȯ�� ��� ����
public static class RoomTemplateExtensions
{
    // �־��� �������� �Ա� Ÿ���� ����
    public static void ClearEntranceTiles(this RoomTemplate room, Vector2 direction)
    {
        const int corridorWidth = 3;            // ���� ��
        const int corridorOffsetStart = 2;      // �� �߽ɿ��� �Ա����� �Ÿ�

        TilemapCollider2D wallCollider = room.GetComponentInChildren<TilemapCollider2D>();
        if (wallCollider == null) return;

        Tilemap tilemap = wallCollider.GetComponent<Tilemap>();
        if (tilemap == null) return;

        Vector2Int dir = Vector2Int.RoundToInt(direction.normalized);
        Vector3Int centerCell = tilemap.WorldToCell(room.transform.position); // �� ��ġ�� �� ��ǥ�� ��ȯ

        if (dir.x != 0) // ���� ����
        {
            int xTarget = centerCell.x + (dir.x > 0 ? 6 : 0); // ������ �Ǵ� ���� ��
            for (int y = centerCell.y + corridorOffsetStart; y <= centerCell.y + corridorOffsetStart + (corridorWidth - 1); y++)
            {
                TryClear(new Vector3Int(xTarget, y, 0), tilemap); // �ش� ���� Ÿ�� ����
            }
        }
        else if (dir.y != 0) // ���� ����
        {
            int yTarget = centerCell.y + (dir.y > 0 ? 7 : 0); // ���� �Ǵ� �Ʒ��� ��
            for (int x = centerCell.x + corridorOffsetStart; x <= centerCell.x + corridorOffsetStart + (corridorWidth - 1); x++)
            {
                TryClear(new Vector3Int(x, yTarget, 0), tilemap); // �ش� ���� Ÿ�� ����
            }
        }
    }

    // Ư�� ���� Ÿ���� �����ϸ� ����
    private static void TryClear(Vector3Int cell, Tilemap tilemap)
    {
        if (tilemap.HasTile(cell))
        {
            tilemap.SetTile(cell, null);
        }
    }
}
