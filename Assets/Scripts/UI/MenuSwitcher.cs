using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;

    public void SetScreen(GameObject panel)
    {
        Debug.Assert(panels.Contains(panel), "None panel in massive");
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
        }
        else
        {
            foreach (GameObject child in panels)
            {
                child.SetActive(false);
            }
            panel.SetActive(true);
        }
    }
}
