using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    public float CurrentHealth { get; private set; }

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, maxHealth);

        if (CurrentHealth <= 0) 
        {
            SkillManager.UltimativePoints++;
            gameObject.SetActive(false);
        }
    }
}
