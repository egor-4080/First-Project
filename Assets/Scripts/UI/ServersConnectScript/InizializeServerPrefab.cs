using TMPro;
using UnityEngine;

public class InizializeServerPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text players;
    [SerializeField] private TMP_Text isLocked;
    [SerializeField] private TMP_Text ping;

    public void Init(string name, int players, int maxPlayers, bool isLocked, float ping)
    {
        
    }
}
