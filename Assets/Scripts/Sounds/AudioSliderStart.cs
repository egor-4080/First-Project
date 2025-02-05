using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class AudioSliderStart : MonoBehaviour
{
    private void Start()
    {
        var button = GetComponentInParent<Button>();
        button.gameObject.SetActive(false);
    }
}
