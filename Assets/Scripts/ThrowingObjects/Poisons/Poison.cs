using System.Collections;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    private AudioSource drinkAudio;
    private WaitForSeconds waitSound;

    protected bool isDrunk;

    private void Awake()
    {
        drinkAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        waitSound = new WaitForSeconds(1);
    }

    public Collider2D[] FindAllobjects()
    {
         return Physics2D.OverlapCircleAll(transform.position, 2);
    }

    public virtual void DoEffectWithBody(Collider2D body)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
    public virtual void DoWhenUseMotion(Health player)
    {
        gameObject.SetActive(true);
        StartCoroutine(MusicEffect());
    }

    private IEnumerator MusicEffect()
    {
        drinkAudio.Play();
        yield return waitSound;
        gameObject.SetActive(false);
    }
}