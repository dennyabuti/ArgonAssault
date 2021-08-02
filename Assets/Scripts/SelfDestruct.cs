using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float selfDestroyTime = 3f;
    void Start()
    {
        Destroy(gameObject, selfDestroyTime);
    }

}
