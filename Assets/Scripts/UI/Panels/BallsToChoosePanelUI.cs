using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BallsToChoosePanelUI : MonoBehaviour
{
    public event Action OnBallIsChoosed;

    [SerializeField] Button _ballButtonPrefab;
    [SerializeField] GlobalBallChanger _globalBallChanger;

    List<Button> _buttonBallOptions = new List<Button>();
    List<BallStats> _ballOptions = new List<BallStats>();


    private void OnEnable()
    {
        Subscribe();
        for (int i = 0; i < DataController.currentSessionData.UsableBalls.Count; i++)
        {
            AddNewBall(DataController.currentSessionData.UsableBalls[i]);
        }
    }

    void Subscribe() => BallCreator.OnBallCreation += AddNewBall;
    void Unsubscribe() => BallCreator.OnBallCreation -= AddNewBall;

    public void AddNewBall(BallStats ballS)
    {
        var buttonPrefab = Instantiate(_ballButtonPrefab.gameObject, transform.GetChild(0).GetChild(0));
        var newButton = buttonPrefab.GetComponent<Button>();
        _buttonBallOptions.Add(newButton);
        newButton.onClick.AddListener(delegate { ChooseBallButtonPressed(_buttonBallOptions.Count - 1); });
        _ballOptions.Add(ballS);

        newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ballS.BallName + " Speed: " + ballS.BallSpeed + " Size: " + ballS.BallSize;
        newButton.transform.GetChild(1).GetComponent<Image>().color = ballS.BallColor;
    }


    public void ChooseBallButtonPressed(int buttonId)
    {
        _globalBallChanger.ChangeBall(buttonId);
        if (OnBallIsChoosed != null)
            OnBallIsChoosed.Invoke();
    }

    private void OnDisable()
    {
        Unsubscribe();

        foreach (Button btn in _buttonBallOptions)
        {
            Destroy(btn.gameObject);
        }
        _buttonBallOptions.Clear();
        _ballOptions.Clear();
    }

}
