using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("Simple Spell Parameters")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform limitPoint;
    [SerializeField] private float rangeGizmosSphere;
    [SerializeField] private Transform nonTargetPoint;
    private Transform target;

    [Header("Earth Spell Parameteres")]
    [SerializeField] private GameObject eathSpellPrefab;
    [SerializeField] private TextMeshProUGUI earthSpellText;
    public float EarthSpellTimer { get; private set; }
    private int timer = 4;

    [Header("SnowBall Parameteres")]
    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private Transform snowBallSpawnPoint;
    [SerializeField] private TextMeshProUGUI snowBallIconText;
    private float snowBallCooldown = 5;

    [Header("Ultimate parameteres")]
    [SerializeField] private GameObject ultimativePrefab;
    [SerializeField] private TextMeshProUGUI ultimativeText;
    [SerializeField] private Transform LightningSpawnPoint;
    public static int UltimativePoints = 0;

    private void Awake()
    {
        UltimativePoints = 0;
        EarthSpellTimer = timer;
        //earthSpellText.text = Mathf.Round(Timer).ToString();
    }

    private void Update()
    {
        // Earth Spell
        EarthSpellTimer -= Time.deltaTime;
        if (EarthSpellTimer > 0)
            earthSpellText.text = Mathf.Round(EarthSpellTimer).ToString();
        else
            earthSpellText.text = 0.ToString();
        // SnowBall
        snowBallCooldown -= Time.deltaTime;
        if (snowBallCooldown > 0)
            snowBallIconText.text = Mathf.Round(snowBallCooldown).ToString();
        else
            snowBallIconText.text = 0.ToString();
        // Ult
        ultimativeText.text = UltimativePoints.ToString();
    }

    public void CreateSnowBall()
    {
        if (snowBallCooldown <= 0)
        {
            UltimateCounterPlus();
            Instantiate(snowBallPrefab, snowBallSpawnPoint.position, Quaternion.identity);
            snowBallCooldown = 5;
        }
    }
    // Simple Spell
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firepoint.position, rangeGizmosSphere);
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
                float distanceToEnemy = Vector3.Distance(firepoint.position, enemy.transform.position);
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
            target = nonTargetPoint;  
        }
    }

    public void Attack()
    {
            GameObject spellGO = Instantiate(spellPrefab, firepoint.position, firepoint.rotation);
            Spell spell = spellGO.GetComponent<Spell>();

            if (spell != null)
            {
                spell.Seek(target);
            }
    }
    //Earth Spell

    public void UseEarthSpell()
    {
        if (EarthSpellTimer <= 0)
        {
            UltimateCounterPlus();
            Instantiate(eathSpellPrefab, transform.position, Quaternion.identity);
            EarthSpellTimer = timer;
        }
    }
    // Ult
    public void UltimateCounterPlus()
    {
        UltimativePoints++;
    }

    public void CastUltimate()
    {
        if (UltimativePoints >= 25)
        {
            Instantiate(ultimativePrefab, LightningSpawnPoint.position, Quaternion.identity);
            UltimativePoints -=25;
        }
    }
}
