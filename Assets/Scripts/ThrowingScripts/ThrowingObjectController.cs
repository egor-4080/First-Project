using Photon.Pun;
using UnityEngine;

public class ThrowingObjectController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private Collider2D objectsCollider;
    private Rigidbody2D rigitBody;
    private Potion poison;
    private Item item;
    private bool isPoison;
    protected bool isBreak;

    private void Awake()
    {
        item = GetComponent<Item>();
        objectsCollider = GetComponent<Collider2D>();
        rigitBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
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
                OnBreak();
            }
        }
        if (!collision.gameObject.TryGetComponent(out PlayerContoller player2))
        {
            isBreak = true;
            if(isPoison)
            {
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                item.Used();
            }
        }
    }

    public void Initialization(bool isBreak)
    {
        if (this.isBreak != isBreak)
        {
            this.isBreak = true;
        }
    }

    protected void OnBreak()
    {
        if (!poison.IsDrunk())
        {
            poison.DoEffectWithBody();
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
            rigitBody.velocity = direction * 20;
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
