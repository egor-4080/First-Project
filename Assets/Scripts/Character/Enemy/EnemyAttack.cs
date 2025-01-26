using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float coolDown;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;

    private bool canAttack = true;

    private void Awake()
    {
        LocalInit();
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