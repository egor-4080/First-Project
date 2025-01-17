using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShootPotion : Potion
{
    [SerializeField] private float fastShootTime;
    
    public override void DoEffectWithBody()
    {
        base.DoEffectWithBody();
    }
    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion(player);

        if (!isDrunk)
        {
            player.GetComponentInChildren<Weapon>().GetFastShoot(fastShootTime);
        }
        isDrunk = true;
    }
}
