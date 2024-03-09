using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerContoller : Character
{
    [SerializeField] private Transform weaponSoket;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Animator animator;
    [SerializeField] private float burden;

    private bool fireActive;
    private bool isFacingRight;
    private bool throwAction;

    private int mathForIsFacing;

    private Vector2 direction;
    private Vector3 difference;
    private Vector3 mousePosition;
    private Vector3 mouseInput;

    private Rigidbody2D rigitBody;
    private ThrowAndTake throwAndTake;

    private void Awake()
    {
        rigitBody = GetComponent<Rigidbody2D>();
        throwAndTake = GetComponent<ThrowAndTake>();
    }

    private void Update()
    {
        mouseInput = Mouse.current.position.ReadValue(); 
        mousePosition = cameraMain.ScreenToWorldPoint(mouseInput);
        mousePosition.z = 0;

        isFacingRight = mousePosition.x > transform.position.x;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
        RotateGun();

        if (fireActive)
        {
            weapon.Fire(isFacingRight);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BaseComponent trowingObject))
        {
            var collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }
    }

    public virtual void RotateGun()
    {
        difference = (mousePosition - weaponSoket.position).normalized;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (isFacingRight == false)
        {
            angle += 180;
        }

        weaponSoket.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        rigitBody.velocity = direction * speed;
    }

    public virtual void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            animator.SetBool("IsStatic", false);

            if (direction.x != 0)
            {
                animator.SetBool("IsTurnByX", true);
            }
            else
            {
                animator.SetBool("IsTurnByX", false);
                if (direction.y > 0)
                {
                    animator.SetBool("IsTurnByY", true);
                }
                else
                {
                    animator.SetBool("IsTurnByY", false);
                }
            }
        }
        else
        {
            animator.SetBool("IsStatic", true);
        }
    }

    public virtual void OnFire(InputAction.CallbackContext context)
    {
        fireActive = context.ReadValueAsButton();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (isFacingRight) mathForIsFacing = 1;
        else               mathForIsFacing = -1;

        throwAction = context.ReadValueAsButton();
        throwAndTake.SetBooleanValues(throwAction, mathForIsFacing);
    }
}