using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private Transform player; // �̹� �����ϴ� �÷��̾�
    [SerializeField] private MapCreator mapCreator; // MapCreator ����
    public FollowCamera followCamera;

    void Start()
    {
        StartCoroutine(SetPlayerStartWhenReady());
    }

    IEnumerator SetPlayerStartWhenReady()
    {
        // �� ���� �Ϸ���� ���
        while (!mapCreator.IsGenerationComplete)
            yield return null;

        Vector2 startPos = mapCreator.GetPlayerStartPosition();
        player.position = startPos;

     
    }
}

