using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private Transform snowBallSpawnPoint;
    private float snowBallCooldown = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && snowBallCooldown <= 0)
        {
            Instantiate(snowBallPrefab, snowBallSpawnPoint.position, Quaternion.identity);
            snowBallCooldown = 5;
        }

        snowBallCooldown -= Time.deltaTime;
    }
}
