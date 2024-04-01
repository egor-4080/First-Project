using UnityEngine;

public class RedBarrel : Barrel
{
    public override void MakeExploisonEffect(GameObject exploison)
    {
        base.MakeExploisonEffect(exploison);

        explosionController.GetDamageExploison();
    }
}