using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    public string tagToCheck =  "Player";
    public GameObject bossCamera;
    public Color gizmoColor = Color.yellow;

    private void Awake()
    {
        bossCamera.SetActive(false);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheck)
        {
            TurnCameraOn();
        }
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
