using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillElements
{
    public string name;
    public string description;
}

public class SkillList : MonoBehaviour
{
    public List<SkillElements> skillLists = new();

    public void Start()
    {
        skillLists.Add(new SkillElements { name = "���ݷ� ����", description = "���ݷ��� 5 �����մϴ�." });
        skillLists.Add(new SkillElements { name = "���ݼӵ� ����", description = "���ݼӵ��� 30% �����մϴ�." });
        skillLists.Add(new SkillElements { name = "�̵��ӵ� ����", description = "�̵��ӵ��� 20% �����մϴ�." });
        //skillLists.Add(new SkillElements { name = "����ü ����", description = "ȭ�� 3���� �߻��ϰ� �˴ϴ�." });
    }
}
