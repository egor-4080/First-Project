using UnityEngine;

public class SpeedPoison : Poison
{
    protected override void DoEffectWithBody(Collider2D body)
    {
        if (body.TryGetComponent(out PlayerContoller player))
        {
            player.SpeedEffect();
        }
    }

    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion(player);

        if(!isDrunk)
        {
            player.SpeedEffect();
        }
        isDrunk = true;
    }
}