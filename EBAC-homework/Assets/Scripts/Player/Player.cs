using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class Player : Singleton<Player>//, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;

    public CharacterController characterController;
    public float jumpSpeed = 15f;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    private float vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Health")]
    public HealthBase healthBase;
    public UIFillUpdate uIFillUpdate;

    public bool alive = true;

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
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    vSpeed = jumpSpeed;
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

            vSpeed -= gravity * Time.deltaTime;
            speedVector.y = vSpeed;

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
}
