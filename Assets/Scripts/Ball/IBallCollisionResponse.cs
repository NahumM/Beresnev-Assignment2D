using UnityEngine;
using System;

public interface IBallCollisionResponse
{

    public event Action OnPlayerBallCollision;
    public void Init();

    public void CollisionAction(Collision2D collision);
}
