using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Simulation_Unit_Titan : Simulation_Unit
{

    void Update()
    {
        // Vector3 moveToDir = (moveToPos - transform.position).normalized;
        float moveToPower = Mathf.Clamp01(Vector3.Distance(transform.position, moveToPos)/moveAroundRange);
    
        targetPos = moveToPos;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime * speed);
    }
}
