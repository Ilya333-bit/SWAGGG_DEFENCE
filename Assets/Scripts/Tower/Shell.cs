using UnityEngine;

public class Shell : MonoBehaviour
{
    private float _damage;
    private float _impactForce;
    [SerializeField] private string _enemyTag = "Enemy";
    private Rigidbody _rigidbody;
    public GameObject _enemyObject;

    public float Damage(float value) => _damage = value;
    public float ImpactForce(float value) => _impactForce = value;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        FollowTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_enemyTag))
        {
            other.GetComponent<Enemy>().Health -= _damage;
            Destroy(gameObject);
        }
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = _enemyObject.transform.position;
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        _rigidbody.velocity = targetDirection * _impactForce;
    }
}
