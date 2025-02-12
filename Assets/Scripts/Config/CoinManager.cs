using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinManager : MonoBehaviour
{
    public UnityAction OnMoneyChanged { get; private set; }

    [SerializeField] private TMP_Text coinText;

    public static CoinManager Instanse;

    private AudioSource audioSours;
    private float currentMoney;

    private void Awake()
    {
        OnMoneyChanged += UpdateCoinText;

        if (Instanse == null)
        {
            Instanse = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSours = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        audioSours.Play();
    }

    public void AddMoney(float addedMoney)
    {
        ChangeMoney(addedMoney);
    }

    public bool TryBuy(float price)
    {
        if (currentMoney - price < 0)
            return false;
        else
            ChangeMoney(-price);
        return true;
    }

    private void UpdateCoinText()
    {
        coinText.text = currentMoney.ToString();
    }

    private void ChangeMoney(float value)
    {
        currentMoney += value;
        OnMoneyChanged?.Invoke();
    }
}