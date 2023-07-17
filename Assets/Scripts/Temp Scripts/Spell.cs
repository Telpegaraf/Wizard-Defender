using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if ( dir.magnitude < distanceThisFrame )
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    private void HitTarget()
    {
        target.GetComponent<Health>().TakeDamage(1);
        gameObject.SetActive(false);
    }
}
