using UnityEngine;
using System;

[Serializable]
public struct BallStats
{

    public string BallName;
    public float BallSpeed;
    public float BallSize;
    public Color BallColor;
    public BallStats(string ballName, float ballSpeed, float ballSize, Color ballColor)
    {
        this.BallName = ballName;
        this.BallSpeed = ballSpeed;
        this.BallSize = ballSize;
        this.BallColor = ballColor;
    } 
}
