using UnityEngine;

public class MovementInput : IMovementInput
{
    // --------------- Teclas
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    public MovementInput(KeyCode left, KeyCode right, KeyCode jump)
    {
        leftKey = left;
        rightKey = right;
        jumpKey = jump;
    }
    
    public float Horizontal
    {
        get
        {
            float value = 0f;

            if (Input.GetKey(leftKey))
            {
                value = -1f;
            }

            if (Input.GetKey(rightKey))
            {
                value = 1f;
            }

            return value;
        }
    }

    public bool JumpPressed
    {
        get
        {
            return Input.GetKeyDown(jumpKey);
        }
    }

    public bool JumpHeld
    {
        get
        {
            return Input.GetKey(jumpKey);
        }
    }
}
