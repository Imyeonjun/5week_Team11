using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 맵 생성 및 방, 복도 자동 배치를 담당하는 클래스
public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject corridorHorizontalPrefab;  // 가로 복도 프리팹
    [SerializeField] private GameObject corridorVerticalPrefab;    // 세로 복도 프리팹

    public GameObject[] roomTemplates; // 다양한 방 프리팹 목록

    [Header("스테이지 구성 프리팹 갯수")]
    public int stageWidth = 5;         // 가로에 생성할 방 수
    public int stageHeight = 5;        // 세로에 생성할 방 수

    [Header("프리팹 간격")]
    public float roomSpacing = 10f;    // 방 간격 (거리)

    void Start()
    {
        CreatStage();  // 게임 시작 시 맵 생성 실행
    }

    void CreatStage()
    {
        GameObject[,] map = new GameObject[stageWidth, stageHeight];         // 방 정보를 저장할 2차원 배열
        Vector2Int firstPlacedRoom = new Vector2Int(-1, -1);                 // 가장 먼저 배치된 방 좌표

        // 1. 방 배치
        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                Vector2 pos = new Vector2(x * roomSpacing, y * roomSpacing); // 현재 배치할 위치

                RoomTemplate prevRoomLeft = null;
                RoomTemplate prevRoomDown = null;

                // 왼쪽에 방이 있으면 이전 방 정보 가져오기
                if (x > 0 && map[x - 1, y] != null)
                    prevRoomLeft = map[x - 1, y].GetComponent<RoomTemplate>();

                // 아래에 방이 있으면 이전 방 정보 가져오기
                if (y > 0 && map[x, y - 1] != null)
                    prevRoomDown = map[x, y - 1].GetComponent<RoomTemplate>();

                List<GameObject> candidates = new List<GameObject>(); // 배치 가능한 프리팹 리스트

                // 모든 프리팹 중에서 연결 조건을 만족하는 프리팹만 후보로 추가
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

                // 후보 프리팹 중 무작위로 선택하여 배치
                if (candidates.Count > 0)
                {
                    int randIndex = Random.Range(0, candidates.Count);
                    GameObject chosen = candidates[randIndex];
                    RoomTemplate candidate = chosen.GetComponent<RoomTemplate>();

                    map[x, y] = Instantiate(chosen, pos, Quaternion.identity); // 방 배치

                    // 가장 처음 배치된 방 기록
                    if (firstPlacedRoom.x == -1 && !candidate.isEmptyRoom)
                    {
                        firstPlacedRoom = new Vector2Int(x, y);
                    }
                }
            }
        }

        // 2. 복도 연결
        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                GameObject current = map[x, y];
                if (current == null) continue;

                RoomTemplate room = current.GetComponent<RoomTemplate>();
                Vector2 currPos = new Vector2(x * roomSpacing, y * roomSpacing);

                // 오른쪽 방과 연결 가능하면 복도 생성
                if (room.openRight && x + 1 < stageWidth && map[x + 1, y] != null)
                {
                    RoomTemplate rightRoom = map[x + 1, y].GetComponent<RoomTemplate>();
                    if (rightRoom.openLeft)
                    {
                        Vector2 rightPos = new Vector2((x + 1) * roomSpacing, y * roomSpacing);
                        RoomConnecter.TryCreateCorridor(currPos, rightPos, room, rightRoom, corridorHorizontalPrefab, corridorVerticalPrefab);
                    }
                }

                // 위쪽 방과 연결 가능하면 복도 생성
                if (room.openTop && y + 1 < stageHeight && map[x, y + 1] != null)
                {
                    RoomTemplate topRoom = map[x, y + 1].GetComponent<RoomTemplate>();
                    if (topRoom.openBottom)
                    {
                        Vector2 topPos = new Vector2(x * roomSpacing, (y + 1) * roomSpacing);
                        RoomConnecter.TryCreateCorridor(currPos, topPos, room, topRoom, corridorHorizontalPrefab, corridorVerticalPrefab);
                    }
                }
            }
        }

        // 3. 고립된 방 제거
        MapValidator validator = GetComponent<MapValidator>();
        if (validator != null)
        {
            validator.RemoveIsolatedRooms(map, firstPlacedRoom); // BFS 기반 연결 확인
        }
        else
        {
            Debug.LogWarning("MapValidator가 존재하지 않습니다!");
        }
    }
}
