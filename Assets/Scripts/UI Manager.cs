using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public GameObject winLosePanel;      
    public TMP_Text winLoseText;             
    public Button rematchButton;         

    public Transform player1StartPos;    
    public Transform player2StartPos;    
    public PlayerHealth player1Health;        
    public PlayerHealth player2Health;         
    public ItemSpawner itemSpawner;

    public Player player1; 
    public Player player2;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowWinLoseUI(string winner)
    {

            winLosePanel.SetActive(true);
            winLoseText.text = winner + " Wins!"; 

        
    }


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
}