using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    Timer timer;

    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            timer = new Timer(Finished, 5f);
            paused = false;
        }    

        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            timer.SetTimerPause(!paused);
            paused = !paused;
        }

        if (timer != null)
        {
            Debug.Log("Timer: " + Mathf.RoundToInt(timer.GetRemainingSeconds()));
            timer.UpdateTimer();
        }
    }

    void Finished()
    {
        Debug.Log("Timer has Finished!");
        timer = null;
    }    
}
