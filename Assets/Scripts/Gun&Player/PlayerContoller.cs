using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerContoller : Character
{
    [SerializeField] private Transform weaponSoket;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float burden;

    private bool fireActive;
    private bool isFacingRight;
    private Vector3 difference;
    private Vector2 direction;
    private Vector3 mousePosition;
    private Rigidbody2D rigitBody;

    private void Awake()
    {
        rigitBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print(mousePosition);
        mousePosition.z = 0;
        isFacingRight = mousePosition.x > transform.position.x;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
        RotateGun();
        if (fireActive)
        {
            weapon.Fire(isFacingRight);
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
    }

    public virtual void OnFire(InputAction.CallbackContext context)
    {
        fireActive = context.ReadValueAsButton();
    }
}