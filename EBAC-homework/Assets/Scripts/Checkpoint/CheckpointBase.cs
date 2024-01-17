using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
        CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnOn();
    }

    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }
}
