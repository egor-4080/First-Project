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

    protected ThrowingObjectController throwObjectScript;

    protected bool isDrunk;

    private void Awake()
    {
        throwObjectScript = GetComponent<ThrowingObjectController>();
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

    public virtual void DoWhenUseMotion()
    {
        spriteRenderer.sprite = emptyPoison;
        rigidBody.isKinematic = true;
        gameObject.SetActive(true);
        StartCoroutine(MusicEffect());
    }

    public virtual void DoWhenUseMotion(PlayerContoller player)
    {
    }

    public bool IsDrunk()
    {
        if (isDrunk)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Sprite GetEmptySprite()
    {
        return emptyPoison;
    }
    
    private IEnumerator MusicEffect()
    {
        drinkAudio.Play();
        yield return waitSound;
        gameObject.SetActive(false);
        rigidBody.isKinematic = false;
    }
}