using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyController : MonoBehaviourPunCallbacks
{
    private EnemyController[] enemies;

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetEnemiesSetting();
    }

    private void SetEnemiesSetting()
    {
        enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        foreach (EnemyController enemy in enemies)
        {
            if (enemy.enabled == false)
            {
                enemy.enabled = true;
                enemy.FindMasterToStartMoving();
            }
        }
    }
}
