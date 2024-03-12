using UnityEngine;

public class ExploisonController : MonoBehaviour
{
    [SerializeField] float destroyTime;

    private RedBarrel barrel;

    void Start()
    {
        gameObject.SetActive(false);
        barrel = GetComponentInParent<RedBarrel>();
    }

    public void SetExploisenStatus()
    {
        gameObject.SetActive(true);
        barrel.Invoke("SetBarrelStatus", destroyTime);
    }
}