using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] float detectionRadius = 20f;
    [SerializeField] float shootingRange = 10f;
    [SerializeField] float fireRate = 1.5f;
    [SerializeField] int damage;
    [SerializeField] int health;
    [SerializeField] float speed;
    [SerializeField] Transform shootingPoint;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float stoppingDistance = 8f;
    [SerializeField] Transform[] coverPoints;
    private Transform targetEnemy;
    private NavMeshAgent ai;
    private float shootingTimer;
    private bool isTakingCover = false;
    private bool isEnemyVisible = false;

    private void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        health = Unit.defaultValues.health;
        speed = Unit.defaultValues.speed;
        damage = Unit.defaultValues.damage;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
            return;
        }

        FindClosestEnemy();

        if (targetEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);

            isEnemyVisible = IsEnemyVisible();

            if (distanceToEnemy <= shootingRange && !isTakingCover && isEnemyVisible)
            {
                ShootAtEnemy();
            }
            else if (!isTakingCover)
            {
                MoveTowardsEnemy();
            }
        }
    }

    private void FindClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        float closestDistance = detectionRadius;
        targetEnemy = null;

        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetEnemy = enemy.transform;
                }
            }
        }
    }

    private void MoveTowardsEnemy()
    {
        if (targetEnemy != null)
        {
            Vector3 direction = (targetEnemy.position - transform.position).normalized;
            Vector3 targetPosition = targetEnemy.position - direction * stoppingDistance;

            NavMeshPath path = new NavMeshPath();
            if (ai.CalculatePath(targetPosition, path))
            {
                ai.SetDestination(targetPosition);
            }
        }
    }

    private bool IsEnemyVisible()
    {
        if (targetEnemy == null) return false;

        RaycastHit hit;
        Vector3 direction = (targetEnemy.position - shootingPoint.position).normalized;
        if (Physics.Raycast(shootingPoint.position, direction, out hit, shootingRange))
        {
            return hit.transform == targetEnemy && hit.transform.CompareTag("Enemy");
        }
        return false;
    }

    private void ShootAtEnemy()
    {
        shootingTimer += Time.deltaTime;

        if (shootingTimer >= fireRate)
        {
            shootingTimer = 0f;

            if (isEnemyVisible)
            {
                var enemy = targetEnemy.GetComponent<Enemy>();
                if (enemy != null) enemy.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 7500 && !isTakingCover)
        {
            TakeCover();
        }
    }

    private void TakeCover()
    {
        isTakingCover = true;
        Transform closestCover = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform cover in coverPoints)
        {
            float distance = Vector3.Distance(transform.position, cover.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCover = cover;
            }
        }

        if (closestCover != null)
        {
            ai.SetDestination(closestCover.position);
            StartCoroutine(CoverCooldown());
        }
    }

    private IEnumerator CoverCooldown()
    {
        yield return new WaitForSeconds(5f);
        isTakingCover = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
