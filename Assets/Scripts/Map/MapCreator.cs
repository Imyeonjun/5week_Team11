using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject corridorHorizontalPrefab;
    [SerializeField] private GameObject corridorVerticalPrefab;

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

                // ���ǿ� �´� ������ ����Ʈ �����
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

                    // ù �� ����: ������� ���� �游
                    if (firstPlacedRoom.x == -1 && !candidate.isEmptyRoom)
                    {
                        firstPlacedRoom = new Vector2Int(x, y);
                    }
                }
            }
        }

        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                GameObject current = map[x, y];
                if (current == null) continue;

                RoomTemplate room = current.GetComponent<RoomTemplate>();
                Vector2 currPos = new Vector2(x * roomSpacing, y * roomSpacing);

                if (room.openRight && x + 1 < stageWidth && map[x + 1, y] != null)
                {
                    RoomTemplate rightRoom = map[x + 1, y].GetComponent<RoomTemplate>();
                    if (rightRoom.openLeft)
                    {
                        Vector2 rightPos = new Vector2((x + 1) * roomSpacing, y * roomSpacing);
                        CreateCorridor(currPos, rightPos);
                    }
                }

                if (room.openTop && y + 1 < stageHeight && map[x, y + 1] != null)
                {
                    RoomTemplate topRoom = map[x, y + 1].GetComponent<RoomTemplate>();
                    if (topRoom.openBottom)
                    {
                        Vector2 topPos = new Vector2(x * roomSpacing, (y + 1) * roomSpacing);
                        CreateCorridor(currPos, topPos);
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
            Debug.LogWarning("MapValidator�� �������� �ʽ��ϴ�!");
        }
    }

    void CreateCorridor(Vector2 start, Vector2 end)
    {
        Vector2 dir = end - start;
        Vector2 mid = (start + end) / 2f;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            Instantiate(corridorHorizontalPrefab, mid, Quaternion.identity);
        }
        else
        {
            Instantiate(corridorVerticalPrefab, mid, Quaternion.identity);
        }
    }
}
