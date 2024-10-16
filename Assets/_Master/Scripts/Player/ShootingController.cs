using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    [SerializeField] ObjectPooler bulletPool;  
    [SerializeField] ObjectPooler missilePool; 
    [SerializeField] Transform firePoint;
    //[SerializeField] float bulletSpeed = 20f;
    //[SerializeField] float missileSpeed = 15f;
    [SerializeField] int maxMissiles = 2;
    [SerializeField] PlayerAnimatorManager animatorManager;
    [SerializeField] UIManager uiManager;

    private bool isBullet = false;
    private int currentMissiles;
    private PlayerControls controls;

    public int MaxMissiles { get => maxMissiles; set => maxMissiles = value; }
    public int CurrentMissiles { get => currentMissiles; set => maxMissiles = value; }

    void Awake()
    {
        controls = new PlayerControls();
        currentMissiles = maxMissiles;
    }

    void OnEnable()
    {
        controls.Enable();
        controls.PlayerMovement.Shoot.started += OnShoot;  
    }

    void OnDisable()
    {
        controls.PlayerMovement.Shoot.started -= OnShoot; 
        controls.Disable();
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
           || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

        if (context.control.name == "leftButton")
        {
            ShootBullet();
            AudioManager.Instance.PlaySoundFX(SoundType.ShootingBullet);
            isBullet = true;
            animatorManager.HandleShootAnimation(isBullet);
        }
        else if (context.control.name == "rightButton" && currentMissiles > 0)
        {
            uiManager.RemoveRocket();
            ShootMissile();
            AudioManager.Instance.PlaySoundFX(SoundType.ShootingRocket);
            currentMissiles--;
            isBullet = false;
            animatorManager.HandleShootAnimation(isBullet);
        }
    }

    public void MaxAvailableMissiles(float cost)
    {
        if (GameManager.Instance.TotalMoney < cost) return;
        GameManager.Instance.TotalMoney -= cost;
        maxMissiles++;
    }

    public void RecoverMissiles(float cost)
    {
        if (GameManager.Instance.TotalMoney < cost) return;
        GameManager.Instance.TotalMoney -= cost;
        currentMissiles = maxMissiles;
    }

    void ShootBullet()
    {
        GameObject bullet = bulletPool.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);
          
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bullet.GetComponent<Bullet>().BulletSpeed;
        }
    }

    void ShootMissile()
    {
        GameObject missile = missilePool.GetPooledObject();
        if (missile != null)
        {
            missile.transform.position = firePoint.position;
            missile.transform.rotation = firePoint.rotation;
            missile.SetActive(true); 

            Rigidbody rb = missile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * missile.GetComponent<Missile>().BulletSpeed;
        }
    }

    public void ReplenishMissiles(int amount)
    {
        currentMissiles = Mathf.Min(currentMissiles + amount, maxMissiles);
    }
}


