using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20;

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

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
