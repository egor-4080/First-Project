using UnityEngine;

public class DamagePoison : Poison
{
    [SerializeField] private float exploisonDamage;

    public override void DoEffectWithBody(Collider2D body)
    {
        base.DoEffectWithBody(body);
        if (body.TryGetComponent(out Character character))
        {
            //character.TakeDamage(exploisonDamage);
        }
    }

    public override void DoWhenUseMotion(Health player) { }
}