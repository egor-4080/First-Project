using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float weight;
    [SerializeField] protected GameObject fireEffect;

    protected CinemachineImpulseSource impulseSource;
    protected AudioSource fireAudio;
    protected float lastFireTime;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        fireAudio = GetComponent<AudioSource>();
    }

    public virtual void Fire(bool isFacing)
    {
        impulseSource.GenerateImpulse();
        fireEffect.SetActive(true);
        Invoke("OffEffect", 0.1f);
        fireAudio.Play();
    }
    
    private void OffEffect()
    {
        fireEffect.SetActive(false);
    }
}
