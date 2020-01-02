using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static float GetHorizontalAxis()
    {
        float r = 0.0f;
        r += Input.GetAxis("Horizontal");
        r += Input.acceleration.x;
        return Mathf.Clamp(r, -1, 1);
    }

    public static bool GetClickDown()
    {
        return Input.GetButtonDown("Fire1");
    }

    public static bool GetClickUp()
    {
        return Input.GetButtonUp("Fire1");
    }

    public static bool GetClick()
    {
        return Input.GetButton("Fire1");
    }
}
