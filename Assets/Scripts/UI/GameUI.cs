using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TextMeshProUGUI hpText;

    private void Start()
    {
        UpdateHPSlider(1);
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    public void UpdateCountMonster(int count) // ���� ī��Ʈ �߰�
    {
        countText.text = count.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void UpdateHPText(float currentHP, float maxHP)
    {
        hpText.text = $"{Mathf.CeilToInt(currentHP)} / {Mathf.CeilToInt(maxHP)}"; // ���� ü�� �ؽ�Ʈ�� ǥ��
    }

}
