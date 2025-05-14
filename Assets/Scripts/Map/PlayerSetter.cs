using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private MapCreator mapCreator;

    // 기존 맵에서 사용하는 방식
    public void SetPlayer()
    {
        Vector2 startPos = mapCreator.GetPlayerStartPosition();
        player.position = startPos;
    }

    // 보스룸 등 외부 위치 지정용
    public void SetPlayer(Vector2 customPosition)
    {
        player.position = customPosition;
    }
}
