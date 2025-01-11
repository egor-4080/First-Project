using UnityEngine;

public class SpeedPotion : Potion
{
    public override void DoEffectWithBody()
    {
        base.DoEffectWithBody();
    }

    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion();

        if (!isDrunk && !throwObjectScript.IsBreak())
        {
            player.SpeedEffect();
        }
        isDrunk = true;
    }
}