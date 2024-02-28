using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float dragSensitivity = 3;
    private Controls controls;
    private Vector3 difference, origin;
    private Vector3 cursorPosition, cameraPosition;
    private Vector3 originCamPos;
    private Vector3 cameraOffset;
    private float camOffset;
    private bool isdragging;
    private void Start()
    {
        controls = UserSettings.controls;
        originCamPos = cam.transform.position;
        // origin = originCamPos;
    }

    void Update()
    {
        Vector3 offset = GetCameraOffset(transform.position.y, cam.transform.eulerAngles.x, cam.transform.eulerAngles.y);
        // Debug.Log(offset);
        transform.position = Vector3.Lerp(transform.position, origin - difference + offset, Time.deltaTime * dragSensitivity);
        if (controls.InGame.Fire.IsPressed())
        {
            Plane plane = new Plane(Vector3.up, new Vector3(cam.transform.position.x, 0, cam.transform.position.z));
            Ray cursorRay = cam.ScreenPointToRay(UserSettings.GetInputPositions());
            Ray camRay = new Ray(cam.transform.position, cam.transform.forward);


            plane.Raycast(cursorRay, out float cursorRange);
            plane.Raycast(camRay, out float camRange);

            cursorPosition = cursorRay.GetPoint(cursorRange);
            cameraPosition = camRay.GetPoint(camRange);

            difference = cursorPosition - cameraPosition;
            if (!isdragging)
            {
                isdragging = true;
                origin = cursorPosition;
            }
            return;
        }
        isdragging = false;
            // cam.transform.position = Vector3.Lerp(cam.transform.position, origin - difference * dragSensitivity, Time.deltaTime * 15);
    }


    private Vector3 GetCameraOffset(float height, float xAngle, float yAngle)
    {
        float tanEquation = height/Mathf.Tan(xAngle * Mathf.Deg2Rad);
        return new Vector3(-tanEquation*Mathf.Cos(yAngle * Mathf.Deg2Rad), height, -tanEquation*Mathf.Sin(yAngle * Mathf.Deg2Rad));
    }
}
