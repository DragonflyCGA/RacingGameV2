using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class carSelection : MonoBehaviour
{
    public Transform[] cars = new Transform[3];                         // List of car prefabs
    public Text carName;                                                // Name of the current car

    int activeCarIndex = 0;                                             // The index of the current car in the cars array

    void Start()
    {
        setCarName();
    }

    void Update()
    {
        // Rotates the cars slowly
        cars[0].Rotate(0.0f, Time.deltaTime * 10, 0.0f, Space.Self);
        cars[1].Rotate(0.0f, Time.deltaTime * 10, 0.0f, Space.Self);
        cars[2].Rotate(0.0f, Time.deltaTime * 10, 0.0f, Space.Self);
        cars[3].Rotate(0.0f, Time.deltaTime * 10, 0.0f, Space.Self);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    // Shows the next car to the user
    public void NextCar()
    {
        if (activeCarIndex < cars.Length-1)
        {
            cars[activeCarIndex].gameObject.SetActive(false);
            activeCarIndex++;
            cars[activeCarIndex].gameObject.SetActive(true);

            setCarName();
        }
    }

    // Shows the previous car to the user
    public void PreviousCar()
    {
        if (activeCarIndex > 0)
        {
            cars[activeCarIndex].gameObject.SetActive(false);
            activeCarIndex--;
            cars[activeCarIndex].gameObject.SetActive(true);

        }

        setCarName();
    }

    // Sets the car name based on the active index
    void setCarName()
    {
        switch (activeCarIndex)
        {
            case 0:
                carName.text = "Car 1";
                break;
            case 1:
                carName.text = "Car 2";
                break;
            case 2:
                carName.text = "Car 3";
                break;
            case 3:
                carName.text = "Car 4";
                break;
        }
    }

    // Fills the PersistentData class with the selected car name and loads the main scene
    public void startGame()
    {
        PersistentData.selectedCarIndex = activeCarIndex;
        PersistentData.Level = 2;
        SceneManager.LoadScene(0);//was 0
    }

    // Quit the game
    public void Quit()
    {
        Application.Quit();
    }
}
