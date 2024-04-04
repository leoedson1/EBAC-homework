using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SFXManager : Singleton<SFXManager>
{
    public enum SFXType
    {
        COIN_SFX,
        DEATH_SFX,
        ENEMY_DEATH_SFX
    }

    public List<SFXManagerSetup> vfxSetup; 

    public void PlaySFXByType(SFXType sfxType, Vector3 position)
    {
        foreach(var i in vfxSetup)
        {
            if(i.sfxType == sfxType)
            {
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                Destroy(item.gameObject, 5f);
                break;
            }
        }
    }
}

    [System.Serializable]
public class SFXManagerSetup
{
    public SFXManager.SFXType sfxType;
    public GameObject prefab;
}
