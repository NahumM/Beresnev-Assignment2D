using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public event Action PlayButtonPressed;

    [Header("Panels")]
    [SerializeField] GameObject _menuPanel;
    [SerializeField] GameObject _ballSettingsPanel;
    [SerializeField] ScoreUI _scoreUi;

    [Header("Buttons")]
    [SerializeField] Button _playButton;
    [SerializeField] Button _ballSettingsButton;


    public void Init() => Subscribe();

    private void Subscribe()
    {
        _playButton?.onClick.AddListener(PlayButtonPress);
        _ballSettingsButton?.onClick.AddListener(BallSettingsButton);
    }

    public void PlayButtonPress()
    {
        if (PlayButtonPressed != null)
            PlayButtonPressed.Invoke();
    }

    public void BallSettingsButton() => SettingsBallPanelTurn(true);

    public void ScoreUITurn(bool onOFF) => _scoreUi.gameObject.SetActive(onOFF);
    public void ScoreChanged(int value) => _scoreUi.SetScoreText(value);
    public void SetScoreRecord(int value) => _scoreUi.SetScoreRecordText(value);
    public void MenuPanelTurn(bool onOff) => _menuPanel.SetActive(onOff);
    public void SettingsBallPanelTurn(bool onOff) => _ballSettingsPanel.SetActive(onOff);

    private void OnDestroy()
    {
        _playButton?.onClick.RemoveAllListeners();
        _ballSettingsButton?.onClick.RemoveAllListeners();
    }


}
