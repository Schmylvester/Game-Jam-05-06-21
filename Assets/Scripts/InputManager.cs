using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public bool safeTapDownThisFrame { get; private set; }
    public bool unsafeTapDownThisFrame { get; private set; }
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
        tapUpThisFrame = Input.GetMouseButtonUp(0);
        safeTapDownThisFrame = Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();
        unsafeTapDownThisFrame = Input.GetMouseButtonDown(0);
    }
}
