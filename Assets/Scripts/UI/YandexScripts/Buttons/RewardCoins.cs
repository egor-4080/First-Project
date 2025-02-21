using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardCoins : MonoBehaviour
{
    [SerializeField] private string rewardID;
    [SerializeField] private float reward;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => YG2.RewardedAdvShow(rewardID));
        YG2.onRewardAdv += OnReward;
    }

    private void OnReward(string id)
    {
        if (rewardID == id)
        {
            
        }
    }
}
