using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private float damage;
    public GameObject[] Walls;
    public GameObject[] Players;

    public float detectionDistance = 0.5f;
    private float collisionCooldown = 0.3f; 
    private float timeSinceFire = 0f;

    void Update()
    {
      transform.Translate(direction * speed * Time.deltaTime);

        timeSinceFire += Time.deltaTime;

        if (timeSinceFire >= collisionCooldown)
        {
            CollisionPlayers();
            CollisionWalls();
        }
    }

    public void Initialize(Vector2 bulletDirection, float bulletSpeed, float bulletDamage)
    {
        direction = bulletDirection;
        speed = bulletSpeed;
        damage = bulletDamage;
        Debug.Log("Player2 Fired");

    }

    void CollisionPlayers()
    {
        foreach (var player in Players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionDistance)
            {
                Debug.Log("Projectile hit player!");

                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
   
                    playerHealth.TakeDamage(damage);
                }

                Destroy(gameObject);
                break;
            }
        }
    }
    void CollisionWalls()
    {
        foreach (var wall in Walls)
        {
            float distanceToWall = Vector2.Distance(transform.position, wall.transform.position);
            if (distanceToWall <= detectionDistance)
            {
                Debug.Log("Projectile hit wall!");
                Destroy(gameObject);
                break;
            }
        }
    }
}


