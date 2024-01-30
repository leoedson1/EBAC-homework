using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }
    public class BossBase : MonoBehaviour, IDamageable
    {
        public Collider eCollider;
        public FlashColor flashColor;
        public ParticleSystem dmgParticle;

        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;

        public float speed = 5f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;

        private void OnValidate() 
        {
            if(healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
           Init();
           OnValidate();
           if(healthBase != null)
           {
               healthBase.OnKill += OnBossKill;
           }
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());   
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());   
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());   
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());   
        }

        private void OnBossKill(HealthBase h)
        {
            if(eCollider != null) eCollider.enabled = false;
            Destroy(gameObject, 3f);
            SwitchState(BossAction.DEATH);
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position + dir, .1f);            
        }

        public void OnDamage(float damage)
        {
            if(flashColor != null) flashColor.Flash();
            if(dmgParticle != null) dmgParticle.Emit(15);

            healthBase.Damage(damage);
        }

#region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallback?.Invoke();
        }

        private void OnCollisionEnter(Collision collision) 
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }
#endregion

#region RANDOM WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                Vector3 targetPostition = new Vector3( t.transform.position.x, this.transform.position.y, t.transform.position.z ) ; //enxerto
                this.transform.LookAt( targetPostition ) ; //enxerto
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
#endregion

#region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
#endregion

#region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
#endregion

#region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
#endregion
    }
}
