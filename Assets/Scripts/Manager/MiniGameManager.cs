using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public PlayerController playerController { get; private set; }
    private ResourceController resourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private MonsterManager monsterManager;

    private UIManager uiManager;
    public UIManager UIManager => uiManager;

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

    public void StartGame()
    {
        uiManager.SetPlayGame();
        StartNextWave();

    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        monsterManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        monsterManager.StopWave();
        uiManager.SetGameOver();
    }


}
