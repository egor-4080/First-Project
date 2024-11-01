using UnityEngine;
using Cinemachine;
using Photon.Pun;

[RequireComponent(typeof(CinemachineImpulseSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float weight;
    [SerializeField] protected GameObject fireEffect;
    
    protected CinemachineImpulseSource impulseSource;
    protected AudioSource fireAudio;
    protected PhotonView photonView;

    private void Awake()
    {
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