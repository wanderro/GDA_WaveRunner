using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] 
    private float _rotationDelta;

    void Update()
    {
        transform.Rotate(0,0,_rotationDelta);
    }
}
