using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfPropellingScript : MonoBehaviour
{
    
    public float speed = 1f;

    // Update is called once per frame
    private void Update()
    {
        
      transform.position = transform.position + (transform.forward * speed * Time.deltaTime);

    }
}