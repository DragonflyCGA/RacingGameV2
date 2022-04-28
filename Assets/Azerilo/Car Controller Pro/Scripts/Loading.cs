using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class called when we are going from one scene to another scene
public class Loading : MonoBehaviour
{

    public Slider slider;           // Reference to the slider UI element
    public float haltTime = 2;      // Halts the loading scene in seconds. You can put zero here to loading screen loads as fast as it can

    void Start()
    {
        StartCoroutine(LateStart(2f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadLevel(PersistentData.Level);
    }

    // Loads the selected scene asynchronously and shows the loading bar
    void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
        slider.gameObject.SetActive(true);
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
}
