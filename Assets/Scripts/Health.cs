using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    
    [SerializeField] protected int m_startingHealth;
    [SerializeField] protected int m_currentHealth;
    [SerializeField] protected int m_maxHealth;

    protected virtual void OnEnable()
    {
        m_currentHealth = m_startingHealth;
    }

    protected virtual void Update()
    {
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_maxHealth);
    }

    public virtual void TakeDamage(int _damage)
    {
        m_currentHealth -= _damage;
        if(m_currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int _healAmount)
    {
        m_currentHealth += _healAmount;
    }

    public abstract void Die();

}
