using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossRoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject bossRoomPrefab;
    [SerializeField] private PlayerSetter playerSetter;

    [Header("������ ��ġ ����")]
    [SerializeField] private Vector2 spawnPosition = Vector2.zero; // ���ϴ� ��ġ�� ���� ����

    private GameObject bossRoomInstance;

    public void CreateBossRoom()
    {
        if (bossRoomPrefab == null)
        {
            Debug.LogError("������ �������� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // ������ ����
        bossRoomInstance = Instantiate(bossRoomPrefab, spawnPosition, Quaternion.identity);

        // �÷��̾� ���� ��ġ ����
        Tilemap tilemap = bossRoomInstance.GetComponentInChildren<Tilemap>();
        Vector2 playerSpawnPos;

        if (tilemap != null)
        {
            playerSpawnPos = tilemap.transform.position + tilemap.localBounds.center;
        }
        else
        {
            playerSpawnPos = bossRoomInstance.transform.position;
        }

        Vector2 playerPos = tilemap.transform.position + tilemap.localBounds.center;
        playerSetter.SetPlayer(playerPos); // ��ġ ���� ����

        Debug.Log("������ ���� �Ϸ�");
    }
}
