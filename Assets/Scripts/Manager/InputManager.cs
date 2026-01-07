using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static Vector3 _mousePositon;
    public static bool OnMouseClick()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public static bool OnMouseHold()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    public static bool OnMouseRelease()
    {
        return Input.GetKeyUp(KeyCode.Mouse0);
    }

    public static Vector3 GetMousePosition()
    {
        _mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePositon.z = 0;
        return _mousePositon;
    }
}
