using UnityEngine;

public class HealPoison : Poison
{
    [SerializeField] private float healthPoints;

    protected override void DoEffectWithBody(Collider2D body)
    {
        if (body.TryGetComponent(out PlayerContoller player))
        {
            player.HealPlayer(healthPoints);
        }
    }
}