using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapResseter : MonoBehaviour
{
    public void ClearMap()
    {
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
    }
}
