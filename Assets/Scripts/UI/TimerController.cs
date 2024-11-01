using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerController : MonoBehaviour
{
    [SerializeField] private Animator timerAnimator;
    [SerializeField] private TMP_Text timerCount;

    public UnityEvent timerEnd { get; private set; } = new UnityEvent();

    private bool isHide;

    private void Start()
    {

    }

    public void StartTimer(float seconds)
    {
        timerAnimator.SetBool("isAppeared", true);
        StartCoroutine(WaitForEndTimer(seconds));
        StartCoroutine(TimerCounter(seconds));
    }

    private IEnumerator WaitForEndTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timerAnimator.SetBool("isAppeared", false);
        timerEnd?.Invoke();
    }

    private IEnumerator TimerCounter(float seconds)
    {
        while (seconds > 0)
        {
            seconds -= Time.deltaTime;
            var counter = Mathf.Round(seconds * 100) / 100;
            timerCount.text = counter.ToString();
            yield return null;
        }
    }
}
