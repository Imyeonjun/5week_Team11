using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSetter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private MapCreator mapCreator;
    [SerializeField] private MonsterManager monsterManager;

    [SerializeField] private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트

    private List<MonsterController> activeEnemies = new List<MonsterController>(); // 현재 활성화된 적들

    public void Init()
    {
        StartCoroutine(SpawnMonstersWhenReady()); // 코루틴을 이용해 몬스터를 생성하는 함수 호출
    }

    IEnumerator SpawnMonstersWhenReady() // 맵 생성이 완료된 후 몬스터를 배치하는 코루틴
    {
        while (!mapCreator.IsGenerationComplete) // 맵 생성이 끝날 때까지 대기
            yield return null; // 다음 프레임까지 대기

        // 플레이어가 위치한 방 좌표 계산
        Vector2Int playerCell = new Vector2Int( // 플레이어의 현재 방 위치를 계산하여 저장
            Mathf.RoundToInt(player.position.x / mapCreator.roomSpacing), // X 좌표 기준 방 인덱스
            Mathf.RoundToInt(player.position.y / mapCreator.roomSpacing)  // Y 좌표 기준 방 인덱스
        );

        foreach (Vector2Int pos in mapCreator.RoomPositions) // 생성된 모든 방 위치를 순회
        {
            if (pos == playerCell) continue; // 플레이어가 위치한 방은 건너뜀

            GameObject room = mapCreator.GetRoomAt(pos); // 현재 방의 GameObject를 가져옴
            if (room == null) continue; // 방이 존재하지 않으면 건너뜀

            Tilemap tilemap = room.GetComponentInChildren<Tilemap>(); // 방 안에 있는 타일맵 컴포넌트를 가져옴
            Vector3 spawnPos = room.transform.position; // 몬스터 기본 스폰 위치는 방의 월드 위치

            if (tilemap != null) // 타일맵이 존재하면
                spawnPos = tilemap.transform.position + tilemap.localBounds.center; // 타일맵 중앙 위치로 스폰 위치 재조정

            GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]; // 랜덤한 적 프리팹 선택

            GameObject spawnedEnemy = Instantiate(randomPrefab, spawnPos, Quaternion.identity); // 몬스터 프리팹을 해당 위치에 생성

            MonsterController monsterController = spawnedEnemy.GetComponent<MonsterController>();

            if (player != null)
            {
                monsterController.Init(monsterManager, player.transform);
            }

            activeEnemies.Add(monsterController);
        }
    }
}
