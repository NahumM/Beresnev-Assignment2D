using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(IBallMover))]
public class BasicBallCollisionResponse : MonoBehaviour, IBallCollisionResponse
{
    IBallMover _ballMover;
    [SerializeField] GlobalBallChanger _globalBallChanger;

    public event Action OnPlayerBallCollision;

    public void Init()
    {
        if (!TryGetComponent<IBallMover>(out _ballMover)) Debug.LogWarning("Ball mover is not loaded!");
    }
    public void CollisionAction(Collision2D collision)
    {

        if (collision.transform.CompareTag("ReflectionWall"))
        {
            Vector2 _contactNormal = collision.contacts[0].normal;
            ReflectDirection(_contactNormal);
        }

        if (collision.transform.CompareTag("PlayerPlatform"))
        {
            Vector2 _contactNormal = collision.contacts[0].normal;
            ReflectDirection(_contactNormal);

            OnPlayerBallCollision.Invoke();
        }

        if (collision.transform.CompareTag("HorizontalWall"))
        {
            RestartPositionWithNewDirection();
        }
    }

    private void RestartPositionWithNewDirection()
    {

        Vector2 _randomDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        transform.position = Vector2.zero;

        _ballMover?.AssignNewDirection(_randomDirection);

        DeathAction();
    }

    private void DeathAction()
    {
        _globalBallChanger.ChangeBall(UnityEngine.Random.Range(0, DataController.currentSessionData.UsableBalls.Count));
    }

    private void ReflectDirection(Vector2 normal)
    {
        Vector2 _currentDirection = _ballMover.GetDirection();
        Vector2 _reflectedDirection = Vector2.Reflect(_currentDirection.normalized, normal);

        _ballMover?.AssignNewDirection(_reflectedDirection);
    }
}
