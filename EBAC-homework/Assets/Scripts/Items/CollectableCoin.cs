using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class CollectableCoin : CollectableItemBase
{
    public Collider cCollider;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddByType(ItemType.COIN);
        cCollider.enabled = false;
    }
}
