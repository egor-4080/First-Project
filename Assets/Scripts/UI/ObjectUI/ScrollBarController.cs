using System;
using UnityEngine;

public class ScrollBarController : MonoBehaviour
{
    [SerializeField] private int caseCount;
    [SerializeField] private float plusHeight;
    [SerializeField] private float startHeight;

    private RectTransform rectTransform;

    private float weidth;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        weidth = rectTransform.sizeDelta.x;
    }

    public void SetHeightContent(float count)
    {
        print(count);
        rectTransform.sizeDelta = new Vector2(weidth, startHeight);;
        float plusNumber = startHeight;
        for (int i = 0; i < count; i += caseCount)
        {
            plusNumber += plusHeight;
        }

        rectTransform.sizeDelta = new Vector2(weidth, plusNumber);
    }
}
