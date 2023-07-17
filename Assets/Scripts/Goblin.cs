using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    private int damage = 1;
    private Rigidbody2D rb;
    public float currentSpeed { get; private set; }

    private void Awake()
    {
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector3(1 * currentSpeed, rb.velocity.y);
    }

    public void FreezeSpeed()
    {
        StartCoroutine(Freeze());
    }

    private IEnumerator Freeze()
    {
        currentSpeed = 0;
        yield return new WaitForSeconds(4);
        currentSpeed = 4;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hole")
        {
            gameObject.SetActive(false);
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
