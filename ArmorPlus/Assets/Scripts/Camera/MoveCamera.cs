using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float dragSensitivity = 3;
    [SerializeField] private float drag = 3;
    private Controls controls;
    private Vector2 difference, origin;
    private Vector2 cursorPosition;
    private bool isdragging;
    private void Start()
    {
        controls = UserSettings.controls;
        origin = cam.transform.position;
    }

    void Update()
    {

        if (controls.InGame.Fire.IsPressed())
        {
            cursorPosition = UserSettings.GetInputPositions();

            difference = cursorPosition - new Vector2(Screen.width, Screen.height)*0.5f;
            if (!isdragging)
            {
                isdragging = true;
                origin = cursorPosition;
            }
        }
        else
        {
            isdragging = false;
        }
        cam.transform.position = Vector3.Lerp(cam.transform.position, difference * dragSensitivity, Time.deltaTime * 15);
    }
}
