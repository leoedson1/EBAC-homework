using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public KeyCode interactKeyCode = KeyCode.E;
    public Animator chestAnim;
    public Animator tooltipAnim;
    public string triggerOpen = "OpenChest";
    public string triggerTT = "TriggerTooltip";

    [Header("Tooltip")]
    public GameObject tooltip;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;
    
    [Space]
    public ChestItemBase chestItem;
    
    private float _startScale;
    private bool _chestOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        _startScale = tooltip.transform.localScale.x;
        HideTooltip();
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpen) return;

        _chestOpen = true;
        chestAnim.SetTrigger(triggerOpen);
        tooltipAnim.SetTrigger(triggerTT);
        Invoke(nameof(ShowItem), .5f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), .5f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    private void OnTriggerEnter(Collider other) 
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null && !_chestOpen)
        {
            ShowTooltip();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null)
        {
            HideTooltip();
        }
    }

    [NaughtyAttributes.Button]
    private void ShowTooltip()
    {
        tooltip.SetActive(true);
        tooltip.transform.localScale = Vector3.zero;
        tooltip.transform.DOScale(_startScale, tweenDuration);
    }

    [NaughtyAttributes.Button]
    private void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKeyCode) && tooltip.activeSelf)
        {
            OpenChest();
        }
    }
}
