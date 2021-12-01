using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{

    BallStats _ballStats;


    // Components
    IBallMover _ballMover;
    IBallCollisionResponse _ballCollisionResponse;
    IBallView _ballView;


    //Events
    public event Action OnPlayerReflectionResponse;


    public void Init()
    {

        if (!TryGetComponent<IBallMover>(out _ballMover)) Debug.Log("Ball mover is not loaded!");
        if (!TryGetComponent<IBallCollisionResponse>(out _ballCollisionResponse)) Debug.Log("Collision response is not loaded!");
        if (!TryGetComponent<IBallView>(out _ballView)) Debug.Log("Ball view is not loaded!");

        _ballMover?.Init();
        _ballCollisionResponse?.Init();
        _ballView?.Init();

        Subscribe();
    }

    void Subscribe()
    {
        if (_ballCollisionResponse != null)
        {
            _ballCollisionResponse.OnPlayerBallCollision += BallReflectedByPlayer;
           // _ballCollisionResponse.OnBallDeathCollision += ChangeBall;
        }

        BallCreator.OnBallCreation += ChangeBall;
    }

    void Unsubscribe()
    {
        if (_ballCollisionResponse != null)
        {
            _ballCollisionResponse.OnPlayerBallCollision -= BallReflectedByPlayer;
            //_ballCollisionResponse.OnBallDeathCollision -= ChangeBall;
        }

        BallCreator.OnBallCreation -= ChangeBall;
    }

    public void ChangeBall(BallStats stats)
    {
        _ballStats = stats;
        SetBallStats();
    }


    public void StartBallBehaviour()
    {
        _ballMover?.SetMovingSpeed(_ballStats.BallSpeed);
        _ballMover?.StartMovement();
    }


    private void SetBallStats()
    {
        _ballMover?.SetMovingSpeed(_ballStats.BallSpeed);

        _ballView?.SetBallViewTo(_ballStats.BallColor, _ballStats.BallSize);
    }

    private void BallReflectedByPlayer()
    {
        OnPlayerReflectionResponse.Invoke();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }


    private void OnCollisionEnter2D(Collision2D collision) => _ballCollisionResponse?.CollisionAction(collision);
}
