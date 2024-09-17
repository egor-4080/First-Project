using Photon.Pun;
using UnityEngine;

public class ThrowingObjectController : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private AudioSource audioSource;

    private Collider2D objectsCollider;
    private Rigidbody2D rigitBody;
    private Poison poison;
    private bool isPoison;
    protected bool isBreak;
    private float forceSpeed;

    private void Awake()
    {
        objectsCollider = GetComponent<Collider2D>();
        rigitBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        forceSpeed = 80;
        if (TryGetComponent(out poison))
        {
            isPoison = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigitBody.drag = 20;
        objectsCollider.isTrigger = true;
        if (isPoison && !collision.gameObject.TryGetComponent(out PlayerContoller player1))
        {
            if (!isBreak)
            {
                OnBreak(poison.FindAllobjects());
            }
        }
        if (!collision.gameObject.TryGetComponent(out PlayerContoller player2))
        {
            isBreak = true;
            if(isPoison)
            {
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.sprite = poison.GetEmptySprite();
            }
        }
        //Damage
    }

    public void Initialization(bool isBreak)
    {
        if (this.isBreak != isBreak)
        {
            this.isBreak = true;
        }
    }

    protected void OnBreak(Collider2D[] bodiesForEffect)
    {
        damage = 0;
        if (!poison.IsDrunk())
        {
            foreach (var body in bodiesForEffect)
            {
                poison.DoEffectWithBody(body);
            }
        }
    }

    [PunRPC]
    public void Throw(Vector2 direction)
    {
        rigitBody.bodyType = RigidbodyType2D.Dynamic;
        if (isBreak)
        {
            objectsCollider.isTrigger = true;
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            rigitBody.velocity = direction * forceSpeed;
        }
    }

    public bool IsBreak()
    {
        if(isBreak)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
