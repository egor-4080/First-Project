using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField, Range(0, 5000)] protected float maxHealthPoints;
    [SerializeField] protected float speedForce;
    [SerializeField] protected float damage;

    private AudioSource takeDamageSound;
    private Quaternion lookRotation;
    protected Rigidbody2D rigitBody;
    private float currentHealthPoints;
    private bool isAlive = true;

    //animations integers
    protected int TurnByX;
    protected int TurnByY;

    protected virtual void Awake()
    {
        rigitBody = GetComponent<Rigidbody2D>();
        takeDamageSound = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerContoller player))
        {
            /*lookRotation.SetLookRotation(collision.transform.position);
            transform.rotation = lookRotation;
            rigitBody.AddForce(transform.right * -1 * 2000);
            transform.rotation = Quaternion.Euler(0, 0, 0);*/
            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(float takenDamage)
    {
        if (isAlive)
        {
            takeDamageSound.Play();
            currentHealthPoints -= takenDamage;
            if (currentHealthPoints <= 0)
            {
                Destroy(gameObject);
                isAlive = false;
            }
        }
    }
}