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
        new SkillElements { name = "���ݷ� ����", description = "���ݷ��� 5 �����մϴ�." },
        new SkillElements { name = "���ݼӵ� ����", description = "���ݼӵ��� 10% �������ϴ�." },
        new SkillElements { name = "�̵��ӵ� ����", description = "�̵��ӵ��� 20% �������ϴ�." },
        new SkillElements { name = "����ü ���� ����", description = "ȭ�� ������ 1�� �����մϴ�." }
    };
}
