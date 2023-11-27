using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bps = 1f; // Bullets per second
    [SerializeField] private AudioClip shootSound; // Sound effect for shooting
    private AudioSource audioSource;

    private Transform target;
    private float timeUntilFire;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void Update()
    {
        if (target == null || !CheckTargetIsInRange())
        {
            FindTarget();
            if (target == null) return;
        }

        RotateTowardsTarget();

        // Shooting logic
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / bps)
        {
            Shoot();
            timeUntilFire = 0f;
        }
    }

    private void Shoot()
    {
        if (!PauseMenu.isPaused)
        {

            GameObject bulletobj = Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            BulletFire bulletScript = bulletobj.GetComponent<BulletFire>();
            bulletScript.SetTarget(target);
            // Play shoot sound effect
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
        else
        {
            target = null;
        }
    }

    private bool CheckTargetIsInRange()
    {
        if (target == null) return false;
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, 360 * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (turretRotationPoint == null) return;

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(turretRotationPoint.position, Vector3.forward, targetingRange);
    }
}
