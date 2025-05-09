using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject[] roomTemplates; // ���ø� ������ �ҷ�����

    [Header("�������� ���� ������ ����")]
    public int stageWidth = 5;
    public int stageHeight = 5;

    [Header("������ ����")]
    public float roomSpacing = 10f;

    void Start()
    {
        CreatStage();
    }

    void CreatStage()
    {
        GameObject[,] map = new GameObject[stageWidth, stageHeight];

        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                Vector2 pos = new Vector2(x * roomSpacing, y * roomSpacing);
                GameObject chosen = null;

                // ���� �� ���� �������� (���� �Ǵ� �Ʒ���)
                RoomTemplate prevRoomLeft = null;
                RoomTemplate prevRoomDown = null;

                if (x > 0 && map[x - 1, y] != null)
                    prevRoomLeft = map[x - 1, y].GetComponent<RoomTemplate>();

                if (y > 0 && map[x, y - 1] != null)
                    prevRoomDown = map[x, y - 1].GetComponent<RoomTemplate>();

                // ������ �� ���� �´� �� ã��
                foreach (GameObject prefab in roomTemplates)
                {
                    RoomTemplate candidate = prefab.GetComponent<RoomTemplate>();
                    bool fits = true;

                    if (prevRoomLeft != null)
                    {
                        if (!(prevRoomLeft.openRight && candidate.openLeft))
                            fits = false;
                    }

                    if (prevRoomDown != null)
                    {
                        if (!(prevRoomDown.openTop && candidate.openBottom))
                            fits = false;
                    }

                    if (fits)
                    {
                        chosen = prefab;
                        break;
                    }
                }

                // ���ǿ� �´� �������� ������ �ƹ��ų� ��ġ�ϰų� �� �� ó��
                if (chosen == null)
                {
                    int randomIndex = Random.Range(0, roomTemplates.Length);
                    chosen = roomTemplates[randomIndex];
                }

                map[x, y] = Instantiate(chosen, pos, Quaternion.identity);
            }
        }
    }

}
