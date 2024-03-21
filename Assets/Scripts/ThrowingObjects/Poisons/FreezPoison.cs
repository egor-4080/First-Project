using UnityEngine;

public class FreezPoison : Poison
{
    [SerializeField] private float unFreezWait;

    protected override void DoEffectWithBody(Collider2D body)
    {
        if (body.TryGetComponent(out Character character))
        {
            character.FreezCharacter(unFreezWait);
        }
    }

    public override void DoWhenUseMotion(PlayerContoller player) { }
}