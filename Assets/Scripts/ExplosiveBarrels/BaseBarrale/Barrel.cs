using System;
using System.Collections.Generic;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public abstract class Barrel : MonoBehaviour
{   
    public bool isActive { get; private set; } = true;
    
    [SerializeField] private GameObject exploisonPrefab;
    
    private float respawnTime = 2;
    private PhotonView photon;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    public void Exployed()
    {
        Instantiate(exploisonPrefab, transform.position, Quaternion.identity);
        StartCoroutine(nameof(WaitForRespawnBarrel));
    }

    public void TakeDamage(float damage)
    {
        Exployed();
    }
    
    private IEnumerator WaitForRespawnBarrel()
    {
        photon.RPC(nameof(ExployEffects), RpcTarget.AllBuffered);
        yield return new WaitForSeconds(respawnTime);
        photon.RPC(nameof(RespawnEffects), RpcTarget.AllBuffered);
    }
    
    [PunRPC]
    public void ExployEffects()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
    
    [PunRPC]
    public void RespawnEffects()
    {
        isActive = true;
        gameObject.SetActive(true);
    }
}