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

    void StartShieldRegen()
    {
        isRegenerating = true;
        InvokeRepeating(nameof(RegenerateShield), 0f, 0.1f); // Llamar cada 0.1 segundos, ajustable según sea necesario
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            AudioManager.Instance.PlaySoundFX(SoundType.ShieldDamaged);
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealth += currentShield; // Pasar daño sobrante a la vida
                currentShield = 0;
            }
        }
        else
        {
            AudioManager.Instance.PlaySoundFX(SoundType.Hurt);
            currentHealth -= damage;
        }

        CancelInvoke(nameof(RegenerateShield)); // Cancelar la regeneración si recibe daño
        isRegenerating = false;
        UpdateHUD();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();
        }
        else
        {
            Invoke(nameof(StartShieldRegen), shieldRegenDelay); // Esperar un tiempo antes de empezar a regenerar
        }
    }


    public void RecoverHealth(float cost)
    {
        if (GameManager.Instance.TotalMoney < cost) return;
        GameManager.Instance.TotalMoney -= cost;
        currentHealth = maxHealth;
        UpdateHUD();
    }

    public void MaxOutHealth(float cost)
    {
        if (GameManager.Instance.TotalMoney < cost) return;
        GameManager.Instance.TotalMoney -= cost;
        float maxout = maxHealth + (maxHealth * .5f);
        maxHealth += maxout;
        UpdateHUD();
    }

    public void MaxShield(float cost)
    {
        if (GameManager.Instance.TotalMoney < cost) return;
        GameManager.Instance.TotalMoney -= cost;
        float maxout = maxShield + (maxShield * .25f);
        maxShield += maxout;
        UpdateHUD();
    }

    public void ReduceShieldWaitTime(float cost)
    {
        if (GameManager.Instance.TotalMoney < cost) return;
        GameManager.Instance.TotalMoney -= cost;
        shieldRegenRate -= .25f;
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

