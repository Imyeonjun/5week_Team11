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
        Vector2Int firstPlacedRoom = new Vector2Int(-1, -1);

        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                Vector2 pos = new Vector2(x * roomSpacing, y * roomSpacing);

                RoomTemplate prevRoomLeft = null;
                RoomTemplate prevRoomDown = null;

                if (x > 0 && map[x - 1, y] != null)
                    prevRoomLeft = map[x - 1, y].GetComponent<RoomTemplate>();

                if (y > 0 && map[x, y - 1] != null)
                    prevRoomDown = map[x, y - 1].GetComponent<RoomTemplate>();

                // 조건에 맞는 프리팹 리스트 만들기
                List<GameObject> candidates = new List<GameObject>();

                foreach (GameObject prefab in roomTemplates)
                {
                    RoomTemplate candidate = prefab.GetComponent<RoomTemplate>();
                    bool fits = true;

                    if (prevRoomLeft != null)
                        fits &= prevRoomLeft.openRight && candidate.openLeft;

                    if (prevRoomDown != null)
                        fits &= prevRoomDown.openTop && candidate.openBottom;

                    if (fits)
                        candidates.Add(prefab);
                }

                if (candidates.Count > 0)
                {
                    int randIndex = Random.Range(0, candidates.Count);
                    GameObject chosen = candidates[randIndex];
                    RoomTemplate candidate = chosen.GetComponent<RoomTemplate>();

                    map[x, y] = Instantiate(chosen, pos, Quaternion.identity);

                    // 첫 방 저장: 비어있지 않은 방만
                    if (firstPlacedRoom.x == -1 && !candidate.isEmptyRoom)
                    {
                        firstPlacedRoom = new Vector2Int(x, y);
                    }
                }
            }
        }

        MapValidator validator = GetComponent<MapValidator>();

        if (validator != null)
        {
            validator.RemoveIsolatedRooms(map, firstPlacedRoom);
        }
        else
        {
            Debug.LogWarning("MapValidator가 존재하지 않습니다!");
        }
    }
}
