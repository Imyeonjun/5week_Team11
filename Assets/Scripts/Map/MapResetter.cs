using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResseter : MonoBehaviour
{
    [SerializeField] private MapCreator mapCreator;

    public void ClearMap()
    {
        // 오브젝트 제거
        GameObject[] mapObjects = GameObject.FindGameObjectsWithTag("Map");
        foreach (GameObject obj in mapObjects)
        {
            Destroy(obj);
        }

        GameObject[] corridorObjects = GameObject.FindGameObjectsWithTag("Corridor");
        foreach (GameObject obj in corridorObjects)
        {
            Destroy(obj);
        }

        // 내부 데이터 초기화
        if (mapCreator != null)
        {
            mapCreator.ResetData();
        }
    }
}
