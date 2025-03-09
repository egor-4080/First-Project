using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AudioSliderStart : MonoBehaviour
{ 
    [SerializeField] private GameObject settingsPanel;
    
    private void Start()
    {
        settingsPanel.SetActive(false);
    }
}
