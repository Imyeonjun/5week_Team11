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
        skillLists.Add(new SkillListElements { name = "���ݷ� ����", description = "���ݷ��� 5 �����մϴ�." });
        skillLists.Add(new SkillListElements { name = "���ݼӵ� ����", description = "���ݼӵ��� 30% �����մϴ�." });
        skillLists.Add(new SkillListElements { name = "����ü ����", description = "ȭ�� 3���� �߻��ϰ� �˴ϴ�." });
    }
}

