using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameManager manager;
    public TimeController timeController;
    public float placingBlockTimer;  //context: in second
    private float placingBlockTimerEditable;
    private float timeRemaining;
    private float timeReducer;
    private int gameAlreadyOver = 0;

    int SECONDREDUCER = 1;  //for "bar in TimeController is reduced per second" purpose

    void Start()
    {
        timeController.TakePlacingBlockTimer(placingBlockTimer);
        placingBlockTimerEditable = placingBlockTimer;
        timeRemaining = placingBlockTimer ;
    }

    void TimeReducing()
    {
        if (timeRemaining <= 0 || gameAlreadyOver == 1)
        {
            return;
        }
        else
        {
            timeReducer = Time.deltaTime;
            timeRemaining -= timeReducer;
            //Debug.Log(timeRemaining);
            IsNeedToSignalController();
            IsTimeOut();
        }
    }

    void IsTimeOut()
    {
        if (timeRemaining <= 0)
        {
            timeReducer = 0;
            Debug.Log("Time's Up");
            manager.GameOver();
        }
    }

    void IsNeedToSignalController()
    {
        if (timeRemaining <= placingBlockTimerEditable - SECONDREDUCER)
        {
            timeController.ReduceTimeBar();
            placingBlockTimerEditable -= SECONDREDUCER;
        }
        
    }

    public void ResetTimerPlease()
    {
        ResetTimer();
    }

    void ResetTimer()
    {
        timeRemaining = placingBlockTimer;
        placingBlockTimerEditable = placingBlockTimer;
        timeController.ResetTheTimeBar();
    }

    public void GameAlreadyOver()
    {
        gameAlreadyOver = 1;
    }

    void Update()
    {
        TimeReducing();
    }
}
