using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public float Rotateer;
    public GameHandler GH;

    void Start()
    {
        GH = GameObject.Find("Canvus").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Rotateer, 0 , Space.World);
    }

    private void OnTriggerEnter(Collider other) {
        GH.coins++;
        Destroy(gameObject);
    }

    
}