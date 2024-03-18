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

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        fireAudio = GetComponent<AudioSource>();
    }

    public virtual void Fire(bool isFacing)
    {
        OnEffect();
        impulseSource.GenerateImpulse();
        fireAudio.Play();
    }

    [PunRPC]
    private void OnEffect()
    {
        fireEffect.SetActive(true);
        Invoke("OffEffect", 0.1f);
    }
    
    private void OffEffect()
    {
        fireEffect.SetActive(false);
    }
}
