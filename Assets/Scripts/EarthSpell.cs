using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpell : MonoBehaviour
{
    [SerializeField] float _InitialVelocity;
    [SerializeField] float _Angle;
    //[SerializeField] LineRenderer _Line;
    [SerializeField] float _Step;
    private Transform _Firepoint;
    //[SerializeField] GameObject earthSpellPrefab;
    private int damage = 7;

    private void Awake()
    {
        GameObject firepoint = GameObject.Find("Player");
        _Firepoint = firepoint.transform;
        float angle = _Angle * Mathf.Deg2Rad;
        DrawPath(_InitialVelocity, angle, _Step);
        StopAllCoroutines();
        StartCoroutine(Coroutine_Movement(_InitialVelocity, angle));
        
    }

    private void Update()
    {
        float angle = _Angle * Mathf.Deg2Rad;
        DrawPath(_InitialVelocity, angle, _Step);
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            //GetComponent<EarthAttack>().SetTimer();
            //Instantiate(earthSpellPrefab, _Firepoint.transform.position, Quaternion.identity);
            StopAllCoroutines();
            StartCoroutine(Coroutine_Movement(_InitialVelocity, angle));
        }

    }

    private void DrawPath(float v0, float angle, float step)
    {
        step = Mathf.Max(0.01f, step);
        float totalTime = 10;
        //_Line.positionCount = (int)(totalTime / step) + 2;
        int count = 0;
        for (float i = 0; i < totalTime; i+=step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);
            //_Line.SetPosition(count, _Firepoint.position + new Vector3(x, y, 0));
            count++;
        }
        float xFinal = v0 * totalTime * Mathf.Cos(angle);
        float yFinal = v0 * totalTime * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(totalTime, 2);
        //_Line.SetPosition(count, _Firepoint.position + new Vector3(xFinal, yFinal, 0));
    }

    private IEnumerator Coroutine_Movement(float v0, float angle)
    {
        float t = 0;
        while (t< 100)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - (1f/2f) * -Physics.gravity.y * Mathf.Pow(t,2);
            transform.position = _Firepoint.position + new Vector3(x, y,0);
            //transform.position = transform.position + new Vector3(x, y,0);

            t += Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
            collision.GetComponent<Health>().TakeDamage(damage * 3);
        else if (collision.CompareTag("Enemy"))
            collision.GetComponent<Health>().TakeDamage(damage);
        if (collision.gameObject.name == "Road")
            Destroy(gameObject);
    }
}
