using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SaveData
{
    public int MaxRecord;
    public BallStats CurrentBallActivated;
    public List<BallStats> UsableBalls;
}

