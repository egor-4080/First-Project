using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class HelpWithPotion : MonoBehaviour
{
    private PhotonView photon;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item) && photon.IsMine)
        {
            item.TurnHelpText(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item) && photon.IsMine)
        {
            item.TurnHelpText(false);
        }
    }
}
