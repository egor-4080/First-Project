using System.Collections;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] protected Sprite emptyPoison;

    private AudioSource drinkAudio;
    private WaitForSeconds waitSound;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    protected bool isDrunk;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if(effect == null)
        {
            return;
        }
        Instantiate(effect, transform.position, Quaternion.identity);
        effect = null;
    }
    public virtual void DoWhenUseMotion(Health player)
    {
        spriteRenderer.sprite = emptyPoison;
        rigidBody.isKinematic = true;
        gameObject.SetActive(true);
        StartCoroutine(MusicEffect());
    }

    private IEnumerator MusicEffect()
    {
        drinkAudio.Play();
        yield return waitSound;
        gameObject.SetActive(false);
        rigidBody.isKinematic = false;
    }
}