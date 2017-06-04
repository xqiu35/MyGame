using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputListener : MonoBehaviour {

    private static InputListener inputListener;

    public static float HorizontalInput { get; private set; }
    public static float VertialInput { get; private set; }

    public static bool up { get; private set; }
    public static bool down { get; private set; }
    public static bool left { get; private set; }
    public static bool right { get; private set; }
    public static bool JumpInput { get; set; }

    public static float mouseX { get; private set; }
    public static float mouseY { get; private set; }

    void Awake()
    {
        if(inputListener==null)
        {
            inputListener = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(inputListener!=this)
        {
            Destroy(gameObject);
        }
    }

    public static void listenMouseX()
    {
        mouseX = CrossPlatformInputManager.GetAxis("Mouse X");
    }

    public static void listenMouseY()
    {
        mouseY = CrossPlatformInputManager.GetAxis("Mouse Y");
    }

    public static void listenMouseXY()
    {
        mouseX = CrossPlatformInputManager.GetAxis("Mouse X");
        mouseY = CrossPlatformInputManager.GetAxis("Mouse Y");
    }

    public static void listenHorizontalInput()
    {
        HorizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        left = HorizontalInput < 0 ? true : false;
        right = HorizontalInput > 0 ? true : false;
    }

    public static void listenVertialInput()
    {
        VertialInput = CrossPlatformInputManager.GetAxis("Vertical");
        up = VertialInput > 0 ? true : false;
        down = VertialInput < 0 ? true : false;
    }

    public static void listenJumpInput()
    {
        JumpInput = CrossPlatformInputManager.GetButtonDown("Jump");
    }

    public static bool listenKeyDown(string key)
    {
        return CrossPlatformInputManager.GetButtonDown(key);
    }

    public static bool listenKey(string key)
    {
        return CrossPlatformInputManager.GetButton(key);
    }

}
