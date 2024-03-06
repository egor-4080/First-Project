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

    private Vector2 direction;
    private Vector3 difference;
    private Vector3 mousePosition;
    private Vector3 mouseInput;

    private Rigidbody2D rigitBody;

    private void Awake()
    {
        rigitBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mouseInput = Mouse.current.position.ReadValue(); 
        mousePosition = cameraMain.ScreenToWorldPoint(mouseInput);
        mousePosition.z = 0;

        isFacingRight = mousePosition.x > transform.position.x;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
        RotateGun();
        SetMoveAnimation();

        if (fireActive)
        {
            weapon.Fire(isFacingRight);
        }
    }

    public void SetMoveAnimation()
    {
        animator.SetBool("IsStatic", isStatic);
        animator.SetBool("IsTurnByX", isTurnByX);
        animator.SetBool("IsTurnByY", isturnByY);
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
        print("A");
        direction = context.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            isStatic = false;

            if(direction.x != 0)
            {
                isTurnByX = true;
            }
            else
            {
                isTurnByX = false;

                if(direction.y > 0)
                {
                    isturnByY = true;
                }
                else
                {
                    isturnByY = false;
                }
            }
        }
        else
        {
            isStatic = true;
        }
    }

    public virtual void OnFire(InputAction.CallbackContext context)
    {
        fireActive = context.ReadValueAsButton();
    }
}