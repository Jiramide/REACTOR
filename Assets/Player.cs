using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector3 horizontalUnit = new Vector3(0.0f, 0.0f, 1.0f);
    public Vector3 verticalUnit = new Vector3(1.0f, 0.0f, 0.0f);

    public float dasLength = 0.2f;
    private bool isDASActive = false;
    private float lastInputConsumed = 0.0f;
    
    private void Update() {

        float horiAxis = Input.GetAxisRaw("Horizontal");
        float vertAxis = Input.GetAxisRaw("Vertical");
        bool horiDown = Input.GetButtonDown("Horizontal");
        bool vertDown = Input.GetButtonDown("Vertical");
        bool horiHeld = horiAxis != 0.0f;
        bool vertHeld = vertAxis != 0.0f;

        isDASActive = horiHeld && !horiDown
            || vertHeld && !vertDown;

        if (!isDASActive && !horiDown && !vertDown) return;
        if (isDASActive && Time.time - lastInputConsumed <= dasLength) return;
        lastInputConsumed = Time.time;

        float axisToConsume = 0.0f;
        Vector3 unitToConsume = Vector3.zero;

        if (horiHeld) {
            axisToConsume = horiAxis;
            unitToConsume = horizontalUnit;
        } else if (vertHeld) {
            axisToConsume = vertAxis;
            unitToConsume = verticalUnit;
        }

        transform.Translate(axisToConsume * unitToConsume);
    }

}
