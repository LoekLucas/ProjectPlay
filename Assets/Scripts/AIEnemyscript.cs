using UnityEngine;
using Pathfinding;
public class AIEnemyscript : Enemies
{
    private AIPath path;
    [SerializeField] private Transform target;
    private float distanceToTarget;
    private void Start()
    {
        path = GetComponent<AIPath>();
    }
    private void FixedUpdate()
    {
        path.maxSpeed = moveSpeed;
        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if(distanceToTarget < stopDistance)
        {
            path.destination = transform.position;
        }
        else if(distanceToTarget > seeDistance)
        {
            path.destination = transform.position;
        }
        else
        {
            path.destination = target.position;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Bullet"))
        {
            health--;
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
