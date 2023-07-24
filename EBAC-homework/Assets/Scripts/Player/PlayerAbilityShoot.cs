using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public BaseGun gun1;
    public BaseGun gun2;
    public Transform gunPosition;
    public TMP_Text equippedGunUI;

    [SerializeField] private BaseGun _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun(gun1, "Gun 1");

        equippedGunUI.text = "Gun 1";

        inputs.Gameplay.SwaptoGun1.performed += cts => CreateGun(gun1, "Gun 1");
        inputs.Gameplay.SwaptoGun2.performed += cts => CreateGun(gun2, "Gun 2");
        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void CreateGun(BaseGun g, string t)
    {        
        if(g != _currentGun)
        { 
        Destroy(_currentGun);

        equippedGunUI.text = t;

        _currentGun = Instantiate(g, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
        }
    }

    private void DestroyGun()
    {
        Destroy(_currentGun);
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Start Shoot");
    }
    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }
}
