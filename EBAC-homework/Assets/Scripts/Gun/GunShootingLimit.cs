using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootingLimit : BaseGun
{
    public List<UIGunUpdate> uIGunUpdates;

    public float maxShots = 5f;
    public float rechargeTime = 1f;

    private float _currentShots;
    private bool _recharging =  false;

    private void Awake()
    {
        GetAllUIS();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if(_recharging) yield break;

        while(true)
        {
            if(_currentShots < maxShots)
            {
                Shoot();
                _currentShots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    private void CheckRecharge()
    {
        if(_currentShots >= maxShots)
        {
            StopShoot();
            StartRecharging();
        }
    }

    private void StartRecharging()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < rechargeTime)
        {
            time += Time.deltaTime;
            Debug.Log("Recharging" + time);
            uIGunUpdates.ForEach(i => i.UpdateValue(time / rechargeTime));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdates.ForEach(i => i.UpdateValue(maxShots, _currentShots));
    }

    private void GetAllUIS()
    {
        uIGunUpdates = GameObject.FindObjectsOfType<UIGunUpdate>().ToList();
    }
}
