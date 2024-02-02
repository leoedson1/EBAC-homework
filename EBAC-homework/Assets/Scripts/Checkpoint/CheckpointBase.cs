using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool _checkpointActivated = false;
    private Color _blackTint = new Color(1, 1, 1);
    private string _checkpointKey = "Checkpoint Key";

    private void OnTriggerEnter(Collider other) 
    {
        if(!_checkpointActivated && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnOn();
        SaveCheckpoint();
    }

    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        SaveCheckpoint();
    }

    [NaughtyAttributes.Button]
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", _blackTint);
    }

    private void SaveCheckpoint()
    {
    /*
        if(PlayerPrefs.GetInt(_checkpointKey, 0) > key)
            PlayerPrefs.SetInt(_checkpointKey, key);
    */
        CheckpointManager.Instance.SaveCheckpoint(key);

        _checkpointActivated = true;
    }
}
