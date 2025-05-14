using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillElements
{
    public string name;
    public string description;
    public int stack = 0;
}

public class SkillList : MonoBehaviour
{
    public List<SkillElements> skillLists = new()
    {
        new SkillElements { name = "공격력 증가", description = "공격력이 5 증가합니다." },
        new SkillElements { name = "공격속도 증가", description = "공격속도가 10% 빨라집니다." },
        new SkillElements { name = "이동속도 증가", description = "이동속도가 20% 빨라집니다." },
        new SkillElements { name = "투사체 갯수 증가", description = "화살 갯수가 1개 증가합니다." }
    };
}
