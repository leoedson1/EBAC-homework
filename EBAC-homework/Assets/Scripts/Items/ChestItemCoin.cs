using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;

    private List<GameObject> _items = new List<GameObject>();

    public float randomRange = 1f;
    public float tweenEndTime = .5f;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    [NaughtyAttributes.Button]
    private void CreateItems()
    {
        for(int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(-randomRange, randomRange) + Vector3.right * Random.Range(-randomRange, randomRange);
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            _items.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach(var i in _items)
        {
            i.transform.DOLocalMoveY(2f, tweenEndTime).SetRelative();
            i.transform.DOScale(0, tweenEndTime/2).SetDelay(tweenEndTime/2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
