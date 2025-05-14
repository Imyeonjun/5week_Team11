using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResseter : MonoBehaviour
{
    [SerializeField] private MapCreator mapCreator;

    public void ClearMap()
    {
        // ������Ʈ ����
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

        // ���� ������ �ʱ�ȭ
        if (mapCreator != null)
        {
            mapCreator.ResetData();
        }
    }
}
