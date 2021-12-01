using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMover : MonoBehaviour, IBallMover
{

    //Components
    Rigidbody2D _rb;

    //Calculation
    Vector2 _direction;

    //Ball Parameters
    [SerializeField] float _ballSpeed;
    bool _isStartMoving;

    public void Init()
    {
        if (!TryGetComponent<Rigidbody2D>(out _rb)) Debug.LogError("Rigidbody2D is missing!");
        AssignStartRandomDirection();
    }

    private void AssignStartRandomDirection() => _direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    public void StartMovement() => _isStartMoving = true;
    public void SetMovingSpeed(float speed) => _ballSpeed = speed;
    public void AssignNewDirection(Vector3 direction) => _direction = direction;
    public Vector2 GetDirection() => _direction;
    public float GetSpeed() => _ballSpeed;


    private void FixedUpdate()
    {
        if (_isStartMoving)
        {
            Vector2 positionToMove = _rb.position;
            positionToMove += _direction.normalized * Time.fixedDeltaTime * _ballSpeed;

            _rb.MovePosition(positionToMove);
        }
    }
}
