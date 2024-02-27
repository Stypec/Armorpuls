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
    static int currentTastPriority = 5;
    public float speed;
    public FightMode fightMode {private set; get;}
    protected Vector3 moveToPos;
    protected Vector3 targetPos;
    bool stayNearTargetPos = false;
    void Awake()
    {
        moveToPos = transform.position;
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
