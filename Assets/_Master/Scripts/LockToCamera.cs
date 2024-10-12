using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToCamera : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
