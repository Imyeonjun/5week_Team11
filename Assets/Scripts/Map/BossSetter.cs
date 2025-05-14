using System.Collections.Generic;
using UnityEngine;

public class BossSetter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private MonsterManager monsterManager;
    [SerializeField] private List<GameObject> bossPrefabs; // ���� ������ ����Ʈ

    public MonsterController activeBoss { get; private set; } // ������ ���� 1���� ����

    /// <summary>
    /// ������ ��ġ�� ������ �����մϴ�.
    /// </summary>
    public void SpawnBoss(Vector2 spawnPosition)
    {
        if (bossPrefabs == null || bossPrefabs.Count == 0)
        {
            Debug.LogError("BossSetter: ���� ������ ����Ʈ�� ��� �ֽ��ϴ�!");
            return;
        }

        GameObject bossPrefab = bossPrefabs[Random.Range(0, bossPrefabs.Count)];
        GameObject spawnedBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        MonsterController bossController = spawnedBoss.GetComponent<MonsterController>();
        if (bossController == null)
        {
            Debug.LogError("BossSetter: �����տ� MonsterController ������Ʈ�� �����ϴ�!");
            return;
        }

        if (player != null)
        {
            bossController.Init(monsterManager, player);
        }

        activeBoss = bossController;
    }
}
