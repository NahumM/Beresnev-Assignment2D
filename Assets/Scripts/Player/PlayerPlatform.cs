using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{
    [SerializeField] InputHandler _inputHandler;
    
    Vector3 _fingerPosition;

    bool _isGameStarted;
    bool _isMovementStarted;

    float _speed = 15f;
    
    enum PlayerSide { TopSide, BottomSide};

    PlayerSide _playerSide;

    public void Init()
    {
        Subscribe();
        DetectSide();
    }


    public void EnableController() => _isGameStarted = true;

    void Subscribe()
    {
        _inputHandler.OnFirstTouch += OnInputStartAction;
        _inputHandler.OnTouchMoved += OnInputMovedAction;
        _inputHandler.OnTouchEnded += OnInputEndedction;
    }

    void Unsubscribe()
    {
        _inputHandler.OnFirstTouch -= OnInputStartAction;
        _inputHandler.OnTouchMoved -= OnInputMovedAction;
        _inputHandler.OnTouchEnded -= OnInputEndedction;
    }

    void DetectSide()
    {
        if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height / 2)
        {
            _playerSide = PlayerSide.TopSide;
        }
        else _playerSide = PlayerSide.BottomSide;
    }

    void OnInputStartAction(Vector3 position)
    {
        switch (_playerSide)
        {
            case PlayerSide.TopSide:
                if (_playerSide == PlayerSide.TopSide)
                {
                    if (position.y > Screen.height / 2)
                        _isMovementStarted = true;
                }
                break;
            case PlayerSide.BottomSide:
                if (_playerSide == PlayerSide.BottomSide)
                {
                    if (position.y < Screen.height / 2)
                        _isMovementStarted = true;
                }
                break;
        }
    }

    void OnInputMovedAction(Vector3 position)
    {
        _fingerPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, position.z + 10f));
    }

    void OnInputEndedction(Vector3 position) => _isMovementStarted = false;

    Vector2 ClampPositionInBounds(Vector2 position)
    {
       Vector3 leftBoundOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10f));
        Vector3 rightBoundOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10f));

        float clampedXPosition = Mathf.Clamp(position.x, leftBoundOfScreen.x + transform.localScale.x / 2, rightBoundOfScreen.x - transform.localScale.x / 2);

        return new Vector2(clampedXPosition, position.y);

    }

    private void FixedUpdate()
    {
        if (_isGameStarted && _isMovementStarted)
        {
            Vector2 positionToMove = new Vector2(_fingerPosition.x, transform.position.y);
            positionToMove = ClampPositionInBounds(positionToMove);
            transform.position = Vector2.MoveTowards(transform.position, positionToMove, Time.fixedDeltaTime * _speed);
        }
    }

    private void OnDestroy() => Unsubscribe();

}
