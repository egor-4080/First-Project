using UnityEngine;

public class HealPoison : Poison
{
    [SerializeField] private float healthPoints;

    protected override void DoEffectWithBody(Collider2D body)
    {
        if (body.TryGetComponent(out Health character))
        {
            character.HealCharacter(healthPoints);
        }
    }

    public override void DoWhenUseMotion(Health player)
    {
        base.DoWhenUseMotion(player);

        if (!isDrunk)
        {
            player.HealCharacter(healthPoints);
        }
        isDrunk = true;
    }
}