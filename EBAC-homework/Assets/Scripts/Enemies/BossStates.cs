using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override private void OnStateEnter(params object[] objs) 
        {
            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }
    public class BossStateInit : BossStateBase
    {
        BossStateBase.OnStateEnter(objs);
        Debug.Log("Boss: " + boss);
    }
}
