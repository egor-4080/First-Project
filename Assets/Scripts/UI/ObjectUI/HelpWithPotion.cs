using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpWithPotion : MonoBehaviour
{
    [SerializeField] private GameObject helpText;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        helpText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        helpText.SetActive(false);
    }
}
