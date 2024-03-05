using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suit
{
    public class SuitItemPower : SuitItemBase
    {
        public float damageMultiplier = .5f;
        public override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamageMultiplier(damageMultiplier, duration);
        }
    }
}
