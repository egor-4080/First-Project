using UnityEngine;

public class HealPoison : Potion
{
    [SerializeField] private float healthPoints;

    public override void DoEffectWithBody()
    {
        base.DoWhenUseMotion();
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