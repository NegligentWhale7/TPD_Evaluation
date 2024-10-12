using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20;

    public float BulletSpeed { get => bulletSpeed;}

    //private void Update()
    //{
    //    if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameOver
    //       || GameManager.Instance.CurrentGameState == GameManager.GameState.InterWave) return;

    //    transform.Translate(bulletSpeed * Time.deltaTime * Vector3.forward);
    //}

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
