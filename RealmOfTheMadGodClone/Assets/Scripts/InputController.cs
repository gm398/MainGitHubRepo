using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    public KeyCode
        primaryFireKey = KeyCode.Mouse0,
        secondaryFireKey = KeyCode.Mouse1,
        switchWeaponKey = KeyCode.Q,
        forwardKey = KeyCode.W,
        backKey = KeyCode.S,
        leftKey = KeyCode.A,
        rightKey = KeyCode.D,
        sprintKey = KeyCode.LeftShift,
        jumpKey = KeyCode.Space;


    public static bool primaryFire;
    public static bool secondaryFire;
    public static bool switchWeapon;
    public static bool forward;
    public static bool back;
    public static bool left;
    public static bool right;
    public static bool sprint;
    public static bool jump;

    public static float xAxis, zAxis;
    public static float mouseX, mouseY;

    public static float mouseScroll;

    static bool isInputsEnabled = true;
    // Update is called once per frame
    void Update()
    {
        if (isInputsEnabled)
        {
            CheckInputs();
        }
    }

    void CheckInputs()
    {
        primaryFire = Input.GetKey(primaryFireKey);
        secondaryFire = Input.GetKey(secondaryFireKey);
        switchWeapon = Input.GetKeyDown(switchWeaponKey);
        forward = Input.GetKey(forwardKey);
        back = Input.GetKey(backKey);
        left = Input.GetKey(leftKey);
        right = Input.GetKey(rightKey);
        sprint = Input.GetKey(sprintKey);
        jump = Input.GetKey(jumpKey);

        xAxis = 0;
        xAxis += (right) ? 1 : 0;
        xAxis += (left) ? -1 : 0;

        zAxis = 0;
        zAxis += (forward) ? 1 : 0;
        zAxis += (back) ? -1 : 0;

        mouseY = Input.GetAxisRaw("Mouse Y");
        mouseX = Input.GetAxisRaw("Mouse X");

        mouseScroll = Input.mouseScrollDelta.y;

    }

    public static void EnableInputs()
    {
        isInputsEnabled = true;
    }
    public static void DisableInputs()
    {
        isInputsEnabled = false;

        primaryFire = false;
        secondaryFire = false;

        switchWeapon = false;

        xAxis = 0;
        zAxis = 0;
        forward = false;
        back = false;
        left = false;
        right = false;
        sprint = false;
        jump = false;


        mouseY = 0;
        mouseX = 0;
    }
}
