using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject[] roomTemplates; // 템플릿 프리팹 불러오기

    [Header("스테이지 생성 범위")]
    public int stageWidth = 5;
    public int stageHeight = 5;

    [Header("스테이지 간격")]
    public float roomSpacing = 10f;

    void Start()
    {
        CreatStage();
    }

    void CreatStage()
    {
        for (int x = 0; x < stageWidth; x++)
        {
            for (int y = 0; y < stageHeight; y++)
            {
                Vector2 pos = new Vector2(x * roomSpacing, y * roomSpacing);
                int index = Random.Range(0, roomTemplates.Length);
                Instantiate(roomTemplates[index], pos, Quaternion.identity);
            }
        }
    }
}
