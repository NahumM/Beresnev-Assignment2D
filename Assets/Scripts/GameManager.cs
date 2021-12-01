using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] UIManager _uIManager;
    [SerializeField] DataController _dataController;
    [SerializeField] Ball _ball;
    [SerializeField] GlobalBallChanger _globalBallChanger;
    [SerializeField] List <PlayerPlatform> _players = new List<PlayerPlatform>();

    int _scoreRecord;
    int _score;
    BallStats _currentBall;

    enum GameState
    {
        InMenu,
        Playing,
        Pause,
    }

    GameState currentGameState
    {
        get
        {
            return currentGameState;
        }

        set
        {
            switch (value)
            {
                case GameState.InMenu:
                    Time.timeScale = 1f;
                    _uIManager.MenuPanelTurn(true);
                    break;
                case GameState.Playing:
                    Time.timeScale = 1f;
                    _uIManager.MenuPanelTurn(false);
                    _ball?.ChangeBall(_currentBall);
                    _ball?.StartBallBehaviour();
                    foreach (PlayerPlatform player in _players) player.EnableController();
                    _uIManager?.ScoreUITurn(true);
                    break;
                case GameState.Pause:
                    Time.timeScale = 0f;
                    break;

            }
        }
    }

    private void Start()
    {
        Initialize();
        LoadData();
        Subscribe();
    }

    private void Initialize()
    {
        _dataController?.Init();
        _uIManager?.Init();
        _ball?.Init();
        foreach (PlayerPlatform player in _players) player.Init();

    }
    private void LoadData()
    {
        SetNewScoreRecord(DataController.currentSessionData.MaxRecord);
        _currentBall = DataController.currentSessionData.CurrentBallActivated;
        _ball.ChangeBall(_currentBall);
    }

    private void Subscribe()
    {
        _uIManager.PlayButtonPressed += StartTheGame;
        _ball.OnPlayerReflectionResponse += ScoreAdd;
        _globalBallChanger.OnBallChanged += OnBallChange;
    }

    private void Unsubscribe()
    {
        _uIManager.PlayButtonPressed -= StartTheGame;
        _ball.OnPlayerReflectionResponse -= ScoreAdd;
        _globalBallChanger.OnBallChanged -= OnBallChange;
    }

    private void OnBallChange(BallStats stats)
    {
        _currentBall = stats;
        LoadData();
    }

    private void ScoreAdd()
    {
        _score++;
        if (_score > _scoreRecord) SetNewScoreRecord(_score);
        _uIManager.ScoreChanged(_score);
    }

    private void SetNewScoreRecord(int value)
    {
        _scoreRecord = value;
        DataController.currentSessionData.MaxRecord = _scoreRecord;
        _uIManager.SetScoreRecord(_scoreRecord);

    }

    private void StartTheGame()
    {
        currentGameState = GameState.Playing;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
