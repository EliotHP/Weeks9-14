using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //You should read Player script first, things will make a lot more sense after (also maybe projectile script)
    //Welcome to the health script, the script where, I managed to do everything a lot more simpler than I thought it would be
    //floats for current/max health
    public float maxHealth = 30f;
    public float currentHealth;
    //Sliders for health bars assigned in heirarchy
    public Slider healthBar;
    public bool isPlayer1;
    public UIManager uiManager;

    void Start()
    {
        //Tells the script whose health bar is whose, makes the slider non-interactable so players cant just give themselves more health (they can make it look like it for some reason but it doesn't actually effect the health)
        currentHealth = maxHealth;
        if (isPlayer1)
        {
            healthBar = GameObject.Find("HealthBarPlayer1").GetComponent<Slider>();
        }
        else
        {
            healthBar = GameObject.Find("HealthBarPlayer2").GetComponent<Slider>();
        }
        healthBar.interactable = false;
        UpdateHealthBar();
    }
    //called from projectile this function just tells the script how much to damage the player. It also handles the win/lose scenario, and calls the function in the UI manager script, also telling it who won.
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {

            if (gameObject.CompareTag("Player1"))
            {
                uiManager.ShowWinLoseUI("Player 2");
            }
            else if (gameObject.CompareTag("Player2"))
            {
                uiManager.ShowWinLoseUI("Player 1");
            }

            //Die
            Die();
        }
    }
    //Die
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        if (isPlayer1)
        {
            UIManager.Instance.ShowWinLoseUI("Player 2");
        }
        else
        {
            UIManager.Instance.ShowWinLoseUI("Player 1");
        }
    }
    //This just consistently updates the slider so its accurate to the player current health
    void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }
    //called on rematch, resets health and updates health bar
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    }
 

