using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Vector2;

public class Input : MonoBehaviour
{
    public bool Active { get; private set; }

    private Pointer _pointer;
    private TouchInfo _info;

    private void Start()
    {
        _pointer = Pointer.current;

        _info = new TouchInfo();
    }

    private void Update()
    {
        CheckPointer();
        if (Active)
        {
            _info.Pressed = _pointer.press.isPressed;
            if (_info.Pressed)
            {
                if (_info.InitPos == zero)
                    _info.InitPos = _pointer.position.ReadValue();
                _info.CurrentPos = _pointer.position.ReadValue();
            }
            else
            {
                _info.InitPos = zero;
            }
        }
    }

    private void CheckPointer()
    {
        if (_pointer.enabled)
        {
            Active = true;
        }
        else
        {
            Active = false;
        }
    }

    public TouchInfo GetInput()
    {
        return _info;
    }
}
