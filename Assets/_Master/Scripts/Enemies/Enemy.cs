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
    Transform player;
    float timeCounter = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (player == null) return;        
        agent.destination = player.position;

        timeCounter += Time.deltaTime;
        if (timeCounter > timeForShoot)
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
