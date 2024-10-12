using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] ObjectPooler bulletPool;
    [SerializeField] ObjectPooler coinPool;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] float timeForShoot;
    [SerializeField] float distanceToShoot = 1.5f;
    [SerializeField] float coinRatio = 50;
    [Header("UI")]
    [SerializeField] UIBarItem healthBar;

    Transform player;
    float timeCounter = 0;
    bool canShoot = false;
    float currentHealth = 0;
    float maxHealth = 100;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject.transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
           || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

        if (Vector3.Distance(transform.position, player.position) < distanceToShoot)
        {
            agent.isStopped = true;
            canShoot = true;
        }
        else
        {
            agent.isStopped = false;
            canShoot = false;
        }

        transform.LookAt(player);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
           || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

        if (player == null) return;        
        agent.destination = player.position;
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
           || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

        if (!canShoot) return;
        timeCounter += Time.deltaTime;
        if (timeCounter >= timeForShoot)
        {
            ShootBullet();
            timeCounter = 0;
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = bulletPool.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);  

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.DisplayBarValue(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            GameManager.Instance.AddScore(10);
            InstantiateCoin();
            EnemiesTracker.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    public void InstantiateCoin()
    {
        if (Random.Range(0, 100) < coinRatio)
        {
            var coin = coinPool.GetPooledObject();
            coin.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            coin.SetActive(true);
        }
    }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }
}
