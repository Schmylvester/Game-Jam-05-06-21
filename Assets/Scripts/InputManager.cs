using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool tapDownThisFrame { get; private set; }
    public bool tapUpThisFrame { get; private set; }
    bool lastFrame;

    private void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    void Update()
    {
        checkMouse();
    }

    void checkMouse()
    {
        tapDownThisFrame = Input.GetMouseButtonDown(0);
        tapUpThisFrame = Input.GetMouseButtonUp(0);
    }

    public void clearInputThisFrame()
    {
        tapDownThisFrame = false;
        tapUpThisFrame = false;
    }
}
