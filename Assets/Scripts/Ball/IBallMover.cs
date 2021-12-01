using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBallMover
{
    public void Init();
    public void SetMovingSpeed(float speed);
    public void StartMovement();
    public void AssignNewDirection(Vector3 direction);

    public Vector2 GetDirection();
    public float GetSpeed();
}
