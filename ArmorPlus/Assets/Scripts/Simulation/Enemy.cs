using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
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
    private Transform targetPlayer;
    private NavMeshAgent ai;
    private float shootingTimer;
    private bool isTakingCover = false;
    private bool isPlayerVisible = false;

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

        FindClosestPlayer();

        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

            isPlayerVisible = IsPlayerVisible();

            if (distanceToPlayer <= shootingRange && !isTakingCover && isPlayerVisible)
            {
                ShootAtPlayer();
            }
            else if (!isTakingCover)
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void FindClosestPlayer()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        float closestDistance = detectionRadius;
        targetPlayer = null;

        foreach (Collider player in players)
        {
            if (player.CompareTag("Player"))
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetPlayer = player.transform;
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        if (targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            Vector3 targetPosition = targetPlayer.position - direction * stoppingDistance;

            NavMeshPath path = new NavMeshPath();
            if (ai.CalculatePath(targetPosition, path))
            {
                ai.SetDestination(targetPosition);
            }
        }
    }

    private bool IsPlayerVisible()
    {
        if (targetPlayer == null) return false;

        RaycastHit hit;
        Vector3 direction = (targetPlayer.position - shootingPoint.position).normalized;
        if (Physics.Raycast(shootingPoint.position, direction, out hit, shootingRange))
        {
            return hit.transform == targetPlayer && hit.transform.CompareTag("Player");
        }
        return false;
    }

    private void ShootAtPlayer()
    {
        shootingTimer += Time.deltaTime;

        if (shootingTimer >= fireRate)
        {
            shootingTimer = 0f;

            if (isPlayerVisible)
            {
                var player = targetPlayer.GetComponent<Player>();
                if (player != null) player.TakeDamage(damage);
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