using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAttack : MonoBehaviour
{
    [Header ("Earth Spell Parameteres")]
    [SerializeField] private GameObject eathSpellPrefab;
    public float Timer {get; private set;}
    private int timer = 4;

    private void Awake()
    {
        Timer = timer;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && Timer <= 0)
            UseEarthSpell();
        if (Timer > 0)
            Timer -= Time.deltaTime;
        else
            CreateEarthProjectile();
    }

    private void UseEarthSpell()
    {
        Instantiate(eathSpellPrefab, transform.position, Quaternion.identity);
        Timer = timer;
    }

    private void CreateEarthProjectile()
    {
        eathSpellPrefab.transform.position = new Vector3(100, 100, 0);
        eathSpellPrefab.SetActive(true);
    }
}
