using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private int damage;
    private Rigidbody2D rb;
    private Animator anim;
    public float currentSpeed { get; private set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = maxSpeed;
        //if (currentSpeed == 5)
            //BirdAnimation();
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
        anim.SetBool("isFreeze", true);
        yield return new WaitForSeconds(4);
        currentSpeed = maxSpeed;
        anim.SetBool("isFreeze", false);
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

    private void BirdAnimation()
    {

    }
}
