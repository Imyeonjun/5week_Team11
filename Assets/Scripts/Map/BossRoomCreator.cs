using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossRoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject bossRoomPrefab;
    [SerializeField] private PlayerSetter playerSetter;

    [Header("보스룸 위치 설정")]
    [SerializeField] private Vector2 spawnPosition = Vector2.zero; // 원하는 위치로 조절 가능

    private GameObject bossRoomInstance;

    public void CreateBossRoom()
    {
        if (bossRoomPrefab == null)
        {
            Debug.LogError("보스룸 프리팹이 할당되지 않았습니다.");
            return;
        }

        // 보스룸 생성
        bossRoomInstance = Instantiate(bossRoomPrefab, spawnPosition, Quaternion.identity);

        // 플레이어 스폰 위치 설정
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
        playerSetter.SetPlayer(playerPos); // 위치 직접 지정

        Debug.Log("보스룸 생성 완료");
    }
}
