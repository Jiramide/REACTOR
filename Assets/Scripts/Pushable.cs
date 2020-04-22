using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{

    private LevelLayout grid;
    private void Start()
    {
        grid = LevelLayout.GetInstance();
        grid.AddEntity(gameObject);
    }

    private bool PromptPush(GameObject objectToPush, Vector3 pushDirection)
    {
        return objectToPush.GetComponent<Pushable>()
            ?.Push(pushDirection)
            ?? false;
    }

    private GameObject getAdjacent(Vector3 offset)
    {
        Vector3 adjacentPos = transform.position + offset;

        return grid.GetAt((int) adjacentPos.x, (int) adjacentPos.y, (int) adjacentPos.z);
    }

    public bool Push(Vector3 movementVector)
    {

        bool allowedToMove = true;
        bool moveSuccess = false;
        GameObject objectPushed = getAdjacent(movementVector);

        if (objectPushed != null)
        {
            allowedToMove = PromptPush(objectPushed, movementVector);
        }

        if (allowedToMove)
        {
            GameObject objectCarried = getAdjacent(Vector3.up);
            if (objectCarried != null)
            {
                PromptPush(objectCarried, movementVector);
            }

            moveSuccess = grid.MoveObject(
                gameObject,
                (int) (transform.position.x + movementVector.x),
                (int) (transform.position.y + movementVector.y),
                (int) (transform.position.z + movementVector.z)
            );

            if (moveSuccess)
            {
                transform.Translate(movementVector);
            }
        }

        return allowedToMove && moveSuccess;
    }

}
