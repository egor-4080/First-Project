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
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float spreadAngle;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private bool needEffect;

    protected PhotonView photonView;
    protected BulletPool objectPool;
    private CinemachineImpulseSource impulseSource;
    private AudioSource fireAudio;
    private float lastFireTime;

    protected virtual void Awake()
    {
        objectPool = FindFirstObjectByType<BulletPool>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        fireAudio = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
    }

    public void TryFire(bool isFacing)
    {
        if (Time.time > lastFireTime + fireRate)
        {
            lastFireTime = Time.time;
            Fire(isFacing);
        }
    }

    protected virtual void Fire(bool isFacing)
    {
        impulseSource.GenerateImpulse();
        if (needEffect)
        {
            photonView.RPC(nameof(OnEffect), RpcTarget.All);   
        }
    }

    [PunRPC]
    public void OnEffect()
    {
        fireAudio.Play();
        fireEffect.SetActive(true);
        Invoke(nameof(OffEffect), 0.1f);
    }
    
    private void OffEffect()
    {
        fireEffect.SetActive(false);
    }

    public void GetFastShoot(float fastShootTime)
    {
        fireRate /= 2;
        Invoke(nameof(OffFastShoot), fastShootTime);
    }

    private void OffFastShoot()
    {
        fireRate *= 2;
    }
}