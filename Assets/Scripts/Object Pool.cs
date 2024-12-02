using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;

    private List<BulletController> pooledObjects;
    private PhotonView photon;

    private void Awake()
    {
        photon = GetComponent<PhotonView>();
    }

    private void Start()
    {
        pooledObjects = new List<BulletController>(amountToPool);
        if (photon.IsMine)
        {
            GameObject temp;
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                temp = PhotonNetwork.Instantiate(objectToPool.name, Vector2.zero, Quaternion.identity);
                pooledObjects.Add(temp.GetComponent<BulletController>());
            }
        }
    }

    public BulletController GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        GameObject temp = PhotonNetwork.Instantiate(objectToPool.name, Vector2.zero, Quaternion.identity);
        pooledObjects.Add(temp.GetComponent<BulletController>());
        return pooledObjects.Last();
    }
}
