using UnityEngine;

public class RedBarrel : Barrel
{
    private ExploisonController explosionController;

    public override void MakeExploisonEffect(GameObject exploison)
    {
        Destroy(gameObject);
        explosionController = exploison.GetComponent<ExploisonController>();
        explosionController.GetDamageExploison();
    }
}