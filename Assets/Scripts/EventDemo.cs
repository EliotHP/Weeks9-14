using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;

    public UnityEvent OnTimerFinished;
    public float timerLength = 2;
    public float t;

    private void Update()
    {
        t += 1 * Time.deltaTime;
        if (t > timerLength)
        {
            OnTimerFinished.Invoke();
            t = 0;
        }
    }
    public void MouseJustEntered()
    {
        Debug.Log("Mouse just entered me");
        banana.localScale = Vector3.one * 1.2f;
    }
    public void MouseJustExited()
    {
        Debug.Log("Mouse just exited me");
        banana.localScale = Vector3.one;
    }
}
