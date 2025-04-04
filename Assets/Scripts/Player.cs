using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class Player : MonoBehaviour
{

    // Welcome to the player script, where for some reason I decided to localize almost everything to do with the game. Great ideas and many failures were had here.
    public float speed = 7f;

    //Weapon equips are run through this script, for more information look for line 87 
    public bool Shotgun = false;
    public bool Rifle = false;
    public bool Pistol = false;
    public bool WeaponNull = true;

    //Since the same script is used for both players a lot of script is doubled with the only difference being it only applies to P1 or P2, which is determined by a public boolean on line 33.
    // Did this make everything more difficult? Yes (arguably). Did I still try to do it? yes. Mistakes were made
    private Vector2 facingDirectionP1 = Vector2.right;
    private Vector2 facingDirectionP2 = Vector2.right;

    private float TimeSinceLastFireP1 = 0f;
    private float TimeSinceLastFireP2 = 0f;

    //these are the weapon stats. They change depending on what weapon is equiped (decided in Item Spawner Script), the projectile script takes from these depending on what weapon is equiped and applies them to the projectile clone they make
    public float FireRate;
    public float Damage;
    public float ProjectileSpeed;

    //GameObjects to instantiate for weapon sprites and projectile
    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject currentWeapon;
    public bool isPlayer1 = true; //Hey look its the boolean I mentioned. Cool no? probably not

    //Tried to re-do what I did in semester ones final object where I created an array of collideable objects so I didn't have to write code for every one. This is how the walls work. Wasn't sure if Collide2D was allowed
    //so everythings done manually (hence line 204 and onwards)
    public GameObject[] walls;
    public float playerWidth = 1f;
    public float playerHeight = 1f;
    //this is so players can be returned to their start position upon rematch
    private Vector3 startPosition;



    private void Start()
    {
        //this is called every rematch so players go back to no weapon and starting position
        WeaponNull = true;
       startPosition = transform.position;
    }

    void Update()
    {
        //Movement for players. Originally it used the "Input.GetAxis("Horizontal")" line, but that proved difficult when needing to use alternative key inputs for player two so instead I went with
        //the incredibly janky route of doing it all manually for each player. This also helped determine the direction the player was facing and with the collision
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
        //Pretty much exactly the same, just for P2 instead
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
        //aformentioned weapon stats. 
        if (WeaponNull)
        {
            FireRate = 50;
            Damage = 0;
            ProjectileSpeed = 0;
        }
        if (Shotgun)
        {
            FireRate = 1;
            Damage = 10;
            ProjectileSpeed = 4;
        }
        if (Rifle)
        {
            FireRate = 0.05f;
            Damage = 3;
            ProjectileSpeed = 10;
        }
        if (Pistol)
        {
            FireRate = 0.5f;
            Damage = 5;
            ProjectileSpeed = 7;
        }
        //This in retreospect probably could have been done earlier. Regardless It handles the direction the player is facing in a somewhat simple or stupid way.
        //basically if D (for example) is pressed, that can only mean the player is looking right, so player direction should then be right. Then it flips the sprite because I didn't want to bother setting up animations or anything
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

            //This just calls the fire comand in the direction the players facing so long as the projectilePrefab (a random triangle with code I placed off to the side) and fire point are not null
            if (Input.GetKeyDown(KeyCode.E) && projectilePrefab != null && firePoint != null)
            {
                //The firerate happens to be the only stat handled in the player script. I also definitely didn't realize I accidentally deleted it as some point and had to rewrite it while commenting my code.
                if (Time.time - TimeSinceLastFireP1 >= FireRate)
                {
                    Fire(facingDirectionP1);
                    TimeSinceLastFireP1 = Time.time;

                }
            }
            //This is just to delete the sprite once WeaponNull == true. This is only ever true at the start of a round.
            if (WeaponNull == true)
            {
                Destroy(currentWeapon);
            }
        }

        //exactly the same as P1 but now for P2. For whatever reason I was having a lot of problems with P2 only firing in one direction so I had to do some debugging. Left them there to show my work
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
                if (Time.time - TimeSinceLastFireP2 >= FireRate)
                {
                    Fire(facingDirectionP2);
                    TimeSinceLastFireP2 = Time.time;

                }
               
            }
            if (WeaponNull == true)
            {
                Destroy(currentWeapon);
            }
        }

    }
    //Despite the time it took for me to figure it out, this is relatively simple. It just instantiates the projectile using the projectilePrefab as well, a prefab, and at the firepoint, which is the player
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
    //I feel this part is a lot more complicated then it should be, however it was the only way to get it to work. Essentially theres 3 shapes out of frame that are the weapon prefabs. When item spawner tells the player what weapon it has equipped then
    //it instantiates a clone at the players position
       public void DisplayWeapon(GameObject weaponPrefab)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            currentWeapon = Instantiate(weaponPrefab, transform);

        currentWeapon.transform.localPosition = new Vector3(0, 0, -5);

        //rotates based on player position.
        //there was a bug where it sticks to the direction the first player to pick up a weapon was looking in. So usually a weapon was backwards for one of the players
        //I managed to fix that by using the facingDirection from earlier. Worked wonders. Except for the shotgun, which is backwards for P2 for some reason.

        if (isPlayer1)
        {
            float playerFacingXP1 = facingDirectionP1.x;
            currentWeapon.transform.localScale = new Vector3(playerFacingXP1, 1, 1);
        }
        if (!isPlayer1)
        {
            float playerFacingXP2 = facingDirectionP2.x;
            currentWeapon.transform.localScale = new Vector3(playerFacingXP2, 1, 1);
        }

    }
    //Oh yay, collision, my favourite (thats a lie). Not much to say about this, as I mentioned before it's essentially what I did in processing but a little more complex so I don't need to tell it the objects size every time theres a new one. this just checks object size
    //and says that if the player "overlaps" with it then the function should return true. Go back to line 70 and 90 and thats how it works.
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

    //this is called by UI Manager when rematch is pushed. It does exactly what it says it does.
    public void ResetPosition()
    {
        transform.position = startPosition;
    }

}




