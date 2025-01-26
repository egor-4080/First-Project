using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GodMode : MonoBehaviour
{
    private Image image;
    private bool setter;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient) Destroy(gameObject);
    }

    public void SetGodMode()
    {
        var enemyAttacks = FindObjectsByType<EnemyAttack>(FindObjectsSortMode.None);
        setter = !setter;

        switch (setter)
        {
            case true:
            {
                image.color = Color.green;
                foreach (var enemyAttack in enemyAttacks)
                {
                    enemyAttack.ChangeDamage(0);
                }

                break;
            }
            case false:
            {
                image.color = Color.red;
                foreach (var enemyAttack in enemyAttacks)
                {
                    enemyAttack.ChangeDamage(-1);
                }

                break;
            }
        }
    }
}