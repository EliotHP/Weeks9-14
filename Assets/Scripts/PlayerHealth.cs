using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 30f;
    public float currentHealth;

    public Slider healthBar;
    public bool isPlayer1;
    public UIManager uiManager;

    void Start()
    {
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


            Die();
        }
    }

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
    void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }
    }
 

