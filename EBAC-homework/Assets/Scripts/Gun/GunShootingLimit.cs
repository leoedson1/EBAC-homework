using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootingLimit : BaseGun
{
    public float maxShots = 5f;
    public float rechargeTime = 1f;

    private float _currentShots;
    private bool _recharging =  false;

    protected override IEnumerator ShootCoroutine()
    {
        /*
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
        */
        if(_recharging) yield break;

        while(true)
        {
            if(_currentShots < maxShots)
            {
                Shoot();
                _currentShots++;
                CheckRecharge();
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
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }
}
