using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider eCollider;
        public FlashColor flashColor;
        public ParticleSystem dmgParticle;
        public float startLife = 10f;

        [SerializeField] private float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithSpawnAnimation = true;

        private void Awake()
        {
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithSpawnAnimation)
                SpawnAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }
        
        protected virtual void OnKill()
        {
            if(eCollider != null) eCollider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float h)
        {
            if(flashColor != null) flashColor.Flash();
            if(dmgParticle != null) dmgParticle.Emit(15);

            transform.position -= transform.forward;

            _currentLife -= h;

            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region  ANIMATION
        private void SpawnAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion

        //debug
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

        public void Damage(float damage)
        {
            Debug.Log("Damage");
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position + dir, .1f);
        }

        private void OnCollisionEnter(Collision collision) 
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.Damage(1);
            }
        }
    }
}