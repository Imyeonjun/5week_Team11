using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject corridorHorizontalPrefab;
    [SerializeField] private GameObject corridorVerticalPrefab;
    [SerializeField] private MiniGameManager miniManager;
    [SerializeField] private PlayerSetter playerSetter;

    public GameObject[] roomTemplates;

    [Header("스테이지 설정")]
    private int stageWidth = 10;
    private int stageHeight = 10;
    private float roomSpacing = 12f;

    public float RoomSpacing => roomSpacing;

    public int minRoomCount = 5;

    public bool IsGenerationComplete { get; private set; } = false;

    private Vector2 playerStartPos;
    private GameObject[,] map;
    private List<Vector2Int> roomPositions;

    public Vector2 GetPlayerStartPosition() => playerStartPos;
    public List<Vector2Int> RoomPositions => roomPositions;
    public GameObject GetRoomAt(Vector2Int pos) => map[pos.x, pos.y];

    void Update()
    {
        minRoomCount = 5 + miniManager.currentStageIndex;
    }

    public void CreateStage()
    {
        Debug.Log("맵 생성");

        map = new GameObject[stageWidth, stageHeight];
        roomPositions = GenerateRoomPath(minRoomCount, stageWidth, stageHeight);

        Dictionary<Vector2Int, HashSet<string>> requiredOpens = new Dictionary<Vector2Int, HashSet<string>>();

        foreach (Vector2Int pos in roomPositions)
        {
            requiredOpens[pos] = new HashSet<string>();
        }

        foreach (Vector2Int pos in roomPositions)
        {
            Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

            foreach (var dir in directions)
            {
                Vector2Int next = pos + dir;
                if (roomPositions.Contains(next))
                {
                    if (dir == Vector2Int.right) { requiredOpens[pos].Add("Right"); requiredOpens[next].Add("Left"); }
                    if (dir == Vector2Int.left) { requiredOpens[pos].Add("Left"); requiredOpens[next].Add("Right"); }
                    if (dir == Vector2Int.up) { requiredOpens[pos].Add("Top"); requiredOpens[next].Add("Bottom"); }
                    if (dir == Vector2Int.down) { requiredOpens[pos].Add("Bottom"); requiredOpens[next].Add("Top"); }
                }
            }
        }

        foreach (Vector2Int pos in roomPositions)
        {
            Vector2 worldPos = new Vector2(pos.x * roomSpacing, pos.y * roomSpacing);
            HashSet<string> needed = requiredOpens[pos];

            var candidates = roomTemplates.Where(prefab =>
            {
                RoomTemplate rt = prefab.GetComponent<RoomTemplate>();
                return (!needed.Contains("Top") || rt.openTop)
                    && (!needed.Contains("Bottom") || rt.openBottom)
                    && (!needed.Contains("Left") || rt.openLeft)
                    && (!needed.Contains("Right") || rt.openRight);
            }).ToList();

            if (candidates.Count == 0)
            {
                Debug.LogWarning($"조건에 맞는 방 프리팹 없음: {pos} → 기본 첫 번째 사용");
                candidates.Add(roomTemplates[0]);
            }

            GameObject prefab = candidates[Random.Range(0, candidates.Count)];
            GameObject room = Instantiate(prefab, worldPos, Quaternion.identity);
            map[pos.x, pos.y] = room;
        }

        Vector2Int startRoom = roomPositions[0];
        GameObject startRoomGO = map[startRoom.x, startRoom.y];
        Tilemap tilemap = startRoomGO.GetComponentInChildren<Tilemap>();
        if (tilemap != null)
        {
            playerStartPos = tilemap.transform.position + tilemap.localBounds.center;
        }
        else
        {
            playerStartPos = startRoomGO.transform.position;
        }
        IsGenerationComplete = true;

        foreach (Vector2Int pos in roomPositions)
        {
            Vector2Int[] directions = { Vector2Int.right, Vector2Int.up };

            foreach (var dir in directions)
            {
                Vector2Int next = pos + dir;
                if (roomPositions.Contains(next))
                {
                    Vector2 start = new Vector2(pos.x * roomSpacing, pos.y * roomSpacing);
                    Vector2 end = new Vector2(next.x * roomSpacing, next.y * roomSpacing);
                    RoomTemplate roomA = map[pos.x, pos.y].GetComponent<RoomTemplate>();
                    RoomTemplate roomB = map[next.x, next.y].GetComponent<RoomTemplate>();

                    RoomConnecter.TryCreateCorridor(start, end, roomA, roomB, corridorHorizontalPrefab, corridorVerticalPrefab);
                }
            }
        }

        playerSetter.SetPlayer();
    }

    List<Vector2Int> GenerateRoomPath(int minRoomCount, int width, int height)
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        Vector2Int start = new Vector2Int(width / 2, height / 2);
        visited.Add(start);
        queue.Enqueue(start);

        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };

        System.Random rnd = new System.Random();

        while (visited.Count < minRoomCount && queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            directions = directions.OrderBy(x => rnd.Next()).ToArray();

            foreach (var dir in directions)
            {
                Vector2Int next = current + dir;
                if (next.x < 0 || next.y < 0 || next.x >= width || next.y >= height)
                    continue;

                if (!visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue(next);

                    if (visited.Count >= minRoomCount)
                        break;
                }
            }
        }

        return new List<Vector2Int>(visited);
    }
}
