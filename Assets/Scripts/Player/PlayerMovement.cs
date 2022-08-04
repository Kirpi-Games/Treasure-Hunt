using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using Akali.Scripts.Utilities;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    private bool movementOnGoing = true;
    [SerializeField]private float smoothSpeed;
    public float movementUnit = 1;
    private float negativeMovementUnit;
    [SerializeField]private float distance = 1;
    [SerializeField]private float rayOffsetY = 1;
    private Vector3 Movement;
    private Vector3 desiredPosition;
    private Vector3 smoothPosition;
    private bool moveForward = true;
    private bool moveLeft = true;
    private bool moveRight = true;
    private bool moveBack = true;
    RaycastHit hit;

    private Vector3 FirstPos;
    private Vector3 LastPos;  

    private float Distance;  //Minimum distance for a Swipe.
        
    private void Awake()
    {
        GameStateManager.Instance.GameStatePlaying.OnExecute += MovePlayer;
        GameStateManager.Instance.GameStatePlaying.OnExecute += UpdateSwipe;
        negativeMovementUnit = (-1 * movementUnit);
    }
    
    void Start()
    {

        Distance = Screen.height * 15 / 100; //DragDistance.

    }

    void UpdateSwipe()
    {

        if (Input.touchCount == 1) // User TouchCount.
        {

            Touch touch = Input.GetTouch(0); // Get Touch.
            if (touch.phase == TouchPhase.Began) //Check for First Touch.
            {

                FirstPos = touch.position;
                LastPos = touch.position;

            }
            else if (touch.phase == TouchPhase.Moved) // Update the last position.
            {

                LastPos = touch.position;

            }
            else if (touch.phase == TouchPhase.Ended) //Check finger.
            {
                LastPos = touch.position; //last touch position.

                //Check distance.
                if (Mathf.Abs(LastPos.x - FirstPos.x) > Distance || Mathf.Abs(LastPos.y - FirstPos.y) > Distance)
                {

                    if (Mathf.Abs(LastPos.x - FirstPos.x) > Mathf.Abs(LastPos.y - FirstPos.y))
                    {
                        if ((LastPos.x > FirstPos.x))
                        {
                            SwipeRight();
                        }
                        else
                        {
                            SwipeLeft();
                        }
                    }
                    else
                    {
                        if (LastPos.y > FirstPos.y)
                        {
                            SwipeUp();
                        }
                        else
                        {
                            SwipeDown();
                        }
                    }
                }
            }
        }
    }

    private void MovePlayer()
    {
        RayControl(Vector3.forward);
        RayControl(Vector3.left);
        RayControl(Vector3.right);
        RayControl(Vector3.back);
    }

    void SwipeLeft()
    {
        if (movementOnGoing == true){
            if (moveLeft) Movement = new Vector3(negativeMovementUnit, 0.0f, 0.0f);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
        if (transform.position == desiredPosition) { Movement = new Vector3(0.0f, 0.0f, 0.0f); movementOnGoing = true;}
        Debug.Log("Left Swipe");
    }

    void SwipeRight()
    {
        if (movementOnGoing == true){
            if (moveRight) Movement = new Vector3(movementUnit, 0.0f, 0.0f);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
        if (transform.position == desiredPosition) { Movement = new Vector3(0.0f, 0.0f, 0.0f); movementOnGoing = true;}
        
        Debug.Log("Left Swipe");
    }

    void SwipeUp()
    {
        if (movementOnGoing == true){
            if (moveForward) Movement = new Vector3(0.0f, 0.0f, movementUnit);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
        if (transform.position == desiredPosition) { Movement = new Vector3(0.0f, 0.0f, 0.0f); movementOnGoing = true;}
        Debug.Log("Left Swipe");
    }

    void SwipeDown()
    {
        if (movementOnGoing == true){
            if (moveBack) Movement = new Vector3(0.0f, 0.0f, negativeMovementUnit);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothPosition;
        if (transform.position == desiredPosition) { Movement = new Vector3(0.0f, 0.0f, 0.0f); movementOnGoing = true;}
        Debug.Log("Left Swipe");
    }


    private void RayControl(Vector3 direction)
    {
        int layerMask = 1 << 3;
        layerMask = ~layerMask;
        Debug.DrawRay(transform.position + new Vector3(0,rayOffsetY,0),transform.TransformDirection(direction) * distance,Color.blue);
        if (Physics.Raycast(transform.position + new Vector3(0,rayOffsetY,0),transform.TransformDirection(direction),out hit,distance,layerMask))
        {
            if (hit.collider.gameObject.layer == Constants.Barrier)
            {
                if (direction == Vector3.forward)
                {
                    moveForward = false;
                }
                if (direction == Vector3.back)
                {
                    moveBack = false;
                }
                if (direction == Vector3.right)
                {
                    moveRight = false;
                }
                if (direction == Vector3.left)
                {
                    moveLeft = false;
                }
            }
        }
        else
        {
            if (direction == Vector3.forward)
            {
                moveForward = true;
            }
            if (direction == Vector3.back)
            {
                moveBack = true;
            }
            if (direction == Vector3.right)
            {
                moveRight = true;
            }
            if (direction == Vector3.left)
            {
                moveLeft = true;
            }
        }
    }
}



