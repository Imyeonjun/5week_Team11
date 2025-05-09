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

        // 첫 방이 없으면 연결 검사 건너뜀
        if (firstPlacedRoom.x != -1 && firstPlacedRoom.y != -1)
        {
            RoomChecker(map, firstPlacedRoom);
        }
        else
        {
            Debug.LogWarning("생성된 방이 하나도 없어서 RoomChecker를 실행하지 않음");
        }
    }

    void RoomChecker(GameObject[,] map, Vector2Int start)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        bool[,] visited = new bool[width, height];

        if (map[start.x, start.y] == null)
        {
            Debug.LogWarning("시작 방 좌표에 방이 없음");
            return;
        }

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(start);
        visited[start.x, start.y] = true;

        Vector2Int[] directions = {
            new Vector2Int(1, 0),  // 오른쪽
            new Vector2Int(-1, 0), // 왼쪽
            new Vector2Int(0, 1),  // 위
            new Vector2Int(0, -1)  // 아래
        };

        while (queue.Count > 0)
        {
            Vector2Int curr = queue.Dequeue();
            RoomTemplate room = map[curr.x, curr.y].GetComponent<RoomTemplate>();

            foreach (var dir in directions)
            {
                Vector2Int next = curr + dir;

                if (next.x < 0 || next.y < 0 || next.x >= width || next.y >= height)
                    continue;

                if (visited[next.x, next.y]) continue;
                if (map[next.x, next.y] == null) continue;

                RoomTemplate neighbor = map[next.x, next.y].GetComponent<RoomTemplate>();

                // 문 방향이 맞는 경우만 연결로 판단
                if (
                    (dir.x == 1 && room.openRight && neighbor.openLeft) ||
                    (dir.x == -1 && room.openLeft && neighbor.openRight) ||
                    (dir.y == 1 && room.openTop && neighbor.openBottom) ||
                    (dir.y == -1 && room.openBottom && neighbor.openTop)
                )
                {
                    visited[next.x, next.y] = true;
                    queue.Enqueue(next);
                }
            }
        }

        // 고립된 방 제거
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] != null && !visited[x, y])
                {
                    Debug.Log($"고립된 방 제거: ({x}, {y})");
                    Destroy(map[x, y]);
                    map[x, y] = null;
                }
            }
        }
    }
}
