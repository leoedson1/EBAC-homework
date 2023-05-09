using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootingAngled : GunShootingLimit
{
    public int amountPerShot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        int multi = 0;


        for(int i = 0; i < amountPerShot; i++)
        {
            if(i%2 == 0)
            {
                multi++;
            }
            
            var projectile = Instantiate(prefabProjectile, positionToShoot);

            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i % 2 == 0 ? angle : -angle) * multi;

            projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
}
