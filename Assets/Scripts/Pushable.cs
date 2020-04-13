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

        Debug.Log("push " + gameObject.name + " dir " + movementVector);

        RaycastHit movementHit;
        bool allowedToMove = true;
        Vector3 raycastOrigin = transform.position;

        if (Physics.Raycast(raycastOrigin, movementVector, out movementHit, 0.5f))
        {
            GameObject objectHit = movementHit.transform.gameObject;
            allowedToMove = PromptPush(objectHit, movementVector);
        }

        if (allowedToMove)
        {
            RaycastHit carryInfo;
            if (Physics.Raycast(raycastOrigin, Vector3.up, out carryInfo, 0.5f))
            {
                GameObject objectCarrying = carryInfo.transform.gameObject;
                PromptPush(objectCarrying, movementVector);
            }

            transform.Translate(movementVector);
        }

        return allowedToMove;
    }

}
