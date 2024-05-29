using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

[System.Serializable]
public enum FightMode
{
    Coward,
    Flanker,
    Tanker,
}
public class Simulation_Unit : MonoBehaviour
{
    protected static int moveAroundRange = 5;
    [SerializeField] protected float sightRadius;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected float optimalFiringDis;
    [SerializeField] protected float fireRate;
    [SerializeField] protected int damage;
    protected float shootTimer;
    public int currentTaskPriority = 5;
    public int health = 10000;

    public float speed;
    public FightMode fightMode {private set; get;}
    protected Vector3 moveToPos;
    protected Vector3 targetPos;
    bool stayNearTargetPos = false;
    void Awake()
    {
        moveToPos = transform.position;
    }
    public virtual void TakeDamage(int damage, out bool check)
    {
        health -= damage;
        check = false;
    }
    public void ChangeFightMode(FightMode _mode)
    {
        fightMode = _mode;
    }
    public void ChangeMoveToPos(Vector3 _moveToPos)
    {
        moveToPos = _moveToPos + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
    }
}
