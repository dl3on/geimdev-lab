using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    public CharacterManager characterManager; // Contains active character's position
    public Transform endLimit; // GameObject that indicates end of map
    public Transform topLimit;
    //public Transform bottomLimit;
    private float offsetX; // initial x-offset between camera and Mario
    private float offsetY;
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float startY;
    private float endY;
    private float viewportHalfWidth;
    private float verticalThreshold; // The Y position where the camera starts moving
    public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); // the z-component is the distance of the resulting plane from the camera 
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - transform.position.x);

        verticalThreshold = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;

        offsetX = transform.position.x - characterManager.activeCharacter.transform.position.x;
        offsetY = transform.position.y - characterManager.activeCharacter.transform.position.y;
        startX = transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
        startY = transform.position.y;
        endY = topLimit.transform.position.y - viewportHalfWidth;

    }

    void Update()
    {
        Transform activeCharacterPos = characterManager.activeCharacter.transform;

        float desiredX = activeCharacterPos.position.x + offsetX;
        float desiredY = transform.position.y;

        if (activeCharacterPos.position.y > verticalThreshold)
        {
            desiredY = activeCharacterPos.position.y;
        }

        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
            transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);
        if (desiredY > startY && desiredY < endY)
            transform.position = new Vector3(transform.position.x, desiredY, transform.position.z);
    }

    public void GameRestart()
    {
        transform.position = startPosition; //new Vector3(5.12f, 5.6f, -10);
    }
}
