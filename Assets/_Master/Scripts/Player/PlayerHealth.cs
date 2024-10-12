using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxShield = 100f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float shieldRegenRate = 5f;
    [SerializeField] private float shieldRegenDelay = 5f;
    [Header("UI References")]
    [SerializeField] private UIBarItem healthBar;
    [SerializeField] private UIBarItem shieldBar;
    [Header("Player Events")]
    [SerializeField] private UnityEvent playerDeathEvent;

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
        shieldBar.DisplayBarValue(currentShield, maxShield);
        healthBar.DisplayBarValue(currentHealth, maxHealth);
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
        playerDeathEvent?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }
}

