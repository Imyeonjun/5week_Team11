using System.Collections.Generic;
using UnityEngine;

public class MapValidator : MonoBehaviour
{
    public void RemoveIsolatedRooms(GameObject[,] map, Vector2Int start)
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
            new Vector2Int(1, 0), new Vector2Int(-1, 0),
            new Vector2Int(0, 1), new Vector2Int(0, -1)
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
