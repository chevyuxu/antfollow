using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAI : MonoBehaviour
{
    public static AntAI instance;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform target;
    public bool isTarget = false;
    public float moveSpeed = 5.0f;
    public float changeDirectionInterval = 2.0f;
    private Vector3 moveDirection;
    private float timer;
    private void OnEnable()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ChangeDirection();
    }
    private void Update()
    {
        if (isTarget)
        {
            navMeshAgent.destination = target.position;
        }
        else if (!isTarget)
        {
            timer += Time.deltaTime;

            if (timer > changeDirectionInterval)
            {
                ChangeDirection();
                timer = 0;
            }

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, 0, randomZ).normalized;
    }


}
