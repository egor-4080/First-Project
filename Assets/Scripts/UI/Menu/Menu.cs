using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    public enum Screens
    {
        Connect = 0,
        Wait,
        Rooms
    }

    public void SetScreen(Screens screen)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == (int)screen);
        }
    }
}