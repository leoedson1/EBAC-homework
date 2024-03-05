using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Suit;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<HealthUIFillUpdate> healthFillUpdate;

    public float damageMultiplier = 1f;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void ApplyDamage()
    {
        Damage(5);
    }
    
    public void Damage(float h)
    {
        _currentLife -= h * damageMultiplier;

        if(_currentLife <= 0)
        {
            Kill();
        }

        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(healthFillUpdate != null)
        {
            healthFillUpdate.ForEach(i => i.UpdateValue((float) _currentLife / startLife));
        }
    }

        public void ChangeDamageMultiplier(float damageMultiplier, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(damageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float damageMultiplier, float duration)
    {
        this.damageMultiplier = damageMultiplier;
        yield return new WaitForSeconds(duration);
        this.damageMultiplier = 1;
    }
}