using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    [Header ("Spell Parameters")]
    [SerializeField] private float damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform limitPoint;
    [SerializeField] private float rangeGizmosSphere;
    private float cooldownTimer = Mathf.Infinity;

    private Transform target;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeGizmosSphere);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) 
        {
            if (enemy.transform.position.x < limitPoint.position.x)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= rangeGizmosSphere && nearestEnemy.transform.position.x < limitPoint.transform.position.x)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown)
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        GameObject spellGO = Instantiate(spellPrefab, firepoint.position, firepoint.rotation);
        Spell spell = spellGO.GetComponent<Spell>();

        if (spell != null)
        {
            spell.Seek(target);
        }
        cooldownTimer = 0;
    }

    //private int FindProjectiles()
    //{
    //    for (int i = 0; i < projectiles.Length; i++)
    //    {
    //        if (!projectiles[i].activeInHierarchy)
    //        {
    //            return i;
    //        }
    //    }
    //    return 0;
    //}
}
