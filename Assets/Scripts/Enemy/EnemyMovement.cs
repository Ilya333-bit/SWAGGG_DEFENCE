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
        Vector3 direction = (_pathPoints.GetPathPoint(_currentIndex) - transform.position).normalized;
        if (_isMovement)
        {
            Movement(direction);
            UpdateIndex();
        }
        if (_health <= 0)
        {
            Destroy(gameObject);
            Economy.Coins = Economy.Coins + _coins;
            Economy.Murders = Economy.Murders + 1;
        }
        LookAtDirection(direction);
    }

    private void Movement(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * _speed, Space.World);
        
    }

    private void UpdateIndex()
    {
        if (Vector3.Distance(transform.position, _pathPoints.GetPathPoint(_currentIndex)) < 0.5f)
        {
            _currentIndex++;

            if (_currentIndex == _pathPoints.GetLength())
            {
                _isMovement = !_isMovement;
                GetComponent<EnemyAttack>().IsAttacking = true;
                GetComponent<EnemyAttack>().Vector3MainTower = _pathPoints.GetPathPoint(_currentIndex);
            }
        }
    }

    private void LookAtDirection(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        Vector3 euler = targetRotation.eulerAngles;
        euler.x = 0f;
        euler.z = 0f;
        euler.y += 90f;
        targetRotation = Quaternion.Euler(euler);
        transform.rotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 3);
    }
}
