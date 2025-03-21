using UnityEngine;
using UnityEngine.Serialization;

public class HealPoison : Potion
{ 
    [SerializeField] private float healthSelfPoints;

    public override void DoEffectWithBody()
    {
        base.DoEffectWithBody();
    }
    public override void DoWhenUseMotion(PlayerContoller player)
    {
        base.DoWhenUseMotion(player);

        if (!isDrunk)
        {
            player.GetComponent<Health>().HealCharacter(healthSelfPoints);
        }
        isDrunk = true;
    }
}