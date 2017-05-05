using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateToNewPosition : MonoBehaviour {
    [SerializeField] private Transform newTransform;

    private Vector3 oldPosition;
    private bool isInOldPosition = true;

    void Awake()
    {
        oldPosition = transform.position;
    }

    public void ListenerGoToNextPosition()
    {
        if (isInOldPosition)
        {
            isInOldPosition = !isInOldPosition;
            transform.position = newTransform.position;
        }else
        {
            ListenerBackToOldPosition();
        }
    }

    public void ListenerBackToOldPosition()
    {
        isInOldPosition = !isInOldPosition;
        transform.position = oldPosition;
    }
}
