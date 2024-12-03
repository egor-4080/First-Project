using UnityEngine;
using Cinemachine;
using Photon.Pun;
using NUnit.Framework;
using UnityEngine.Pool;

[RequireComponent(typeof(CinemachineImpulseSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected float reloadSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float weight;
    [SerializeField] private GameObject fireEffect;

    protected PhotonView photonView;
    protected BulletPool objectPool;
    private CinemachineImpulseSource impulseSource;
    private AudioSource fireAudio;

    private void Awake()
    {
        objectPool = FindFirstObjectByType<BulletPool>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        fireAudio = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
    }

    public virtual void Fire(bool isFacing)
    {
        OnEffect();
        impulseSource.GenerateImpulse();
        fireAudio.Play();
        photonView.RPC(nameof(OnEffect), RpcTarget.Others);
    }

    [PunRPC]
    public void OnEffect()
    {
        fireEffect.SetActive(true);
        Invoke("OffEffect", 0.1f);
    }
    
    private void OffEffect()
    {
        fireEffect.SetActive(false);
    }
}