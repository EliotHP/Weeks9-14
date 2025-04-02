using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 7f;


    public bool Shotgun = false;
    public bool Rifle = false;
    public bool Pistol = false;
    public bool WeaponNull = true;

    private float nextFireTimeP1 = 0f;
    private float nextFireTimeP2 = 0f;
    private Vector2 facingDirectionP1 = Vector2.right;
    private Vector2 facingDirectionP2 = Vector2.right;

    public float FireRate;
    public float Damage;
    public float ProjectileSpeed;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject currentWeapon;
    public bool isPlayer1 = true;

    public GameObject[] walls;
    public float playerWidth = 1f;
    public float playerHeight = 1f;
    private Vector3 startPosition;



    private void Start()
    {
        WeaponNull = true;
       startPosition = transform.position;
    }

    void Update()
    {
        if (isPlayer1)
        {
            float horizontalInput = 0;
            float verticalInput = 0;

            if (Input.GetKey(KeyCode.A)) horizontalInput = -1; // Left
            if (Input.GetKey(KeyCode.D)) horizontalInput = 1;  // Right
            if (Input.GetKey(KeyCode.W)) verticalInput = 1;    // Up
            if (Input.GetKey(KeyCode.S)) verticalInput = -1;   // Down


            Vector2 newPos = new Vector2(transform.position.x + horizontalInput * speed * Time.deltaTime,
                               transform.position.y + verticalInput * speed * Time.deltaTime);

            if (!IsCollidingWithWall(newPos))
            {
                transform.Translate(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime, 0);
            }
        }
        else
        {
            float horizontalInputP2 = 0;
            float verticalInputP2 = 0;


            if (Input.GetKey(KeyCode.B)) horizontalInputP2 = -1;  // Left
            if (Input.GetKey(KeyCode.M)) horizontalInputP2 = 1;  // Right
            if (Input.GetKey(KeyCode.H)) verticalInputP2 = 1;      // Up
            if (Input.GetKey(KeyCode.N)) verticalInputP2 = -1;   // Down

            Vector2 newPos = new Vector2(transform.position.x + horizontalInputP2 * speed * Time.deltaTime,
                              transform.position.y + verticalInputP2 * speed * Time.deltaTime);

            if (!IsCollidingWithWall(newPos))
            {
                transform.Translate(horizontalInputP2 * speed * Time.deltaTime, verticalInputP2 * speed * Time.deltaTime, 0);
            }
        }

        if (WeaponNull)
        {
            FireRate = 0;
            Damage = 0;
            ProjectileSpeed = 0;
        }
        if (Shotgun)
        {
            FireRate = 2;
            Damage = 10;
            ProjectileSpeed = 4;
        }
        if (Rifle)
        {
            FireRate = 10;
            Damage = 3;
            ProjectileSpeed = 10;
        }
        if (Pistol)
        {
            FireRate = 5;
            Damage = 5;
            ProjectileSpeed = 7;
        }
        if (isPlayer1)
        {
            if (Input.GetKey(KeyCode.D)) 
            {
                facingDirectionP1 = Vector2.right;
                transform.localScale = new Vector3(2, 2, 1);
            }
            else if (Input.GetKey(KeyCode.A)) 
            {
                facingDirectionP1 = Vector2.left;
                transform.localScale = new Vector3(-2, 2, 1);
            }

            
            if (Input.GetKeyDown(KeyCode.E) && projectilePrefab != null && firePoint != null)
            {
                Fire(facingDirectionP1);
            }

            if (WeaponNull == true)
            {
                Destroy(currentWeapon);
            }
        }


        if (!isPlayer1)
        {
            if (Input.GetKey(KeyCode.M)) 
            {
                facingDirectionP2 = Vector2.right;
                transform.localScale = new Vector3(2, 2, 1);
                Debug.Log("Player 2 facing right");
            }
            else if (Input.GetKey(KeyCode.B)) 
            {
                facingDirectionP2 = Vector2.left;
                transform.localScale = new Vector3(-2, 2, 1);
                Debug.Log("Player 2 facing left");
            }

            if (Input.GetKeyDown(KeyCode.G) && projectilePrefab != null && firePoint != null)
            {
                Fire(facingDirectionP2); 
            }
        }

    }
    void Fire(Vector2 facingDirection)
    {
        Debug.Log("Fire method triggered!");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.Initialize(facingDirection, ProjectileSpeed, Damage);
        }
    }
       public void DisplayWeapon(GameObject weaponPrefab)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            currentWeapon = Instantiate(weaponPrefab, transform);

        currentWeapon.transform.localPosition = new Vector3(0, 0, -5);

        float playerFacingX = transform.localScale.x;
        currentWeapon.transform.localScale = new Vector3(playerFacingX * 0.5f, 1, 1);


    }
    bool IsCollidingWithWall(Vector2 newPos)
    {
    
        foreach (GameObject wall in walls)
        {
        
            Vector2 wallPosition = wall.transform.position;
            float wallWidth = wall.GetComponent<SpriteRenderer>().bounds.size.x;
            float wallHeight = wall.GetComponent<SpriteRenderer>().bounds.size.y;

       
            bool xOverlap = newPos.x + playerWidth / 2 > wallPosition.x - wallWidth / 2 &&
                             newPos.x - playerWidth / 2 < wallPosition.x + wallWidth / 2;

            bool yOverlap = newPos.y + playerHeight / 2 > wallPosition.y - wallHeight / 2 &&
                             newPos.y - playerHeight / 2 < wallPosition.y + wallHeight / 2;

            if (xOverlap && yOverlap)
            {
                return true; 
            }
        }

        return false; 
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
    }

}




