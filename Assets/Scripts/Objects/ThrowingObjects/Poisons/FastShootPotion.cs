using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShootPotion : Potion
{
    public override void DoEffectWithBody()
    {
        base.DoEffectWithBody();
    }
    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion(player);

        if (!isDrunk)
        {
            //player.GetComponent<PotionBoosts>().
        }
        isDrunk = true;
    }
}
