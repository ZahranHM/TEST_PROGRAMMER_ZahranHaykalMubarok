using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public TimeManager timeManager;
    public GameObject timerBarRemaining;
    private Vector3 initialBarScale;
    private Vector3 initialBarPosition;
    private float barWidthPerPBT;

    void Start()
    {
        initialBarScale = timerBarRemaining.transform.localScale;
        initialBarPosition = timerBarRemaining.transform.position;
    }

    public void TakePlacingBlockTimer(float placingBlockTimer)
    {
        barWidthPerPBT = timerBarRemaining.transform.localScale.x/placingBlockTimer; //for bar reducing purpose
    }

    public void ReduceTimeBar()
    {
        TimeBarReducing();
    }

    void TimeBarReducing()
    {
        timerBarRemaining.transform.localScale -= new Vector3(barWidthPerPBT, 0, 0);
        timerBarRemaining.transform.position -= new Vector3(barWidthPerPBT/2, 0, 0);
    }

    public void ResetTheTimeBar()
    {
        ResetTimeBar();
    }

    void ResetTimeBar()
    {
        timerBarRemaining.transform.localScale = initialBarScale;
        timerBarRemaining.transform.position = initialBarPosition;
    }

    void Update()
    {
        
    }
}
