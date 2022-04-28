using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public void Racetrack1(){
        SceneManager.LoadScene("T1");
    }

    public void Racetrack2(){
        SceneManager.LoadScene("T2");
    }
    public void Racetrack3(){
        SceneManager.LoadScene("T3");
    }

    public void Racetrack4(){
        SceneManager.LoadScene("T4");
    }
}
