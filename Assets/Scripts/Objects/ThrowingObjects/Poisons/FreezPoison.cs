using UnityEngine;

public class FreezPoison : Potion
{
    [SerializeField] private float unFreezWait;

    public override void DoEffectWithBody() { }
    public override void DoWhenUseMotion(PlayerContoller player) { }
}