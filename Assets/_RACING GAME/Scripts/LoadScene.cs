using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadScene_btn(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
