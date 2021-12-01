using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSettingsUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] BallCreationPanelUI _ballCreationPanelUI;
    [SerializeField] BallsToChoosePanelUI _ballsToChoosePanelUI;


    private void OnEnable()
    {
        Subscrabe();
    }

    private void Subscrabe()
    {
        _ballsToChoosePanelUI.OnBallIsChoosed += TurnBallSettingsOff;
    }

    private void Unsubscrabe()
    {
        _ballsToChoosePanelUI.OnBallIsChoosed -= TurnBallSettingsOff;
    }

    private void TurnBallSettingsOff()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Unsubscrabe();
    }
}
