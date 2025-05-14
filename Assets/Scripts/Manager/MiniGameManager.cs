using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public PlayerController playerController { get; private set; }
    private ResourceController resourceController;

    public MonsterSetter monsterSetter;
    private MonsterManager monsterManager;

    public UIManager UIManager => uiManager;
    private UIManager uiManager;

    [SerializeField] private int currentStageIndex = 0;

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

    public void StartGame() //게임 시작 함수
    {
        uiManager.SetPlayGame();

        SetStage();
    }

    private void SetStage()
    {
        currentStageIndex += 1;

        monsterManager.StartWave();

        uiManager.ChangeWave(currentStageIndex);
    }

    public void ClearStage()
    {
        SetStage();
    }

    public void GameOver()
    {
        monsterManager.StopWave();
        uiManager.SetGameOver();
    }
}
