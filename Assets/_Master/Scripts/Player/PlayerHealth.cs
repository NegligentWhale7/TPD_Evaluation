using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image shieldBar;
    public Image healthBar;
    public float maxShield = 100f;
    public float maxHealth = 100f;
    public float shieldRegenRate = 5f;
    public float shieldRegenDelay = 5f;

    private float currentShield;
    private float currentHealth;
    private bool isRegenerating;

    void Start()
    {
        currentShield = maxShield;
        currentHealth = maxHealth;
        UpdateHUD();
    }

    void UpdateHUD()
    {
        shieldBar.fillAmount = currentShield / maxShield;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield; // Take overflow damage to health
                currentShield = 0;
            }
        }
        else
        {
            currentHealth -= damage;
        }
        CancelInvoke("RegenerateShield");
        isRegenerating = false;
        UpdateHUD();

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke("StartShieldRegen", shieldRegenDelay);
        }
    }

    void StartShieldRegen()
    {
        isRegenerating = true;
    }

    void RegenerateShield()
    {
        if (isRegenerating && currentShield < maxShield)
        {
            currentShield += shieldRegenRate * Time.deltaTime;
            if (currentShield > maxShield)
            {
                currentShield = maxShield;
            }
            UpdateHUD();
        }
    }

    void GameOver()
    {
        // Implement game over logic
    }
}

