using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{

    public bool Push(Vector3 movementVector)
    {

        RaycastHit hitInfo;
        bool allowedToMove = true;

        if (Physics.Raycast(transform.position, movementVector, out hitInfo, 1.0f))
        {
            GameObject objectHit = hitInfo.transform.gameObject;
            if (objectHit.CompareTag("Pushable"))
            {
                allowedToMove = objectHit.GetComponent<Pushable>().Push(movementVector);
            }
            else if (objectHit.CompareTag("Wall"))
            {
                allowedToMove = false;
            }
        }

        if (allowedToMove)
        {
            transform.Translate(movementVector);
        }

        return allowedToMove;
    }

}
