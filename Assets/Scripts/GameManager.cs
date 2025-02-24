using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public event EventHandler OnStateChanged;
    private enum State
    {
        WaitingToSart,
        CountdownToStart,
        GamePlaying,
        GameOver
        
    }

    private State state;
    private float waitingToStartTimer = 1f;
     float countdownToStartTimer = 3f;
     float gamePlayingTimer = 10f;
     float gamePlayingTimerMax = 10f;
    

    private void Awake()
    {
        state = State.WaitingToSart;

        Instance = this;
    }

    private void Update()
    {

        switch (state)
        {
            case State.WaitingToSart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                
                break;
            case State.GamePlaying:

                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGameTimerNormalized()
    {
        return gamePlayingTimer / gamePlayingTimerMax;
    }
}
