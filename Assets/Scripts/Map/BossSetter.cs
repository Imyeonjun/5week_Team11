using System.Collections.Generic;
using UnityEngine;

public class BossSetter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private List<GameObject> bossPrefabs; // 보스 프리팹 리스트

    public MonsterController activeBoss { get; private set; } // 생성된 보스 1마리 추적

    /// <summary>
    /// 지정된 위치에 보스를 생성합니다.
    /// </summary>
    public void SpawnBoss(Vector2 spawnPosition)
    {
        if (bossPrefabs == null || bossPrefabs.Count == 0)
        {
            Debug.LogError("BossSetter: 보스 프리팹 리스트가 비어 있습니다!");
            return;
        }

        GameObject bossPrefab = bossPrefabs[Random.Range(0, bossPrefabs.Count)];
        GameObject spawnedBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        MonsterController bossController = spawnedBoss.GetComponent<MonsterController>();
        if (bossController == null)
        {
            Debug.LogError("BossSetter: 프리팹에 MonsterController 컴포넌트가 없습니다!");
            return;
        }

        if (player != null)
        {
            bossController.Init(monsterManager, player);
        }

        activeBoss = bossController;
    }
}
