using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerContoller : Character
{
    [SerializeField] private Texture2D cursor;
    [SerializeField] private Transform weaponSoket;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform throwStartPoint;
    [SerializeField] private GameObject hpBar;

    [SerializeField] private float boost;

    private bool fireActive;
    private bool isFacingRight;
    private bool isControl = true;
    private bool isDead;

    private float angle;

    private Vector2 direction;
    private Vector3 difference;
    private Vector3 mousePosition;
    private Vector3 mouseInput;

    private PlayerSpawner playerSpawner;
    private Throw throwScript;
    private Collider2D[] takingObjects;
    private GameObject currentTakeObject;
    private Health player;
    private PhotonView photon;
    private Inventory inventoryClass;
    private Item equipedItem;
    private ItemBox equipedItemBox;
    private Weapon weapon;
    private SpriteRenderer spriteSettings;

    private void Start()
    {
        if (!photonView.IsMine) return;
        isDead = false;
        Cursor.SetCursor(cursor, new Vector2(12.5f, 20), CursorMode.Auto);
        spriteSettings.sortingOrder = 10;
    }

    protected override void Awake()
    {
        base.Awake();

        spriteSettings = GetComponent<SpriteRenderer>();
        inventoryClass = GetComponent<Inventory>();
        photon = GetComponent<PhotonView>();
        player = GetComponent<Health>();
        throwScript = GetComponent<Throw>();
    }

    private void Update()
    {
        bool isMouseOverUI = IsMouseOverUI();
        
        if(!photon.IsMine || !isControl)
        {
            return;
        }
        mouseInput = Mouse.current.position.ReadValue();
        mousePosition = cameraMain.ScreenToWorldPoint(mouseInput);
        mousePosition.z = 0;

        Ray ray = Camera.main.ScreenPointToRay(mouseInput);
        RaycastHit2D hit =  Physics2D.Raycast(ray.origin, ray.direction * 100);
        //print(hit?hit.collider.gameObject.name: "null");

        isFacingRight = mousePosition.x > transform.position.x;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
        hpBar.transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
        RotateGun();
        

        if(isMouseOverUI)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursor, new Vector2(12.5f, 20), CursorMode.Auto);
        }
        if (fireActive && isMouseOverUI == false)
        {
            weapon?.TryFire(isFacingRight);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        if (this.weapon != null && photon.IsMine)
        {
            PhotonNetwork.Destroy(this.weapon.gameObject);
        }

        this.weapon = weapon;
        weapon.transform.SetParent(weaponSoket);
        weapon.transform.localPosition = Vector3.zero;
    }

    private bool IsMouseOverUI()
    {
        if (!photonView.IsMine) return false;
        
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;  // ���������� true, ���� ���� ������� UI ��� ������
    }

    public void SpeedEffect()
    {
        speedForce += boost;
        Invoke("OffSpeedEffect", 5);
    }

    private void OffSpeedEffect()
    {
        speedForce -= boost;
    }

    public void Initialization(Camera playerCamera, PlayerSpawner playerSpawner, Transform content)
    {
        cameraMain = playerCamera;
        this.playerSpawner = playerSpawner;
        inventoryClass.Init(content);
    }

    public void SetControl(bool value)
    {
        isControl = value;
        if (value)
        {
            Cursor.SetCursor(cursor, new Vector2(12.5f, 20), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
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
        difference = (mousePosition - weaponSoket.position);
        angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (isFacingRight == false)
        {
            angle += 180;
        }
        weaponSoket.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FixedUpdate()
    {
        if (!photon.IsMine || isDead)
        {
           rigitBody.velocity = Vector2.zero;
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

    public void SelectItem(Item item, ItemBox itemBox)
    {
        equipedItemBox = itemBox;
        equipedItem = item;
    }

    public void DropFromInventory(Item item)
    {
        throwScript.DropObject(item.gameObject);
    }

    public override void OnDeath()
    {
        if (!photon.IsMine) return;
        
        if (weapon)
            PhotonNetwork.Destroy(weapon.gameObject);

        isDead = true;
        animator.SetBool("IsDead", true);
        SetControl(false);
        playerSpawner.PlayerRespawn();
        inventoryClass.OnPlayerDeath();
        Invoke(nameof(DestroyDeadPlayer), 5);
    }

    private void DestroyDeadPlayer()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public void OnTake(InputAction.CallbackContext context)
    {
        if (!photon.IsMine || !isControl)
        {
            return;
        }

        FindAllTakeObjectsAroundPlayer();
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (!photon.IsMine || !isControl)
        {
            return;
        }

        if (equipedItem != null)
        {
            GameObject item = equipedItem.gameObject;
            Potion poisonScript = item.GetComponent<Potion>();
            poisonScript.DoWhenUseMotion(this);
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!photon.IsMine || !isControl)
        {
            return;
        }

        if (equipedItem != null)
        {
            throwScript.ThrowObject(equipedItem.gameObject, difference);
            inventoryClass.DeleteItem(equipedItemBox);
            equipedItem = null;
        }
    }
}