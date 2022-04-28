using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour
{
    [Header("Car Desciption")]
    [SerializeField] private Text carName;
    [SerializeField] private Text carDescript;
    [SerializeField] private Text carPrice;

    [Header("Car Stats")]
    [SerializeField] private Image carSpeed;
    [SerializeField] private Image carAccel;
    [SerializeField] private Image carHandeling;

    [Header("Car Model")]
    [SerializeField] private Transform carHolder;

    public void DisplayCar (Car _car) {
        carName.text = _car.carName;
        carDescript.text = _car.carDescription;
        carPrice.text = _car.carPrice + "$";

        carSpeed.fillAmount = _car.speed / 100;
        carHandeling.fillAmount = _car.handling / 100;
        carAccel.fillAmount = _car.acceleration / 100;
    }
}
