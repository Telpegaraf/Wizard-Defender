using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifeTime;

    private BoxCollider2D boxColl;

    private void Awake()
    {
        boxColl = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        lifeTime += Time.deltaTime;
        if (lifeTime > 2.5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxColl.enabled = false;
        Deactivate();

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxColl.enabled = true;

        float localscaleX = transform.localScale.x;
        if (Mathf.Sign(localscaleX) != direction)
            localscaleX = -localscaleX;

        transform.localScale = new Vector3(localscaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
