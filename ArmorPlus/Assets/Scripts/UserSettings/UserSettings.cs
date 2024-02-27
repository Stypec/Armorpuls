using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserSettings : MonoBehaviour
{
    public static Controls controls;

    void Awake()
    {
        controls = new Controls();
        controls.InGame.Enable();
        DontDestroyOnLoad(gameObject);
    }


    public static Vector2 GetInputPositions()
    {
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            return Touchscreen.current.touches[0].position.ReadValue();
        }
        return Mouse.current.position.ReadValue();
    }
}
