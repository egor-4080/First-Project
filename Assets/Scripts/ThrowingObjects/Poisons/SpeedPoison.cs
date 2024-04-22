using UnityEngine;

public class SpeedPoison : Poison
{
    private PlayerContoller character;

    public override void DoEffectWithBody(Collider2D body)
    {
        base.DoEffectWithBody(body);
        if (body.TryGetComponent(out PlayerContoller player))
        {
            player.SpeedEffect();
        }
    }

    public override void DoWhenUseMotion(Health player)
    {
        base.DoWhenUseMotion(player);

        character = player.gameObject.GetComponent<PlayerContoller>();

        if (!isDrunk)
        {
            character.SpeedEffect();
        }
        isDrunk = true;
    }
}