using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CreateButtonEventRegister : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_Dropdown maxPlayersInput;

    private Button confirmButton;

    private void Awake()
    {
        confirmButton = GetComponent<Button>();
    }

    private void CreateRoom()
    {
        roomManager.CreateRoom(int.Parse(maxPlayersInput.options[maxPlayersInput.value].text), passwordInput.text);
    }

    private void OnEnable()
    {
        confirmButton.onClick.AddListener(CreateRoom);
    }
}
