using Photon.Realtime;
using UnityEngine;
using System.Linq;

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
        objects.ToList()
            .Select(collider => collider.GetComponent<PlayerContoller>())
            .Where(player => player != null)
            .ToList().ForEach(player => player.SpeedEffect());
    }
}