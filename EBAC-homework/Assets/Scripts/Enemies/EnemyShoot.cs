using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

    public class EnemyShoot : EnemyBase
    {
        public BaseGun baseGun;

        protected override void Init()
        {
            base.Init();

            baseGun.StartShoot();
        }
    }
}
