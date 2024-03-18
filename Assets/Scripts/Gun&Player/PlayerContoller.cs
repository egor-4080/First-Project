using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : Character
{
    [SerializeField] private List<GameObject> throwingObjects;
    [SerializeField] private Transform weaponSoket;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform throwStartPoint;

    private bool fireActive;
    private bool isFacingRight;

    private int mathForIsFacing;
    private float angle;

    private Vector2 direction;
    private Vector3 difference;
    private Vector3 mousePosition;
    private Vector3 mouseInput;

    private Throw throwAndTake;
    private Collider2D[] takingObjects;
    private GameObject currentTakeObject;

    protected override void Awake()
    {
        base.Awake();

        throwAndTake = GetComponent<Throw>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision) { }

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

    public void Initialization(Camera playerCamera)
    {
        cameraMain = playerCamera;
    }

    //Bonus!

    //1
    public void HealPlayer(float heal)
    {
        currentHealthPoints += heal;
        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
    }

    //2

    //3

    //4

    //5

    //6

    //EndBonus

    private void FindAllTakeObjectsAroundPlayer()
    {
        takingObjects = Physics2D.OverlapCircleAll(transform.position, 1);
        if (takingObjects != null)
        {
            foreach (var takingObject in takingObjects)
            {
                currentTakeObject = takingObject.gameObject;
                if (currentTakeObject.TryGetComponent(out Poison couldThrow))
                {
                    takingObject.isTrigger = false;
                    takingObject.gameObject.SetActive(false);
                    throwingObjects.Add(currentTakeObject);
                    takingObject.transform.SetParent(throwStartPoint);
                    takingObject.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    public virtual void RotateGun()
    {
        difference = (mousePosition - weaponSoket.position).normalized;
        angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (isFacingRight == false)
        {
            angle += 180;
        }
        weaponSoket.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        rigitBody.velocity = direction * speedForce;

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

    public void OnTake(InputAction.CallbackContext context)
    {
        FindAllTakeObjectsAroundPlayer();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (isFacingRight) mathForIsFacing = 1;
        else mathForIsFacing = -1;

        throwAndTake.SetValues(mathForIsFacing, throwingObjects, difference);
        throwAndTake.StartCoroutines();
    }
}