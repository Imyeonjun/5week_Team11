using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSetter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private MapCreator mapCreator;
    [SerializeField] private GameObject monsterPrefab;

    void Start()
    {
        StartCoroutine(SpawnMonstersWhenReady());
    }

    IEnumerator SpawnMonstersWhenReady()
    {
        while (!mapCreator.IsGenerationComplete)
            yield return null;

        // �÷��̾ ��ġ�� �� ��ǥ ���
        Vector2Int playerCell = new Vector2Int(
            Mathf.RoundToInt(player.position.x / mapCreator.roomSpacing),
            Mathf.RoundToInt(player.position.y / mapCreator.roomSpacing)
        );

        foreach (Vector2Int pos in mapCreator.RoomPositions)
        {
            if (pos == playerCell) continue; // �÷��̾ �ִ� ���� ����

            GameObject room = mapCreator.GetRoomAt(pos);
            if (room == null) continue;

            Tilemap tilemap = room.GetComponentInChildren<Tilemap>();
            Vector3 spawnPos = room.transform.position;

            if (tilemap != null)
                spawnPos = tilemap.transform.position + tilemap.localBounds.center;

            Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
        }
    }
}
