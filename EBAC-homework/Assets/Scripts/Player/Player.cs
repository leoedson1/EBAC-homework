using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Suit;


public class Player : Singleton<Player>//, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;

    public CharacterController characterController;
    public float jumpSpeed = 15f;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public bool alive = true;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private float _vSpeed = 0f;
    private bool _jumping = false;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Health")]
    public HealthBase healthBase;
    public UIFillUpdate uIFillUpdate;


    [Space]
    [SerializeField] private SuitSwapper _suitSwapper;

    private void OnValidate() 
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();

        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;   
    }

#region HEALTH
    private void OnKill(HealthBase h)
    {
        if(alive)
        {
            alive = false;
            colliders.ForEach(i => i.enabled = false);
            animator.SetTrigger("Death");
            
            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
        alive = true;
        animator.SetTrigger("Revive");
        Respawn();
        healthBase.ResetLife();
        colliders.ForEach(i => i.enabled = true);
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }
#endregion

    void Update()
    {
        if(alive == true)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

            var inputAxisVertical = Input.GetAxis("Vertical");
            var speedVector = transform.forward * inputAxisVertical * speed;

            if(characterController.isGrounded)
            {
                if(_jumping)
                {
                    _jumping = false;
                    animator.SetTrigger("Land");
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    _vSpeed = jumpSpeed;
                    if(!_jumping)
                    {
                        _jumping = true;
                        animator.SetTrigger("Jump");
                    }
                }
            }
            
            var isWalking = inputAxisVertical != 0;
            if(isWalking)
            {
                if(Input.GetKey(keyRun))
                {
                    speedVector *= speedRun;
                    animator.speed = speedRun;
                }
                else
                {
                    animator.speed = 1;
                }
            }

            _vSpeed -= gravity * Time.deltaTime;
            speedVector.y = _vSpeed;

            characterController.Move(speedVector * Time.deltaTime);

            animator.SetBool("Run", isWalking);
        }
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetLastCheckpointPosition();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(SuitSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(SuitSetup setup, float duration)
    {
        _suitSwapper.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _suitSwapper.ResetTexture();
    }

}
