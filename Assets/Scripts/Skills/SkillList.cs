using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillListElements
{
    public string name;
    public string description;
}

public class SkillList
{
    public List<SkillListElements> skillLists;

    public void SkillLists()
    {
        skillLists.Add(new SkillListElements { name = "공격력 증가", description = "공격력이 5 증가합니다." });
        skillLists.Add(new SkillListElements { name = "공격속도 증가", description = "공격속도가 30% 증가합니다." });
        skillLists.Add(new SkillListElements { name = "투사체 증가", description = "화살 3개를 발사하게 됩니다." });
    }
}

