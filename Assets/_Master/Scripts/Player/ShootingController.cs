using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    public ObjectPooler bulletPool;  // Pool para balas
    public ObjectPooler missilePool; // Pool para misiles
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float missileSpeed = 15f;
    public int maxMissiles = 2;

    private int currentMissiles;
    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.PlayerMovement.Shoot.started += OnShoot;  // Subscribe to the Shoot action
    }

    void OnDisable()
    {
        controls.PlayerMovement.Shoot.started -= OnShoot;  // Unsubscribe to avoid memory leaks
        controls.Disable();
    }

    void Start()
    {
        currentMissiles = maxMissiles;
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        if (context.control.name == "leftButton")
        {
            ShootBullet();
        }
        else if (context.control.name == "rightButton" && currentMissiles > 0)
        {
            ShootMissile();
            currentMissiles--;
        }
    }

    void ShootBullet()
    {
        // Obtener una bala del pool
        GameObject bullet = bulletPool.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);  // Activar la bala

            // Aplicar la velocidad de la bala
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }

    void ShootMissile()
    {
        // Obtener un misil del pool
        GameObject missile = missilePool.GetPooledObject();
        if (missile != null)
        {
            missile.transform.position = firePoint.position;
            missile.transform.rotation = firePoint.rotation;
            missile.SetActive(true);  // Activar el misil

            // Aplicar la velocidad del misil
            Rigidbody rb = missile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * missileSpeed;
        }
    }

    // Opción para recuperar misiles a través de la lógica del juego
    public void ReplenishMissiles(int amount)
    {
        currentMissiles = Mathf.Min(currentMissiles + amount, maxMissiles);
    }
}


