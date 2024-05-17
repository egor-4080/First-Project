using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform throwStartPoint;
    [SerializeField] private GameObject itemBox;

    private List<ItemBox> items = new();
    private Transform content;
    private PhotonView photon;

    private void Awake()
    {
        content = GameObject.FindGameObjectWithTag("Content").transform;
        photon = GetComponent<PhotonView>();
    }

    public void AddItem(Item item)
    {
        PhotonView takenObjectPhoton = item.GetComponent<PhotonView>();
        int id = takenObjectPhoton.ViewID;
        ItemBox itemBox = Instantiate(this.itemBox, content)
            .GetComponent<ItemBox>();
        itemBox.Init(item);
        items.Add(itemBox);

        photon.RPC(nameof(SetTakenObjectParameters), RpcTarget.All, id);
    }

    [PunRPC]
    public void SetTakenObjectParameters(int id)
    {
        PhotonView photonObject = PhotonView.Find(id);
        Collider2D takingObject = photonObject.gameObject.GetComponent<Collider2D>();

        takingObject.isTrigger = false;
        takingObject.gameObject.SetActive(false);
        takingObject.transform.SetParent(throwStartPoint);
        takingObject.transform.localPosition = Vector3.zero;
    }

    /*public void OnThrow(InputAction.CallbackContext context)
    {
        if (inventory.Count != 0 && throwScript.CouldThrow())
        {
            throwScript.SetValues(inventory[0], difference);
            inventory.RemoveAt(0);
            throwScript.ThrowObject();
        }
    }*/
}
