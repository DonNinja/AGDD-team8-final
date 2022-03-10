using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public Car car;

    public Color BrakeColorOff;
    public Color BrakeColorOn;

    public SpriteRenderer BrakeLeft;
    public SpriteRenderer BrakeRight;

    // Update is called once per frame
    void Update()
    {
        if (car.throttle == -1)
        {
            BrakeLeft.color = BrakeColorOn;
            BrakeRight.color = BrakeColorOn;
        }
        else
        {
            BrakeLeft.color = BrakeColorOff;
            BrakeRight.color = BrakeColorOff;
        }
    }
}
