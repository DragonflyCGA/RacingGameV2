using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketPickup : MonoBehaviour
{
    public float Rotateer;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, Rotateer, 0 , Space.World);
    }

    private void OnTriggerEnter(Collider other) {
        
        Destroy(gameObject);
    }
}
