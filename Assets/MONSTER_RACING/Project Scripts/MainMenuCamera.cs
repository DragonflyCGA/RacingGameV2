using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public float speed; //to track camera movement

    // Start is called before the first frame update
    void Start()
    {
       /* #if UNITY_ANDRIOD
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        QualittySettings.antiAliasing = 0;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        #endif
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved){

            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

            //Clamp the camera to move only a speceif area of the map
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, 75.0f, 265.0f),
                Mathf.Clamp(transform.position.y, 43.37f, 43.37f),
                Mathf.Clamp(transform.position.z, 0.0f, 0.0f));
        }
    }
}
