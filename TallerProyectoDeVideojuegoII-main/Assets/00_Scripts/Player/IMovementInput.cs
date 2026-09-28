using UnityEngine;

public interface IMovementInput
{
    float Horizontal { get; }
    bool JumpPressed { get; }
    bool JumpHeld { get; }
}