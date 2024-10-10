using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 10;

    private List<GameObject> objectPool;

    void Awake()
    {
        // Crear el pool de objetos al iniciar el juego
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);  // Inactivamos inicialmente
            objectPool.Add(obj);
        }
    }

    // Método para obtener un objeto del pool
    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;  // Si encontramos uno inactivo, lo reutilizamos
            }
        }

        // Si no hay objetos disponibles, podemos instanciar uno nuevo (opcional)
        GameObject newObj = Instantiate(objectPrefab);
        newObj.SetActive(false);
        objectPool.Add(newObj);
        return newObj;
    }
}

