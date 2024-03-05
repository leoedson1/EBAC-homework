using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suit
{
    public class SuitItemSpeed : SuitItemBase
    {
        public float targetSpeed = 50f;
        public override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeSpeed(targetSpeed, duration);
        }
    }
}
