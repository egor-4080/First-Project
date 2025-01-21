using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float coolDown;
    [SerializeField] private GameObject square;

    public void OnSquareGetOnTrigger(Collider2D other)
    {
        StartCoroutine(nameof(WaitForNextAttack));
        other.gameObject.SendMessageUpwards("IsHuman", false, SendMessageOptions.DontRequireReceiver);
        other.gameObject.SendMessageUpwards("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
    }

    private IEnumerator WaitForNextAttack()
    {
        yield return new WaitForSeconds(coolDown);
        square.SetActive(true);
    }
}