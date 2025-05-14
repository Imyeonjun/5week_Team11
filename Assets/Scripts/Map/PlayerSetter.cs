using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private MapCreator mapCreator;

    // ���� �ʿ��� ����ϴ� ���
    public void SetPlayer()
    {
        Vector2 startPos = mapCreator.GetPlayerStartPosition();
        player.position = startPos;
    }

    // ������ �� �ܺ� ��ġ ������
    public void SetPlayer(Vector2 customPosition)
    {
        player.position = customPosition;
    }
}
