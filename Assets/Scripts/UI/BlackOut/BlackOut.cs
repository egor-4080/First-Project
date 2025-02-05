using System.Collections;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
    [SerializeField] private UnityEvent OnBlackOutEnded;

    private Image image;
    private PhotonView photon;

    private void Awake()
    {
        image = GetComponent<Image>();
        photon = GetComponent<PhotonView>();
    }

    private void Start()
    {
        image.color = new Color(0, 0, 0, 1);
        StartCoroutine(nameof(SetDayBreak));
    }

    public void StartSettingBlackOut()
    {
        if (photon != null)
            photon.RPC(nameof(StartNetworkBlackOut), RpcTarget.All);
        else
            StartCoroutine(nameof(SetBlackOut));
    }

    [PunRPC]
    public void StartNetworkBlackOut()
    {
        StartCoroutine(nameof(SetBlackOut));
    }
    
    private IEnumerator SetDayBreak()
    {
        const float TIME = 1;
        float time = TIME;
        while (time > 0)
        {
            time -= Time.deltaTime;
            image.color = new Color(0, 0, 0, time);
            yield return null;
        }
    }
    
    private IEnumerator SetBlackOut()
    {
        const float TIME = 0;
        float time = TIME;
        while (time < 1)
        {
            time += Time.deltaTime;
            image.color = new Color(0, 0, 0, time);
            yield return null;
        }

        if (PhotonNetwork.IsMasterClient || !photon)
        {
            OnBlackOutEnded?.Invoke();
        }
    }
}