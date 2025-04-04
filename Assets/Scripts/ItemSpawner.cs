using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ItemSpawner : MonoBehaviour
{
//You should read Player script first, things will make a lot more sense after
//Welcome to the ItemSpawner script. This mess of code handles the spawn positions of the weapons, what weapons are chosen, and when a player has it equiped
//Theres 4 empty gameobjects in the heirarchy. These are the spawnpoints
    public GameObject[] spawnPoints;
//Assuming you've read the Player script this is the weaponPrefabs I mentioned. They exist mostly for this script to take their sprites and place them at the spawners
    public GameObject[] weaponPrefabs;
    public float spawnDelay = 2f;

    //These are assigned at random every round so that it can be fully randomized. Technically the way I made this allows for many more weapons to be added
    private GameObject[] spawnedWeapons;
    private bool weaponsSpawned = false;

    //These exist so the spawner can know who is equiping the weapon
    public GameObject player1;
    public GameObject player2;
    void Start()
    {
        spawnedWeapons = new GameObject[2];
    }

   public void Update()
    {
    //Pretty self explanitory
        if (!weaponsSpawned)
        {
            SpawnWeapons();
        }

    //Probably not the most smart to put this in update, however this is the only way I could think of making it work at the time. I'm now realizing theres a lot more options, however, why fix what aint broke?
        CheckPlayerProximity();
    }

    void SpawnWeapons()
    {
        //Choose two random weapons from the weaponPrefabs array
        GameObject weapon1 = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];
        GameObject weapon2 = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];

        //Pick two random spawn points
        int spawnIndex1 = Random.Range(0, spawnPoints.Length);
        int spawnIndex2 = Random.Range(0, spawnPoints.Length);

        //Instantiate weapons at the decided spawn points
        spawnedWeapons[0] = Instantiate(weapon1, spawnPoints[spawnIndex1].transform.position, Quaternion.identity);
        spawnedWeapons[1] = Instantiate(weapon2, spawnPoints[spawnIndex2].transform.position, Quaternion.identity);

        weaponsSpawned = true;
    }
    //This is called by UI manager upon rematch. It does exactly what it says it does. I had to add a spawn delay however as they kept deleting themselves
    public void RespawnItems()
    {
        ClearItems();
        StartCoroutine(SpawnItemsWithDelay());
    }
    //Took me a while to figure out how to spawn with delay, including a lot of google. Eventually I found this
    private IEnumerator SpawnItemsWithDelay()
    {
        yield return new WaitForSeconds(0.5f);  
        SpawnWeapons();  
    }
    //This just destroys the weapons still spawned so its different every round
    public void ClearItems()
    {
        if (spawnedWeapons != null)  
        {
            foreach (GameObject weapon in spawnedWeapons)
            {
                if (weapon != null)  
                {
                    Destroy(weapon);
                }
            }
        }
    }
    
    void CheckPlayerProximity()
    {

        foreach (var weapon in spawnedWeapons)
        {
            if (weapon != null)
            {
                //Checks what player and distance from the spawner, if its within a specific distance then it tells calls the EquipWeapon script, which then gives the weapon to the player.
                float distanceToPlayer1 = Vector2.Distance(player1.transform.position, weapon.transform.position);
                if (distanceToPlayer1 < 1f)
                {
                    //This specifically only runs if the player only has one weapon. Didn't want a player to be able to take both weapons and leave their opponent vunerable
                    Player playerScript = player1.GetComponent<Player>();
                    if (playerScript != null && playerScript.WeaponNull)
                    {
                        EquipWeapon(playerScript, weapon);
                        Destroy(weapon);
                    }
                }

                //Same thing but for player 2
                float distanceToPlayer2 = Vector2.Distance(player2.transform.position, weapon.transform.position);
                if (distanceToPlayer2 < 1f)
                {
                    Player playerScript = player2.GetComponent<Player>();
                    if (playerScript != null && playerScript.WeaponNull)
                    {
                        EquipWeapon(playerScript, weapon);
                        Destroy(weapon);
                    }
                }
            }
        }
    }
    //This just tells the player script what to equip based off the WeaponPrefabs names
    void EquipWeapon(Player playerScript, GameObject weapon)
    {
        if (weapon.name.Contains("Shotgun"))
        {
            playerScript.Shotgun = true;
            playerScript.DisplayWeapon(weapon);
        }
        else if (weapon.name.Contains("Rifle"))
        {
            playerScript.Rifle = true;
            playerScript.DisplayWeapon(weapon);
        }
        else if (weapon.name.Contains("Pistol"))
        {
            playerScript.Pistol = true;
            playerScript.DisplayWeapon(weapon);
        }

        playerScript.WeaponNull = false;
    }
}
