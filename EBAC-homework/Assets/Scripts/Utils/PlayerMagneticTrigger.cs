using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        CollectableItemBase i = other.transform.GetComponent<CollectableItemBase>();
        if(i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}
