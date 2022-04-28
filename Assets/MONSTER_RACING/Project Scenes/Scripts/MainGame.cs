using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public float speed;

    void Start()
    {
        #if UNITY_ANDROID
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
            Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;
            transform.Translate ( -touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -35.0f, 25.0f),
                Mathf.Clamp(transform.position.y, 1.0f, 20.0f),
                Mathf.Clamp(transform.position.z, -9.0f, 15.0f));
                
        }
    }
}

