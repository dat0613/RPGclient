using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeCollider : MonoBehaviour
{
    LinkedList<GameObject> objectList = new LinkedList<GameObject>();

    public void SetActive(bool active)
    {
        transform.gameObject.SetActive(active);
    }

    public LinkedList<GameObject> GetGameObjectInCollider()
    {
        return objectList;
    }

    void OnTriggerEnter(Collider other)
    {
        objectList.AddLast(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        objectList.Remove(other.gameObject);
    }
}
