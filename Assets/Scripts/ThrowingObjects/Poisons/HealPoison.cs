using UnityEngine;

public class HealPoison : Potion
{
    [SerializeField] private float healthPoints;

    public override void DoEffectWithBody(Collider2D body)
    {
        base.DoEffectWithBody(body);
        if (body.TryGetComponent(out Health character))
        {
            character.HealCharacter(healthPoints);
        }
    }

    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion(player);

        if (!isDrunk)
        {
            player.GetComponent<Health>().HealCharacter(healthPoints);
        }
        isDrunk = true;
    }
}