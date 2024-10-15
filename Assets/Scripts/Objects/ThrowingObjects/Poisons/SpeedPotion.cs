using UnityEngine;

public class SpeedPotion : Potion
{
    public override void DoEffectWithBody(Collider2D body)
    {
        base.DoEffectWithBody(body);
        if (body.TryGetComponent(out PlayerContoller player))
        {
            player.SpeedEffect();
        }
    }

    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion();

        //character = player.gameObject.GetComponent<PlayerContoller>();

        if (!isDrunk && !throwObjectScript.IsBreak())
        {
            player.SpeedEffect();
        }
        isDrunk = true;
    }
}