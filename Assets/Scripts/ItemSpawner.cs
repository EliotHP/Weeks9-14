using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] weaponPrefabs;
    public float spawnDelay = 2f;

    private GameObject[] spawnedWeapons;
    private bool weaponsSpawned = false;

    public GameObject player1;
    public GameObject player2;
    void Start()
    {
        spawnedWeapons = new GameObject[2];
    }

   public void Update()
    {
        if (!weaponsSpawned)
        {
            SpawnWeapons();
        }


        CheckPlayerProximity();
    }

    void SpawnWeapons()
    {

        GameObject weapon1 = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];
        GameObject weapon2 = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];


        int spawnIndex1 = Random.Range(0, spawnPoints.Length);
        int spawnIndex2 = Random.Range(0, spawnPoints.Length);


        spawnedWeapons[0] = Instantiate(weapon1, spawnPoints[spawnIndex1].transform.position, Quaternion.identity);
        spawnedWeapons[1] = Instantiate(weapon2, spawnPoints[spawnIndex2].transform.position, Quaternion.identity);

        weaponsSpawned = true;
    }

    public void RespawnItems()
    {
        ClearItems();
        StartCoroutine(SpawnItemsWithDelay());
    }
    private IEnumerator SpawnItemsWithDelay()
    {
        yield return new WaitForSeconds(0.5f);  
        SpawnWeapons();  
    }
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

                float distanceToPlayer1 = Vector2.Distance(player1.transform.position, weapon.transform.position);
                if (distanceToPlayer1 < 1f)
                {
                    Player playerScript = player1.GetComponent<Player>();
                    if (playerScript != null && playerScript.WeaponNull)
                    {
                        EquipWeapon(playerScript, weapon);
                        Destroy(weapon);
                    }
                }


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
