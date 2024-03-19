using UnityEngine;

public class SpeedPoison : Poison
{
    protected override void DoEffectWithBody(Collider2D body)
    {
        if (body.TryGetComponent(out Character character))
        {
            character.SpeedEffect();
        }
    }

    public override void DoWhenUseMotion(PlayerContoller player)
    {
        if(!isDrunk)
        {
            player.SpeedEffect();
        }
        isDrunk = true;
    }
}