using System.Collections.Generic;
using System;

[Serializable]
public struct SaveData
{
    public int MaxRecord;
    public BallStats CurrentBallActivated;
    public List<BallStats> UsableBalls;
}

