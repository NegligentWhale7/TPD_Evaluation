using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] float speed = 15;
    [SerializeField] float damage = 10;
    [SerializeField] float lifeTime = 5;
    [SerializeField] float explosionRadius = 1;
    [SerializeField] float explosionForce = 8;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject explosionEffect;

    private Collider[] colliders = new Collider[10]; // Pre-allocate array for OverlapSphereNonAlloc

    //private void OnEnable()  
    //{  
    //    Invoke("DestroyMissile", lifeTime);  
    //}  

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
            || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void DestroyMissile()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var _explosionFx = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(_explosionFx, 3);
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, colliders, layerMask);
        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i].GetComponent<Enemy>() != null)
            {
                colliders[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        DestroyMissile();
    }
}
