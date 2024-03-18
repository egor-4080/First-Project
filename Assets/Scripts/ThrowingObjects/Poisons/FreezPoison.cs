using UnityEngine;

public class FreezPoison : Poison
{
    protected override void DoEffectWithBody(Collider2D body)
    {
        if (body.TryGetComponent(out Character character))
        {
            character.FreezCharacter();
        }
    }
}