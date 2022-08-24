using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using Akali.Scripts.Utilities;
using Akali.Ui_Materials.Scripts;
using Akali.Ui_Materials.Scripts.Components;
using DG.Tweening;
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
    public float moveSpeed;
    private Vector3 FirstPos;
    private Vector3 LastPos;
    public GameObject playerMesh;
    public Animator animator;
    [SerializeField] private float forceStrenght;
    public GameObject bomb;

    private float Distance;  //Minimum distance for a Swipe.
        
    private void Awake()
    {
        GameStateManager.Instance.GameStatePlaying.OnExecute += MovePlayer;
        GameStateManager.Instance.GameStatePlaying.OnExecute += UpdateSwipe;
        negativeMovementUnit = (-1 * movementUnit);
        RagdollFail(false);
    }
    
    void Start()
    {

        Distance = Screen.height * 15 / 150; //DragDistance.

    }

    private void MovePlayer()
    {
        RayControl(Vector3.forward);
        RayControl(Vector3.left);
        RayControl(Vector3.right);
        RayControl(Vector3.back);
        if (transform.position == desiredPosition) { Movement = new Vector3(0.0f, 0.0f, 0.0f); movementOnGoing = true;}
    }

    #region SwipeControl

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
    
    void SwipeLeft()
    {
        if (movementOnGoing == true){
            if (moveLeft) Movement = new Vector3(negativeMovementUnit, 0.0f, 0.0f);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
            animator.Play("Walk");
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.DOMove(smoothPosition,moveSpeed);
        playerMesh.transform.DOLocalRotate(new Vector3(0,-90,0), 0.2f);
    }

    void SwipeRight()
    {
        if (movementOnGoing == true){
            if (moveRight) Movement = new Vector3(movementUnit, 0.0f, 0.0f);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
            animator.Play("Walk");
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.DOMove(smoothPosition,moveSpeed);
        playerMesh.transform.DOLocalRotate(new Vector3(0,90,0), 0.2f);
    }

    void SwipeUp()
    {
        if (movementOnGoing == true){
            if (moveForward) Movement = new Vector3(0.0f, 0.0f, movementUnit);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
            animator.Play("Walk");
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.DOMove(smoothPosition,moveSpeed);
        playerMesh.transform.DOLocalRotate(new Vector3(0,0,0), 0.2f);
    }

    void SwipeDown()
    {
        if (movementOnGoing == true){
            if (moveBack) Movement = new Vector3(0.0f, 0.0f, negativeMovementUnit);
            movementOnGoing = false;
            desiredPosition = transform.position + Movement;
            animator.Play("Walk");
        }
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.DOMove(smoothPosition,moveSpeed);
        playerMesh.transform.DOLocalRotate(new Vector3(0,-180,0), 0.2f);
    }


    #endregion
    
    

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
            if (hit.collider.gameObject.layer == Constants.Treasure)
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
                TakeTreasure(hit);
            }
            
            if (hit.collider.gameObject.layer == Constants.GoldTreasure)
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
                TakeGoldTreasure(hit);
            }

            if (hit.collider.gameObject.layer == Constants.Final)
            {
                if (PlayerManager.Instance.haveKey)
                {
                    StartCoroutine(PlayerManager.Instance.CompleteGame());
                    CameraController.Instance.Final();
                    MoneyText.Instance.IncreaseMoney(250);
                }
            }
            
            if (hit.collider.gameObject.layer == Constants.Key)
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
                TakeKey(hit);
            }

            
            
            if (hit.collider.gameObject.layer == Constants.Door)
            {
                Final(hit);
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

    public void TakeTreasure(RaycastHit hit)
    {
        animator.Play("Put");
        GameStateManager.Instance.GameStatePlaying.OnExecute -= MovePlayer;
        GameStateManager.Instance.GameStatePlaying.OnExecute -= UpdateSwipe;
        playerMesh.transform.DOLookAt(new Vector3(0, hit.collider.gameObject.transform.position.y, 0), 0.2f);
        CameraController.Instance.Treasure();
        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
        hit.collider.GetComponent<Treasure>().OpenChest();
        Invoke("InvokeMovement",2f);
        GameUiManager.Instance.Notif("Treasure Found",Color.green);
        MoneyText.Instance.IncreaseMoney(100);
    }

    public void Final(RaycastHit hit)
    {
        if (PlayerManager.Instance.haveKey)
        {
            animator.Play("Finish");
            playerMesh.transform.DOLocalRotate(new Vector3(0,-180,0), 0.2f);
            GameStateManager.Instance.GameStatePlaying.OnExecute -= MovePlayer;
            GameStateManager.Instance.GameStatePlaying.OnExecute -= UpdateSwipe;
            CameraController.Instance.Final();
            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
            hit.collider.transform.parent.GetChild(0).DOScale(3, 1f);
            hit.collider.gameObject.transform.DOScale(0, 0.2f).OnComplete(() => transform.DOMoveZ(transform.position.z + 2,1));
            //Animation
            //Particle
            StartCoroutine(PlayerManager.Instance.CompleteGame());
        }
        else
        {
            GameUiManager.Instance.Notif("Key Not Found",Color.red);
        }
    }
    
    public void TakeKey(RaycastHit hit)
    {
        animator.Play("Put");
        GameUiManager.Instance.Notif("Key Found",Color.green);
        GameUiManager.Instance.keybar.text = "1/1";
        PlayerManager.Instance.haveKey = true;
        MoneyText.Instance.IncreaseMoney(100);
    }
    
    public void TakeGoldTreasure(RaycastHit hit)
    {
        animator.Play("Put");
        GameStateManager.Instance.GameStatePlaying.OnExecute -= MovePlayer;
        GameStateManager.Instance.GameStatePlaying.OnExecute -= UpdateSwipe;
        playerMesh.transform.DOLookAt(new Vector3(0, hit.collider.gameObject.transform.position.y, 0), 0.2f);
        CameraController.Instance.Treasure();
        hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
        hit.collider.transform.DOScale(0,1);
        Invoke("InvokeMovement",2f);
        MoneyText.Instance.IncreaseMoney(100);
    }

    public void InvokeMovement()
    {
        GameStateManager.Instance.GameStatePlaying.OnExecute += MovePlayer;
        GameStateManager.Instance.GameStatePlaying.OnExecute += UpdateSwipe;
    }

    public void Fail()
    {
        RagdollFail(true);
        GameStateManager.Instance.GameStatePlaying.OnExecute -= MovePlayer;
        GameStateManager.Instance.GameStatePlaying.OnExecute -= UpdateSwipe;
        transform.GetChild(0).GetComponent<Rigidbody>().AddForce(Vector3.up * forceStrenght);
        Instantiate(bomb, transform.position, transform.rotation);
    }

    public void RagdollFail(bool x)
    {
        transform.DOKill();
        animator.enabled = !x;
        transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = !x;
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        { 
            rb.isKinematic = !x;
        }
        foreach (var col in GetComponentsInChildren<Collider>())
        {
            col.isTrigger= !x;
        }
    }
}



