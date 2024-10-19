using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerController : MonoBehaviour
{
    public UnityEvent timerEnd { get; private set; } = new UnityEvent();

    private bool isHide;

    public void StartTimer(float seconds)
    {
        StartCoroutine(WaitTimer(seconds));
    }

    private IEnumerator WaitTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timerEnd.Invoke();
    }
}
