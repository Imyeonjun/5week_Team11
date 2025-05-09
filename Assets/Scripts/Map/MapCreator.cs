using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject[] roomTemplates; // 템플릿 프리팹 불러오기

    [Header("스테이지 구성 프리팹 갯수")]
    public int stageWidth = 5;
    public int stageHeight = 5;

    [Header("프리팹 간격")]
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

                // 이전 방 정보 가져오기 (왼쪽 또는 아래쪽)
                RoomTemplate prevRoomLeft = null;
                RoomTemplate prevRoomDown = null;

                if (x > 0 && map[x - 1, y] != null)
                    prevRoomLeft = map[x - 1, y].GetComponent<RoomTemplate>();

                if (y > 0 && map[x, y - 1] != null)
                    prevRoomDown = map[x, y - 1].GetComponent<RoomTemplate>();

                // 프리팹 중 조건 맞는 거 찾기
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

                // 조건에 맞는 프리팹이 없으면 아무거나 배치하거나 빈 방 처리
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
