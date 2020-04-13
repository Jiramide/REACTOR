using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{

    private bool PromptPush(GameObject objectToPush, Vector3 pushDirection)
    {
        return objectToPush.GetComponent<Pushable>()
            ?.Push(pushDirection)
            ?? false;
    }

    public bool Push(Vector3 movementVector)
    {

        RaycastHit movementHit;
        bool allowedToMove = true;

        if (Physics.Raycast(transform.position, movementVector, out movementHit, 1.0f))
        {
            GameObject objectHit = movementHit.transform.gameObject;
            allowedToMove = PromptPush(objectHit, movementVector);
        }

        if (allowedToMove)
        {
            transform.Translate(movementVector);

            RaycastHit carryInfo;
            if (Physics.Raycast(transform.position, Vector3.up, out carryInfo, 1.0f))
            {
                GameObject objectCarrying = carryInfo.transform.gameObject;
                PromptPush(objectCarrying, movementVector);
            }
        }

        return allowedToMove;
    }

}
