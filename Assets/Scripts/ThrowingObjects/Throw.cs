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

    public void ThrowObject()
    {
        if (isReload)
        {
            StartCoroutine(SpawnWithRate());
        }
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
        //throwingObjectBaseComponent.Throw(direction);
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