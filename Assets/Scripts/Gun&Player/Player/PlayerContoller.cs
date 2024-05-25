using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : Character
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] private List<GameObject> inventory;
    [SerializeField] private Transform weaponSoket;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform throwStartPoint;

    [SerializeField] private float boost;
    [SerializeField] private float unSpeedWait;

    private bool fireActive;
    private bool isFacingRight;

    private float angle;

    private Vector2 direction;
    private Vector3 difference;
    private Vector3 mousePosition;
    private Vector3 mouseInput;

    private Throw throwScript;
    private Collider2D[] takingObjects;
    private GameObject currentTakeObject;
    private Health player;
    private PhotonView photon;
    private Inventory inventoryClass;
    private Item equipedItem;

    private void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(12.5f, 20), CursorMode.Auto);
    }

    protected override void Awake()
    {
        base.Awake();

        inventoryClass = GetComponent<Inventory>();
        photon = GetComponent<PhotonView>();
        player = GetComponent<Health>();
        throwScript = GetComponent<Throw>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision) { }

    private void Update()
    {
        if(!photon.IsMine)
        {
            return;
        }
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

    public void SpeedEffect()
    {
        if (TryGetComponent(out UnityEngine.AI.NavMeshAgent enemy))
        {
            enemy.speed += boost;
        }
        else
        {
            speedForce += boost;
        }
        Invoke("OffSpeedEffect", unSpeedWait);
    }

    private void OffSpeedEffect()
    {
        if (TryGetComponent(out UnityEngine.AI.NavMeshAgent enemy))
        {
            enemy.speed -= boost;
        }
        else
        {
            speedForce -= boost;
        }
    }

    public void Initialization(Camera playerCamera)
    {
        cameraMain = playerCamera;
    }

    private void FindAllTakeObjectsAroundPlayer()
    {
        takingObjects = Physics2D.OverlapCircleAll(transform.position, 1);
        if (takingObjects != null)
        {
            foreach (Collider2D takingObject in takingObjects)
            {
                currentTakeObject = takingObject.gameObject;
                if (currentTakeObject.TryGetComponent(out Item couldThrow))
                {
                    inventoryClass.AddItem(couldThrow);
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
        if (!photon.IsMine)
        {
            return;
        }

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

    public void SelectItem(Item item)
    {
        if (equipedItem != null)
        {
            equipedItem.gameObject.SetActive(false);
        }
        equipedItem = item;
        equipedItem.gameObject.SetActive(true);
    }

    public void OnTake(InputAction.CallbackContext context)
    {
        if (!photon.IsMine)
        {
            return;
        }

        FindAllTakeObjectsAroundPlayer();
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (!photon.IsMine)
        {
            return;
        }

        if (inventory.Count != 0)
        {
            GameObject poison = inventory[0];
            Poison poisonScript = poison.GetComponent<Poison>();
            poisonScript.DoWhenUseMotion(player);
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (inventory.Count != 0 && throwScript.CouldThrow())
        {
            throwScript.SetValues(inventory[0], difference);
            inventory.RemoveAt(0);
            throwScript.ThrowObject();
        }
    }
}