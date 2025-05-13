using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MapCreator mapCreator; // �� ������ ����
    [SerializeField] private GameObject monsterPrefab; // ���� ������

    void Start()
    {
        StartCoroutine(SpawnMonstersWhenReady());
    }

    IEnumerator SpawnMonstersWhenReady()
    {
        // �� ���� �Ϸ�� ������ ���
        while (!mapCreator.IsGenerationComplete)
            yield return null;

        foreach (Vector2Int pos in mapCreator.RoomPositions)
        {
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
