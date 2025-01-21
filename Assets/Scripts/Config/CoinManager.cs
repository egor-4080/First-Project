using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private float currentMoney; //должен взять деньги из базы данных

    public void AddMoney(float addedMoney)
    {
        currentMoney += addedMoney;
    }

    public bool CanBuySmth(float price)
    {
        if (currentMoney - price < 0)
            return false;
        else
            currentMoney -= price;
        return true;
    }
}