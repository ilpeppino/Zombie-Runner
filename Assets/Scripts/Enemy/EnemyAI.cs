using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform _target;
    [SerializeField] float _chaseRange = 10f;

    private Renderer _renderer;
    private NavMeshAgent _enemyNav;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isProvoked;
    private bool _isAttacking;
    private Animator _animator;


    private void Awake()
    {
        _enemyNav = GetComponent<NavMeshAgent>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.green;
        _animator = GetComponent<Animator>();
        
        _isProvoked = false;
        _isAttacking = false;
    }


    void Update()
    {

        DetermineBehaviour();

        if (_isAttacking)
        {
            AttackTarget();
        } 
        else if (_isProvoked)
        {
            ChaseTarget();
        } 
        else
        {
            StopMoving();
        }
        


        

    }

    private void DetermineBehaviour()
    {
        _distanceToTarget = Vector3.Distance(_target.position, transform.position);

        _isProvoked = (_distanceToTarget <= _chaseRange);
        _isAttacking = (_distanceToTarget <= _enemyNav.stoppingDistance);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }


    private void ChaseTarget()
    {
        _enemyNav.SetDestination(_target.position);
        _isProvoked = true;
        _animator.SetTrigger("move");

    }

    private void AttackTarget()
    {
        _animator.SetTrigger("attack");
    }

    private void StopMoving()
    {
        _isProvoked = false;
        _animator.SetTrigger("idle");
        _isAttacking = false;
    }

    private void SetTriggerForAnimation(string parameter)
    {
        
    }
}