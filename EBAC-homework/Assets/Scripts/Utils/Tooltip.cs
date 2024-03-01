using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tooltip : MonoBehaviour
{
    public Animator tooltipAnim;
    public GameObject tooltip;
    public Transform lookTarget;
    
    void Start()
    {

    }

    void Update()
    {
        if (lookTarget != null)
        {
            tooltip.transform.LookAt(new Vector3(lookTarget.position.x, transform.position.y, lookTarget.position.z));
        }
    }

    [NaughtyAttributes.Button]
    private void TooltipOff()
    {
        gameObject.SetActive(false);
    }
}
