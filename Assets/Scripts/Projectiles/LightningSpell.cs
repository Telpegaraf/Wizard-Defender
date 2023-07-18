using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    private int damage = 25;
    private float lifeCounter = 2;

    private void Update()
    {
        lifeCounter -= Time.deltaTime;
        if (lifeCounter <= 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
    }

    
}
