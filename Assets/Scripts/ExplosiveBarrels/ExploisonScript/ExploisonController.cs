using UnityEngine;

public class ExploisonController : MonoBehaviour
{
    [SerializeField] float destroyTime;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}