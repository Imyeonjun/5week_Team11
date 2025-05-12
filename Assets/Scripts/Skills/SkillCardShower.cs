using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardShower : MonoBehaviour
{
    public GameObject skillCardBg;
    void Start()
    {
        skillCardBg = GetComponent<GameObject>();
    }
}
