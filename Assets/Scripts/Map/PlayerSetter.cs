using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private Transform player; // �̹� �����ϴ� �÷��̾�
    [SerializeField] private MapCreator mapCreator; // MapCreator ����
    
    public void SetPlayer()
    {
        Vector2 startPos = mapCreator.GetPlayerStartPosition();
        player.position = startPos;
    }
}

