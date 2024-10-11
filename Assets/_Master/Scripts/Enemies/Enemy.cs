using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] ObjectPooler bulletPool;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] float timeForShoot;
    [SerializeField] float distanceToShoot = 1.5f;
    Transform player;
    float timeCounter = 0;
    bool canShoot = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < distanceToShoot)
        {
            agent.isStopped = true;
            canShoot = true;
        }
        else
        {
            agent.isStopped = false;
            canShoot = false;
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;        
        agent.destination = player.position;
    }

    private void LateUpdate()
    {
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
            bullet.SetActive(true);  // Activar la bala

            // Aplicar la velocidad de la bala
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;
        }
    }
}
