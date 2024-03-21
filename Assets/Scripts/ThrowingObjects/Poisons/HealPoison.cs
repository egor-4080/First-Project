using UnityEngine;

public class HealPoison : Poison
{
    [SerializeField] private float healthPoints;

    protected override void DoEffectWithBody(Collider2D body)
    {
        if (body.TryGetComponent(out Character character))
        {
            character.HealCharacter(healthPoints);
        }
    }

    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion(player);

        if (!isDrunk)
        {
            player.HealCharacter(healthPoints);
        }
        isDrunk = true;
    }
}