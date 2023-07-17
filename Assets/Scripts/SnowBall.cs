using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rangeGizmosSphere;
    [SerializeField] private float secondRange;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyLayer;


    private void Awake()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.25f);
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(-1 * speed, rb.velocity.y);
        if (FreezeCreatures())
        {
            Debug.Log("Freeze");
        }
        if (EnemyInCollision())
        {
            Destroy(gameObject);
        }
    }

    private bool FreezeCreatures()
    {
        RaycastHit2D hit =
            Physics2D.CircleCast(transform.position,
            secondRange, Vector2.left, 0, enemyLayer);
        if (hit.collider != null && transform.position.y !> hit.transform.position.y)
            hit.transform.GetComponent<Goblin>().FreezeSpeed();
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangeGizmosSphere);
        Gizmos.DrawWireSphere(transform.position, secondRange);
    }

    private bool EnemyInCollision()
    {
        RaycastHit2D hit =
            Physics2D.CircleCast(transform.position,
            rangeGizmosSphere, Vector2.left, 0, enemyLayer);
        if (hit.collider != null)
            hit.transform.GetComponent<Goblin>().FreezeSpeed();
        return hit.collider != null;
    }
}
