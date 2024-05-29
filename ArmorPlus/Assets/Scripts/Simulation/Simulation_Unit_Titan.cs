using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Simulation_Unit_Titan : Simulation_Unit
{
    NavMeshAgent agent;
    Collider[] detectedEnemies;
    List<Transform> visibleEnemies = new List<Transform>();
    [SerializeField] int damageThreshold;
    [SerializeField] float minDisForLeap;
    public delegate void Empty_Delegate();
    public event Empty_Delegate OnDamageThresholdReached;

    float disToTarget;
    Vector3 dirToTarget;

    bool isReadyToFire;
    Transform target;
    bool isAction;
    int collectedDamage;
    float targetDisOffset;
    void Start()
    {
        OnDamageThresholdReached += OnReachedDamageThreshold;
        agent = GetComponent<NavMeshAgent>();
        targetDisOffset = UnityEngine.Random.Range(-3, 0);
    }

    private void OnReachedDamageThreshold()
    {
        if (this != null)
            StartCoroutine(DodgeRoutine());
    }

    private IEnumerator DodgeRoutine()
    {
        isAction = true;
        
        int rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0)
            rand = -1;
        Vector3 right = Vector3.Cross(dirToTarget, Vector3.up).normalized;
        Vector3 dodgeTarget = Vector3.zero;
        RaycastHit info;
        if (!Physics.SphereCast(transform.position, 0.5f, right, out info, 2))
        {
            dodgeTarget = transform.position + right * 2;
        }
        else if (!Physics.SphereCast(transform.position, 0.5f, -right, out info, 2))
        {
            dodgeTarget = transform.position - right * 2;
        }
        else 
            yield break;
        targetPos = dodgeTarget;
        speed *= 0.5f;

        yield return new WaitUntil(() => Vector3.Distance(transform.position, dodgeTarget) < 0.2f);
        speed *= 2;

        collectedDamage = 0;
        isAction = false;
        Debug.Log(isAction);
    }

    void Update()
    {
        Move();
        if (isReadyToFire)
            Fire();
    }

    private void Fire()
    {
        if (isAction)
            return;
        shootTimer += Time.deltaTime;
        if (shootTimer > 1/fireRate)
        {
            Simulation_Unit SimUnit = target.GetComponentInParent<Simulation_Unit>();
            
            SimUnit.TakeDamage(damage, out bool check);

            if (check)
                Leap();

            shootTimer = 0;
        }
    }

    private void Leap()
    {
        StartCoroutine(LeapRoutine());
    }

    private IEnumerator LeapRoutine()
    {
        isAction = true;
        speed *= 2;

        Vector3 dodgeTarget = transform.position;

        if (Vector3.Distance(transform.position, target.position) > minDisForLeap)
            dodgeTarget = transform.position + dirToTarget * 2.5f;
        
        targetPos = dodgeTarget;

        collectedDamage /= 4;

        dodgeTarget.y = transform.position.y;
        yield return new WaitUntil(() => Vector3.Distance(transform.position, dodgeTarget) < 0.2f);
        speed *= 0.5f;

        isAction = false;
    }

    private void Move()
    {
        if (isAction)
            return;
        Collider[] detectedEnemies = Physics.OverlapSphere(transform.position, sightRadius, enemyMask);

        float dis = sightRadius + 1;
        target = null;
        float currentDis = 0;
        visibleEnemies = new List<Transform>();
        // Debug.Log(visibleEnemies.Count);
        for (int i = 0; i < detectedEnemies.Length; i++)
        {
            // Vector3 dir = (detectedEnemies[i].transform.position - transform.position).normalized;
            visibleEnemies.Add(detectedEnemies[i].transform);
            currentDis = Vector3.Distance(transform.position, detectedEnemies[i].transform.position);
            if (currentDis < dis)
            {
                // Debug.Log("action");
                dis = currentDis;
                target = detectedEnemies[i].transform;
            }
        }

        if (target == null)
        {
            isReadyToFire = false;
            return;
        }

        disToTarget = Vector3.Distance(transform.position, target.position);
        dirToTarget = (target.position - transform.position).normalized;

        Debug.DrawLine(transform.position, transform.position + dirToTarget, Color.blue);

        if (!Physics.Raycast(transform.position, dirToTarget, disToTarget, enemyMask) || targetDisOffset + disToTarget > optimalFiringDis)
        {
            targetPos = target.position;
            isReadyToFire = false;
        }
        else
        {
            isReadyToFire = true;
            targetPos = transform.position;
        }
    }


    public override void TakeDamage(int damage, out bool check)
    {
        health -= damage;
        if (damage > 0)
            collectedDamage += damage;

        if (health == 0)
            gameObject.SetActive(false);

        if (isAction)
        {
            check = false;
            return;
        }
        if (collectedDamage >= damageThreshold)
        {
            check = true;
            collectedDamage = 0;
            OnDamageThresholdReached?.Invoke();
            return;
        }
        check = false;
    }

    void FixedUpdate()
    {
        // transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * speed);
        agent.destination = targetPos;

    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 0.5f);
    }
}
