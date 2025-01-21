using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private float currentMoney; //должен взять деньги из базы данных

    public void TakeMoney(float tookedMoney)
    {
        currentMoney += tookedMoney;
    }

    public bool CanBuySmth(float price)
    {
        if (currentMoney - price < 0)
            return false;
        else
            return true;
    }
}