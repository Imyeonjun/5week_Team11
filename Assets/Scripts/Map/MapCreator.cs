using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// �� ���� �� ��, ���� �ڵ� ��ġ�� ����ϴ� Ŭ����
public class MapCreator : MonoBehaviour
{
    [SerializeField] private GameObject corridorHorizontalPrefab;  // ���� ���� ������
    [SerializeField] private GameObject corridorVerticalPrefab;    // ���� ���� ������

    public GameObject[] roomTemplates; // �پ��� �� ������ ���

    [Header("�������� ���� ������ ����")]
    public int stageWidth = 5;         // ���ο� ������ �� ��
    public int stageHeight = 5;        // ���ο� ������ �� ��

    [Header("������ ����")]
    public float roomSpacing = 10f;    // �� ���� (�Ÿ�)

    void Start()
    {
        CreatStage();  // ���� ���� �� �� ���� ����
    }

    void CreatStage()
    {
        GameObject[,] map = new GameObject[stageWidth, stageHeight];         // �� ������ ������ 2���� �迭
        Vector2Int firstPlacedRoom = new Vector2Int(-1, -1);                 // ���� ���� ��ġ�� �� ��ǥ

        // 1. �� ��ġ
        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                Vector2 pos = new Vector2(x * roomSpacing, y * roomSpacing); // ���� ��ġ�� ��ġ

                RoomTemplate prevRoomLeft = null;
                RoomTemplate prevRoomDown = null;

                // ���ʿ� ���� ������ ���� �� ���� ��������
                if (x > 0 && map[x - 1, y] != null)
                    prevRoomLeft = map[x - 1, y].GetComponent<RoomTemplate>();

                // �Ʒ��� ���� ������ ���� �� ���� ��������
                if (y > 0 && map[x, y - 1] != null)
                    prevRoomDown = map[x, y - 1].GetComponent<RoomTemplate>();

                List<GameObject> candidates = new List<GameObject>(); // ��ġ ������ ������ ����Ʈ

                // ��� ������ �߿��� ���� ������ �����ϴ� �����ո� �ĺ��� �߰�
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

                // �ĺ� ������ �� �������� �����Ͽ� ��ġ
                if (candidates.Count > 0)
                {
                    int randIndex = Random.Range(0, candidates.Count);
                    GameObject chosen = candidates[randIndex];
                    RoomTemplate candidate = chosen.GetComponent<RoomTemplate>();

                    map[x, y] = Instantiate(chosen, pos, Quaternion.identity); // �� ��ġ

                    // ���� ó�� ��ġ�� �� ���
                    if (firstPlacedRoom.x == -1 && !candidate.isEmptyRoom)
                    {
                        firstPlacedRoom = new Vector2Int(x, y);
                    }
                }
            }
        }

        // 2. ���� ����
        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                GameObject current = map[x, y];
                if (current == null) continue;

                RoomTemplate room = current.GetComponent<RoomTemplate>();
                Vector2 currPos = new Vector2(x * roomSpacing, y * roomSpacing);

                // ������ ��� ���� �����ϸ� ���� ����
                if (room.openRight && x + 1 < stageWidth && map[x + 1, y] != null)
                {
                    RoomTemplate rightRoom = map[x + 1, y].GetComponent<RoomTemplate>();
                    if (rightRoom.openLeft)
                    {
                        Vector2 rightPos = new Vector2((x + 1) * roomSpacing, y * roomSpacing);
                        RoomConnecter.TryCreateCorridor(currPos, rightPos, room, rightRoom, corridorHorizontalPrefab, corridorVerticalPrefab);
                    }
                }

                // ���� ��� ���� �����ϸ� ���� ����
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

        // 3. ���� �� ����
        MapValidator validator = GetComponent<MapValidator>();
        if (validator != null)
        {
            validator.RemoveIsolatedRooms(map, firstPlacedRoom); // BFS ��� ���� Ȯ��
        }
        else
        {
            Debug.LogWarning("MapValidator�� �������� �ʽ��ϴ�!");
        }
    }
}
