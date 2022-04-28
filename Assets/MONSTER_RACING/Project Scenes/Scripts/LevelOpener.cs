using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOpener : MonoBehaviour
{
    // Start is called before the first frame update
    public void Track1(){
        SceneManager.LoadScene("T2");
    }
}
