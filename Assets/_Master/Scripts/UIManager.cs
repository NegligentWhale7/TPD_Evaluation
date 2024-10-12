using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private ShootingController shootingController;
    [SerializeField] private GameObject rocketPanel;
    [SerializeField] private GameObject rocketItem;
    [SerializeField] private TextMeshProUGUI currentMoney;
    [SerializeField] private TextMeshProUGUI currentScore;
    [Header("Game Over")]
    [SerializeField] private Canvas gameOverPanel;
    [SerializeField] private TextMeshProUGUI totalGOScoreText;
    [SerializeField] private TextMeshProUGUI totalGOMoneyText;
    [SerializeField] private TextMeshProUGUI waveGOReachedText;
    [Header("Inter Waves")]
    [SerializeField] private Canvas interWavesCanvas;
    [SerializeField] private Canvas shopCanvas;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;
    [SerializeField] private TextMeshProUGUI waveReachedText;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameOverPanel.enabled = false;
        interWavesCanvas.enabled = false;
        ShowAvailableRockets();
    }

    private void Update()
    {
        UpdatePlayerHUD();
    }

    public void UpdatePlayerHUD()
    {
        currentMoney.text = $"$ {GameManager.Instance.TotalMoney}";
        currentScore.text = $"Score: {GameManager.Instance.TotalScore}";
    }

    public void ShowAvailableRockets()
    {
        for (int i = 0; i < shootingController.CurrentMissiles; i++)
        {
            Instantiate(rocketItem, rocketPanel.transform);
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.enabled = true;
        totalGOMoneyText.text = $"$ {GameManager.Instance.TotalMoney}";
        totalGOScoreText.text = $"Total Score: {GameManager.Instance.TotalScore}";
        waveGOReachedText.text = $"Wave Reached: {GameManager.Instance.WaveReached}";
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.enabled = false;
        totalGOMoneyText.text = "";
        totalGOScoreText.text = "";
        waveGOReachedText.text = "";
    }

    public void ShowInterWavePanel()
    {
        interWavesCanvas.enabled = true;
        totalMoneyText.text = $"$ {GameManager.Instance.TotalMoney}";
        totalScoreText.text = $"Total Score: {GameManager.Instance.TotalScore}";
        waveReachedText.text = $"Wave Reached: {GameManager.Instance.WaveReached}";
    }

    public void HideInterWavePanel()
    {
        interWavesCanvas.enabled = false;
        totalMoneyText.text = "";
        totalScoreText.text = "";
        waveReachedText.text = "";
    }

    public void ShowShopCanvas()
    {
        shopCanvas.enabled = true;
    }

    public void HideShopCanvas()
    {
        shopCanvas.enabled = false;
    }
}
