using UnityEngine;

public class FreezPoison : Poison
{
    [SerializeField] private float unFreezWait;

    public override void DoEffectWithBody(Collider2D body)
    {
        base.DoEffectWithBody(body);
        if (body.TryGetComponent(out Character character))
        {
            character.FreezCharacter(unFreezWait);
        }
    }

    public override void DoWhenUseMotion(Health player) { }
}