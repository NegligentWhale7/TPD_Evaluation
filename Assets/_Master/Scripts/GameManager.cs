using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float totalScore;
    private float totalMoney;
    private float waveReached;

    public static GameManager Instance { get; private set; }

    public float TotalScore
    {
        get => totalScore;
        set => totalScore = value;
    }
    public float WaveReached
    {
        get => waveReached;
        set => waveReached = value;
    }
    public float TotalMoney
    {
        get => totalMoney;
        set => totalMoney = value;
    }

    public enum GameState { Playing, InterWave, GameOver }
    public GameState CurrentGameState { get; set; } = GameState.Playing;
    
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
        Cursor.visible = true;
    }

    public void AddScore(float score)
    {
        AudioManager.Instance.PlaySoundFX(SoundType.Score);
        totalScore += score;
    }

    public void AddMoney(float money)
    {
        AudioManager.Instance.PlaySoundFX(SoundType.Coin);
        totalMoney += money;
    }

    public void GameOver()
    {
        AudioManager.Instance.PlaySoundFX(SoundType.GameOver);
        CurrentGameState = GameState.GameOver;
        UIManager.Instance.ShowGameOverPanel();
    }

    public void InterWave()
    {
        AudioManager.Instance.PlaySoundFX(SoundType.Victory);
        CurrentGameState = GameState.InterWave;
        UIManager.Instance.ShowInterWavePanel();
    }

    public void ResumeGame()
    {
        CurrentGameState = GameState.Playing;
        UIManager.Instance.HideGameOverPanel();
        UIManager.Instance.HideInterWavePanel();
        UIManager.Instance.ShowAvailableRockets();
    }
}
