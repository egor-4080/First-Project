using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

public class PlayerContoller : Character
{
    [SerializeField] private List<GameObject> throwingObjects;
    [SerializeField] private Transform weaponSoket;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Animator animator;

    private bool fireActive;
    private bool isFacingRight;
    private bool isTake;

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

    override public void DoAnotherStart() { }

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isTake)
        {
            if (throwingObjects.Count != 5 && TryGetComponent(out Rigidbody2D isThrow))
            {
                collision.isTrigger = false;
                collision.transform.position = new Vector3(1000, 1000, 0);
                throwingObjects.Add(collision.gameObject);
            }
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

        TurnByX = (int)rigitBody.velocity.x;
        TurnByY = (int)rigitBody.velocity.y;

        animator.SetInteger("TurnX", TurnByX);
        animator.SetInteger("TurnY", TurnByY);
    }

    public virtual void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public virtual void OnFire(InputAction.CallbackContext context)
    {
        fireActive = context.ReadValueAsButton();
    }

    public void OnTake2(InputAction.CallbackContext context)
    {
        isTake = context.ReadValueAsButton();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (isFacingRight) mathForIsFacing = 1;
        else               mathForIsFacing = -1;

        throwAndTake.SetValues(mathForIsFacing, throwingObjects);
        throwAndTake.StartCoroutines();
    }
}