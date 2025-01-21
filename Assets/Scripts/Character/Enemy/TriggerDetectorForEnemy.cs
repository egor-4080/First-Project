using UnityEngine;

public class TriggerDetectorForEnemy : MonoBehaviour
{
    [SerializeField] private EnemyAttack enemyAttack;

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemyAttack.OnSquareGetOnTrigger(other);
        gameObject.SetActive(false);
    }
}