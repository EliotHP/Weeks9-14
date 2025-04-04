using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //You should read Player script first, things will make a lot more sense after
    //Welcome to the projectile script, the script that at one point imploded on itself and kept not working until I completely remade it from the ground up. Fun times.
    private Vector2 direction;
    private float speed;
    private float damage;
    //These arrays exist for collision purposes only. Do they work? 50/50, bullets go through walls sometimes. But only sometimes?
    public GameObject[] Walls;
    public GameObject[] Players;

    //these all exist for collision purposes
    public float detectionDistance = 0.5f;
    private float collisionCooldown = 0.3f; 
    private float timeSinceFire = 0f;

    void Update()
    {
        //I was having problems where the bullets players shot would damage that immediatley upon being shot, so I had to add a delay so damage didn't register for a few miliseconds
      transform.Translate(direction * speed * Time.deltaTime);

        timeSinceFire += Time.deltaTime;

        if (timeSinceFire >= collisionCooldown)
        {
            CollisionPlayers();
            CollisionWalls();
        }
    }
    //initializes projectile and uses the damage and speed set out in the player script
    public void Initialize(Vector2 bulletDirection, float bulletSpeed, float bulletDamage)
    {
        direction = bulletDirection;
        speed = bulletSpeed;
        damage = bulletDamage;
       // Debug.Log("Player2 Fired");
       //this debug was because, as mentioned in the player script, P2 kept having problems with firing

    }
    //Checks for distance from players and walls manually then (if player) tells the health script to take damage based on the damage the player script tells it to
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
    //colides with wall, supposedly destroys itself but that only works for some walls for some reason.
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


