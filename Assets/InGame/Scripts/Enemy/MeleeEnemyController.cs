using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Patrol,
    idle,
    Chasing,
    Attack
}
public class MeleeEnemyController : MonoBehaviour
{
    private EnemyState currentState = EnemyState.idle;

    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;
        Debug.Log($"Changing state from {currentState} to {newState}");
        currentState = newState;
    }

    [Header("Player detectioon")]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float colliderDistance;


    [Header("References")]
    [SerializeField] private Transform gfxObject;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private Transform player;



    #region Unity Methods
    private void Awake()
    {/*
        gfxObject = transform.GetChild(0);
        boxCollider = gfxObject.GetComponent<BoxCollider2D>();*/
    }

    private void Start()
    {

    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.idle:
                HandleIdleState();
                break;
            case EnemyState.Patrol:
                HandlePatrolState();
                break;
            case EnemyState.Chasing:
                HandleChasingState();
                break;
            case EnemyState.Attack:
                HandleAttackState();
                break;
            default:
                break;
        }


    }
    #endregion

    #region Helper function
    private bool PlayerInRange()
    {
        var boxSize = getBoxColliderSize();
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * detectRange * colliderDistance, boxSize,
                      0f, Vector2.right, 0, whatIsPlayer);


        //player = hit.transform;
        return hit.collider != null;
    }

    private bool PlayerInAttackRange()
    {
        //Debug.Log((player.position - transform.position).magnitude);
        return (player.position - transform.position).magnitude - _distanceWhileAttacking <= _attackRange;
    }

    private Vector3 getBoxColliderSize()
    {
        return new Vector3(boxCollider.bounds.size.x * detectRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
    }

    private void OnDrawGizmos()
    {
        var boxSize = getBoxColliderSize();
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * transform.localScale.x * detectRange * colliderDistance, boxSize);
    }


    private bool isFacingRight => transform.eulerAngles.x == 180f;

    private void RotateEnemyToWayPoint(Transform posToRotate)
    {
        /*if((transform.position -  posToRotate.position).normalized.x > 0)
        transform.right = posToRotate.right;
        //Debug.Log(transform.right + " "+ posToRotate.right);*/

        if(posToRotate.localPosition.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        }

        else if(posToRotate.localPosition.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,180f,transform.rotation.z);
        }
    }
    #endregion

    #region Idle

    [Header("Idle State")]
    [SerializeField] private float idleWaitTime = .5f;
    private void HandleIdleState()
    {
        StartCoroutine(WaitAndGo());
    }

    private IEnumerator WaitAndGo()
    {
        yield return new WaitForSeconds(idleWaitTime);
        if (PlayerInRange()) ChangeState(EnemyState.Chasing);
        else ChangeState(EnemyState.Patrol);
    }

    #endregion

    #region Patrolling
    [Header("Patrol State")]
    [SerializeField] private float _patrolSpeed = 1f;
    [SerializeField] private List<Transform> _wayPoints = null;
    int _currentIndex = 0;
    private void HandlePatrolState()
    {
        if (transform.position != _wayPoints[_currentIndex].position)
        {

            RotateEnemyToWayPoint(_wayPoints[_currentIndex]);
            MoveToPoint(_currentIndex);
            //Debug.Log(wayPoints[currentIndex].name);


        }

        else
        {
            if (_currentIndex + 1 < _wayPoints.Count)
                _currentIndex++;
            else
            {
                //Debug.Log("Changed to zero");
                _currentIndex = 0;
            }
            ChangeState(EnemyState.idle);
        }

        if (PlayerInRange()) ChangeState(EnemyState.Chasing);

    }



    private void MoveToPoint(Vector2 pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, _chaseSpeed * Time.deltaTime);
    }

    private void MoveToPoint(int index)
    {

        transform.position = Vector2.MoveTowards(transform.position, _wayPoints[index].position, _patrolSpeed * Time.deltaTime);
    }

    #endregion

    #region Chasing

    [Header("Chasing State")]
    [SerializeField] private float _chaseSpeed = 2f;
    [SerializeField] private float _idleTimeBeforeChasing = 1f;
    private void HandleChasingState()
    {
        StartCoroutine(WaitAndChase());
    }

    private IEnumerator WaitAndChase()
    {
        yield return new WaitForSeconds(_idleTimeBeforeChasing);

        if (!PlayerInAttackRange())
        {
            //Debug.Log("Moving towards player in chasing state");
            MoveToPoint(new Vector2(player.position.x, transform.position.y));
        }
        else
        {
            Debug.Log("Changing state to attack");
            ChangeState(EnemyState.Attack);
        }

        if (!PlayerInRange())
        {
            ChangeState(EnemyState.Patrol);
        }
    }

    

    #endregion

    #region Attack
    [Header("Attack State")]
    [SerializeField] private float _attackCooldown = .2f;
    [SerializeField] private float _attackDamage = 2f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField,Tooltip("Minimum distnace between player and enemy when enemy is attacking")] private float _distanceWhileAttacking = .4f; 
    private float lastTimeAttack = 0f;
    private void HandleAttackState()
    {
        if (Time.time > _attackCooldown + lastTimeAttack && PlayerInAttackRange())
        {
            Attack();
        }

        else if (PlayerInRange() && !PlayerInAttackRange())
        {
            //Debug.Log((player.position - transform.position).magnitude);

            ChangeState(EnemyState.Chasing);
        }

        else if (!PlayerInRange())
        {
            ChangeState(EnemyState.Patrol);
        }
    }

    private void Attack()
    {
        Debug.Log("Attacking the player");

        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, whatIsPlayer);
        if (hitObjects == null)
        {
            //Debug.Log("Didnt hit anything");
            return;
        }

        foreach (Collider2D hitObject in hitObjects)
        {
            var health = hitObject.GetComponent<PlayerHealth>();
            var rb = hitObject.GetComponent<Rigidbody2D>();

            if (health == null)
            {
                continue;
            }
            health.TakeDamage(_attackDamage);

            lastTimeAttack = Time.time;
        }
    }

    #endregion


}
