using UnityEngine;

public class EnemyMovement : Enemy
{
    private int _currentIndex;
    [SerializeField] private GameObject _pathPointsObject;
    private PathPoints _pathPoints;
    private bool _isMovement = true;

    private void Start()
    {
        _pathPoints = _pathPointsObject.GetComponent<PathPoints>();
    }

    private void Update()
    {
        if (_isMovement)
        {
            Movement();
            UpdateIndex();
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
            Economy.Coins = Economy.Coins + _coins;
            Economy.Murders = Economy.Murders + 1;
        }
    }

    private void Movement()
    {
        Vector3 direction = (_pathPoints.GetPathPoint(_currentIndex) - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * _speed, Space.World);
    }

    private void UpdateIndex()
    {
        if (Vector3.Distance(transform.position, _pathPoints.GetPathPoint(_currentIndex)) < 0.2f)
        {
            _currentIndex++;

            if (_currentIndex == _pathPoints.GetLength())
            {
                _isMovement = !_isMovement;
            }
        }
    }
}
