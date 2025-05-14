using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private Transform player; // 이미 존재하는 플레이어
    [SerializeField] private MapCreator mapCreator; // MapCreator 참조
    
    public void SetPlayer()
    {
        Vector2 startPos = mapCreator.GetPlayerStartPosition();
        player.position = startPos;
    }
}

