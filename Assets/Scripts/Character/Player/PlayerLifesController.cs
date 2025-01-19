using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerLifesController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int playerLifes;

    private Health playerHealth;

    public int GetCurrentLifes()
    {
        return playerLifes;
    }

    public void TakeLifeAway()
    {
        playerLifes--;
    }

    public void GiveLife()
    {
        playerLifes++;
    }

    public void Init(Health playerHealth)
    {
        this.playerHealth = playerHealth;
        playerHealth.OnDeath.AddListener(TakeLifeAway);
    }
}
