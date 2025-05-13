using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawnLinker : MonoBehaviour
{
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private MapCreator mapCreator;

    void Start()
    {
        StartCoroutine(SetupSpawnArea());
    }

    IEnumerator SetupSpawnArea()
    {
        while (!mapCreator.IsGenerationComplete)
            yield return null;

        foreach (Vector2Int pos in mapCreator.RoomPositions)
        {
            GameObject room = mapCreator.GetRoomAt(pos);
            if (room == null) continue;

            Tilemap tilemap = room.GetComponentInChildren<Tilemap>();
            if (tilemap == null) continue;

            Bounds bounds = tilemap.localBounds;
            Vector2 center = tilemap.transform.position + bounds.center;
            Vector2 size = bounds.size;

            // Rect로 변환하여 몬스터 매니저에 전달
            Rect spawnArea = new Rect(center - size / 2, size);
            monsterManager.AddSpawnArea(spawnArea);
        }
    }
}

