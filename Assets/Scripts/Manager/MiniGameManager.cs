using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public PlayerController playerController { get; private set; }
    private ResourceController resourceController;

    [SerializeField] private MapResseter mapResseter;
    [SerializeField] private MapCreator mapCreator;
    private MonsterManager monsterManager;

    public UIManager UIManager => uiManager;
    private UIManager uiManager;

    [SerializeField] public int currentStageIndex = 0;

    public static bool isFirstLoading = true;

    private void Awake()
    {
        Instance = this;

        playerController = FindObjectOfType<PlayerController>();
        playerController.Init(this);

        uiManager = FindObjectOfType<UIManager>();

        monsterManager = GetComponentInChildren<MonsterManager>();
        monsterManager.Init(this);

        resourceController = playerController.GetComponent<ResourceController>();
        resourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        resourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        //uiManager.ChangePlayerHP(resourceController.CurrentHealth, resourceController.MaxHealth);
    }

    private void Start()
    {
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }

    public void StartGame() //���� ���� �Լ�
    {
        uiManager.SetPlayGame();

        SetStage();
    }

    private void SetStage()
    {
        currentStageIndex += 1;

        mapCreator.CreateStage();

        StartCoroutine(DelayStartWave(3f)); // 3�� �� SetStage ����

        uiManager.ChangeWave(currentStageIndex);
    }

    public void ClearStage()
    {
        StartCoroutine(HandleStageClear());

        mapResseter.ClearMap();
        SetStage();
    }

    public void GameOver()
    {
        monsterManager.StopWave();
        uiManager.SetGameOver();
    }

    public IEnumerator DelayStartWave(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds); // ������ �ð���ŭ ���
        monsterManager.StartWave(); // ���� �� ����
    }

    private IEnumerator HandleStageClear()
    {
        // 1. Ŭ���� ���� ǥ��
        uiManager.ShowClearText();

        // 2. 1�� ���
        yield return new WaitForSeconds(1f);

        // 3. Ŭ���� ���� �����
        uiManager.HideClearText();

    }
}
