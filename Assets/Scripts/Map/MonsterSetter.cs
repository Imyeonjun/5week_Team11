using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSetter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private MapCreator mapCreator;
    [SerializeField] private MonsterManager monsterManager;

    [SerializeField] private List<GameObject> enemyPrefabs; // ������ �� ������ ����Ʈ

    private List<MonsterController> activeEnemies = new List<MonsterController>(); // ���� Ȱ��ȭ�� ����

    public void Init()
    {
        StartCoroutine(SpawnMonstersWhenReady()); // �ڷ�ƾ�� �̿��� ���͸� �����ϴ� �Լ� ȣ��
    }

    IEnumerator SpawnMonstersWhenReady() // �� ������ �Ϸ�� �� ���͸� ��ġ�ϴ� �ڷ�ƾ
    {
        while (!mapCreator.IsGenerationComplete) // �� ������ ���� ������ ���
            yield return null; // ���� �����ӱ��� ���

        // �÷��̾ ��ġ�� �� ��ǥ ���
        Vector2Int playerCell = new Vector2Int( // �÷��̾��� ���� �� ��ġ�� ����Ͽ� ����
            Mathf.RoundToInt(player.position.x / mapCreator.roomSpacing), // X ��ǥ ���� �� �ε���
            Mathf.RoundToInt(player.position.y / mapCreator.roomSpacing)  // Y ��ǥ ���� �� �ε���
        );

        foreach (Vector2Int pos in mapCreator.RoomPositions) // ������ ��� �� ��ġ�� ��ȸ
        {
            if (pos == playerCell) continue; // �÷��̾ ��ġ�� ���� �ǳʶ�

            GameObject room = mapCreator.GetRoomAt(pos); // ���� ���� GameObject�� ������
            if (room == null) continue; // ���� �������� ������ �ǳʶ�

            Tilemap tilemap = room.GetComponentInChildren<Tilemap>(); // �� �ȿ� �ִ� Ÿ�ϸ� ������Ʈ�� ������
            Vector3 spawnPos = room.transform.position; // ���� �⺻ ���� ��ġ�� ���� ���� ��ġ

            if (tilemap != null) // Ÿ�ϸ��� �����ϸ�
                spawnPos = tilemap.transform.position + tilemap.localBounds.center; // Ÿ�ϸ� �߾� ��ġ�� ���� ��ġ ������

            GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]; // ������ �� ������ ����

            GameObject spawnedEnemy = Instantiate(randomPrefab, spawnPos, Quaternion.identity); // ���� �������� �ش� ��ġ�� ����

            MonsterController monsterController = spawnedEnemy.GetComponent<MonsterController>();

            if (player != null)
            {
                monsterController.Init(monsterManager, player.transform);
            }

            activeEnemies.Add(monsterController);
        }
    }
}
