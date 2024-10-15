using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Transform throwingTransformPosition;
    [SerializeField] private float throwRate;

    private Collider2D throwingCollider;
    private ThrowingObjectController throwingObjectBaseComponent;
    private GameObject throwedObject;
    private PhotonView ThrownPhoton;
    private PhotonView photon;

    private bool isReload = true;
    private Vector2 direction;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    public bool CouldThrow()
    {
        return isReload;
    }

    public void ThrowObject(GameObject throwedObject, Vector3 direction)
    {
        if (isReload)
        {
            this.direction = direction;
            this.throwedObject = throwedObject;
            StartCoroutine(SpawnWithRate());
        }
    }
    
    public void DropObject(GameObject throwedObject)
    {
        this.direction = Vector2.zero;
        this.throwedObject = throwedObject;
        StartCoroutine(SpawnWithRate());
    }

    private IEnumerator SpawnWithRate()
    {
        isReload = false;

        ThrownPhoton = throwedObject.GetComponent<PhotonView>();
        throwingObjectBaseComponent = throwedObject.GetComponent<ThrowingObjectController>();
        throwingCollider = throwedObject.GetComponent<Collider2D>();

        int id = ThrownPhoton.ViewID;
        photon.RPC(nameof(SetThrownObjectState), RpcTarget.All, id);

        ThrownPhoton.RPC(nameof(throwingObjectBaseComponent.Throw), RpcTarget.All, direction);
        throwingCollider.isTrigger = false;

        yield return new WaitForSeconds(throwRate);
        isReload = true;
    }

    [PunRPC]
    private void SetThrownObjectState(int id)
    {
        PhotonView photonObject = PhotonView.Find(id);

        photonObject.gameObject.SetActive(true);
        photonObject.gameObject.transform.SetParent(null);
    }

    public void SetValues(GameObject throwedObject, Vector3 direction)
    {
        this.direction = direction;
        this.throwedObject = throwedObject;
    }
}