using UnityEngine;
using System;
public class GlobalBallChanger : MonoBehaviour
{

    public event Action<BallStats> OnBallChanged;

    public void ChangeBall(int id)
    {
        BallStats ballOfChange = DataController.currentSessionData.UsableBalls[id];
        DataController.currentSessionData.CurrentBallActivated = ballOfChange;
        OnBallChanged.Invoke(ballOfChange);
    }

}
