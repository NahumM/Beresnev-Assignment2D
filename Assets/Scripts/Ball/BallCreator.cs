using UnityEngine;
using System;

public static class BallCreator
{
    public static Action<BallStats> OnBallCreation;
    public static BallStats CreateBallData(string ballName, float ballSpeed, float ballSize, Color ballColor)
    {
        BallStats createdBall = new BallStats(ballName, ballSpeed, ballSize, ballColor);
        OnBallCreation.Invoke(createdBall);
        return createdBall;
    }
}
