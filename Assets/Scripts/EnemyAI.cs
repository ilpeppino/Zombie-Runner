using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform _target;
    NavMeshAgent _enemyNav;


    private void Awake()
    {
        _enemyNav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyNav.SetDestination(_target.position);
    }

}