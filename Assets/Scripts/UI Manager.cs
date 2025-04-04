using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    //read this one last
    //Welcome to the UI Manager script, a script that handles mostly stuff non-UI related. It also happens to be my favourite script

    //the only UI related stuff
    public GameObject winLosePanel;      
    public TMP_Text winLoseText;             
    public Button rematchButton;         
    //functions for rematch 
    public PlayerHealth player1Health;        
    public PlayerHealth player2Health;         
    public ItemSpawner itemSpawner;

    public Player player1; 
    public Player player2;

    public static UIManager Instance;

    private void Awake()
    {
        //activates the UI on player death (told to by health script)
        if (Instance == null)
        {
            Instance = this;
        }
        //if Instance != null, i.e player != 0 health, don't display the UI
        else
        {
            Destroy(gameObject);
        }
    }
    //Told by health script who won, then writes it down
    public void ShowWinLoseUI(string winner)
    {

            winLosePanel.SetActive(true);
            winLoseText.text = winner + " Wins!"; 

        
    }

    //This here, is my favourite part as it connects all of the scripts together.
    //The ItemSpawner tells the Player Script what weapon it has
    //The Player script tells the Projectile Script to fire and at what damage and speed
    //The Projectile script tells the Health Script its been hit, and how much damage it takes
    //The Health Script tells the UI Manager Script that its at 0, and the UI needs to appear
    //The UI Script makes the Rematch Button appear
    //The Rematch Button tells the Health to reset, the Players to return to starting position, the weapons to unequip, the UI to disappear and the items to respawn
    //Start again, completely full circle.
    //Beautiful
    public void StartRematch()
    {

        player1Health.ResetHealth();
        player2Health.ResetHealth();


        player1.ResetPosition();
        player2.ResetPosition();


        itemSpawner.RespawnItems();

        player1.WeaponNull = true;
        player2.WeaponNull = true;
        player1.Shotgun = false;
        player2.Shotgun = false;
        player1.Rifle = false;
        player2.Rifle = false;
        player1.Pistol = false;
        player2.Pistol = false;


        winLosePanel.SetActive(false);
    }
    //And that is everything. Is it well coded? I dunno probably not. Does it work? Arguably. And thats good enough for me.
}