using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Desactivar la bala cuando colisiona con algo
        gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        // Desactivar la bala si sale de la pantalla
        gameObject.SetActive(false);
    }
}
