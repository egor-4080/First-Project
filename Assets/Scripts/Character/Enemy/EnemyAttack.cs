using System.Collections;
using Photon.Pun;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float coolDown;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;
    private bool canAttack = true;

    private PhotonView photon;
    private float saveDamage;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
        LocalInit();
    }

    private void Start()
    {
        saveDamage = damage;
    }

    private void FixedUpdate()
    {
        if (!canAttack) return;
        var players = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        if (players.Length == 0) return;
        canAttack = false;
        foreach (var player in players)
        {
            OnSquareGetOnTrigger(player);
        }
    }

    public void ChangeDamage(float damage)
    {
        photon.RPC(nameof(NetworkChange), RpcTarget.All, damage);
    }

    [PunRPC]
    public void NetworkChange(float damage)
    {
        if (damage == -1)
        {
            this.damage = saveDamage;
        }
        else
        {
            this.damage = damage;
        }
    }

    private void LocalInit()
    {
        var dictionary = Config.instance.configStats["EnemyDictionary"];
        damage += dictionary["damage"];
    }

    private void OnSquareGetOnTrigger(Collider2D other)
    {
        other.gameObject.SendMessageUpwards("IsHuman", false, SendMessageOptions.DontRequireReceiver);
        other.gameObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        StartCoroutine(nameof(WaitForNextAttack));
    }

    private IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
}