using System.Collections;
using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject throwEffect;

    protected Collider2D[] bodiesForEffect;
    private Collider2D currentCollider;
    private AudioSource drinkAudio;
    private Rigidbody2D rigitBody;
    private WaitForSeconds waitSound;

    protected bool isBreak;
    private float forceSpeed;
    protected bool isDrunk;

    private void Awake()
    {
        drinkAudio = GetComponent<AudioSource>();
        currentCollider = GetComponent<Collider2D>();
        rigitBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        forceSpeed = 80;
        waitSound = new WaitForSeconds(1);
    }

    /*public void Throw(Vector3 direction)
    {
        rigitBody.velocity = direction * forceSpeed;
    }*/

    /*public void Initialization(bool isBreak)
    {
        if (this.isBreak != isBreak)
        {
            this.isBreak = true;
            bodiesForEffect = Physics2D.OverlapCircleAll(transform.position, 2);
            Instantiate(throwEffect, transform.position, Quaternion.Euler(0, 0, 0));
            OnBreak(bodiesForEffect);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDrunk && !isBreak && !collision.gameObject.TryGetComponent(out PlayerContoller player))
        {
            bodiesForEffect = Physics2D.OverlapCircleAll(transform.position, 2);
            Instantiate(throwEffect, transform.position, Quaternion.Euler(0, 0, 0));
            OnBreak(bodiesForEffect);
        }
        if (collision.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            //enemyController.TakeDamage(damage);
        }
        currentCollider.isTrigger = true;
        isBreak = true;

    }

    public Collider2D[] FindAllobjects()
    {
         return Physics2D.OverlapCircleAll(transform.position, 2);
    }

    protected void OnBreak(Collider2D[] bodiesForEffect)
    {
        foreach (var body in bodiesForEffect)
        {
            DoEffectWithBody(body);
        }
    }*/

    public virtual void DoEffectWithBody(Collider2D body) { }
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