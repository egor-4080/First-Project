using UnityEngine;

public class SpeedExploisonController : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float radius;

    private Collider2D[] objects;

    private void Start()
    {
        Destroy(gameObject, timer);
        GiveSpeedEffectToCharacters();
    }

    private void GiveSpeedEffectToCharacters()
    {
        objects = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in objects)
        {
            if (collider.gameObject.TryGetComponent(out PlayerContoller player))
            {
                player.SpeedEffect();
            }
        }
    }
}