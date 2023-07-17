using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [Header ("SnowBall Parameteres")]
    [SerializeField] private float snowBallSpeed;
    [SerializeField] private float firstRangeGizmosSphere;
    [SerializeField] private float secondRangeGizmosSphere;
    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyLayer;


    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(-1 * snowBallSpeed, rb.velocity.y);
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
            secondRangeGizmosSphere, Vector2.left, 0, enemyLayer);
        if (hit.collider != null && transform.position.y !> hit.transform.position.y)
            hit.transform.GetComponent<Goblin>().FreezeSpeed();
        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, firstRangeGizmosSphere);
        Gizmos.DrawWireSphere(transform.position, secondRangeGizmosSphere);
    }

    private bool EnemyInCollision()
    {
        RaycastHit2D hit =
            Physics2D.CircleCast(transform.position,
            firstRangeGizmosSphere, Vector2.left, 0, enemyLayer);
        if (hit.collider != null)
            hit.transform.GetComponent<Goblin>().FreezeSpeed();
        return hit.collider != null;
    }
}
