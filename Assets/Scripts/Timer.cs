using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int timeToSolve;
    public float timerSpeed;
    public TextMeshProUGUI timer;

    private float _timeRemaining;
    private bool _tick;
    
    void Start()
    {
        _timeRemaining = timeToSolve;
        _tick = true;
    }
    
    void Update()
    {
        if(!_tick) return;
        
        _timeRemaining -= Time.deltaTime * timerSpeed;
        _timeRemaining = Mathf.Clamp(_timeRemaining, 0, timeToSolve);
        timer.text = "Time remaining: " + (int)_timeRemaining/60 + ":" + ((int)_timeRemaining % 60 < 10 ? 0 + "" + (int)_timeRemaining % 60 : (int)_timeRemaining % 60);

        if (_timeRemaining != 0) return;
        
        SceneSwitcher.Fade("OutOfTime");
        _tick = false;
    }
}
