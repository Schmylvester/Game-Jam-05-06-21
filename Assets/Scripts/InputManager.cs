using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool tapDownThisFrame { get; private set; }
    public bool tapDownLastFrame { get; private set; }
    public bool tapUpThisFrame { get; private set; }
    public bool tapUpLastFrame { get; private set; }

    void Update()
    {
        checkMouse();
        checkTouch();
    }

    void checkMouse()
    {
        tapDownThisFrame = Input.GetMouseButtonDown(0);
        tapUpThisFrame = Input.GetMouseButtonUp(0);
    }

    void checkTouch()
    {
        tapUpLastFrame = tapUpThisFrame;
        tapDownLastFrame = tapDownThisFrame;
        if (Input.touchCount > 0 && !tapUpLastFrame)
        {
            tapDownThisFrame = true;
        }
        else if (Input.touchCount == 0 && tapUpLastFrame)
        {
            tapUpThisFrame = true;
        }
    }

    public void clearInputThisFrame()
    {
        tapDownLastFrame = false;
        tapDownThisFrame = false;
        tapUpLastFrame = false;
        tapUpThisFrame = false;
    }
}
