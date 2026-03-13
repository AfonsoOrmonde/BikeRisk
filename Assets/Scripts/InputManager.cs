using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Bker Controls; 

    private void Awake()
    {
        if (Controls == null)
        {
            Controls = new Bker();
            Controls.Enable(); 
        }
    }

    private void OnDestroy()
    {
        if (Controls != null)
        {
            Controls.Disable();
        }
    }
}