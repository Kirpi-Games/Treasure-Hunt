//<<<<<<< HEAD:Assets/Scripts/Gameplay Controller Scripts/SwipeControlsSystem.cs
﻿using System.Collections;
//=======


using System.Collections;
//>>>>>>> f40f9e49f8fbd371bac5ca5ed09e3449b2312a04:Assets/Scripts/Gameplay Controller Scripts/EasySwipeControlsSystem.cs
using System.Collections.Generic;
using UnityEngine;

public class SwipeControlsSystem : MonoBehaviour {
    //m1a1
   // public Transform A;
    //public Transform B;
    //public Transform C;
    //public Transform D;

    //private float timeCount = 0.5f;
    //m1a1
    public Transform player;
    public GameObject player2;

    //Example text Object.


    //Vectors Position.
    private Vector3 FirstPos;    //First position.
    private Vector3 LastPos;     //Last position.


    // Swipes and tap working on mobile device. For debug use "Arrows" for swipes and "Space" for Tap. 


    // Enter the necessary actions, functions in these Voids.
    void SwipeTap()
    {

        //Tap       

        Debug.Log("Touch. Tap!");

        player2.transform.position += new Vector3(0, 0, -4);


    }

    void SwipeLeft()
    {
    //m1a1
       // transform.rotation = Quaternion.Slerp(B.rotation, A.rotation, timeCount);
        //timeCount = timeCount + Time.deltaTime;
        //m1a1
        transform.rotation *= Quaternion.AngleAxis(20, Vector3.left);
        player.transform.Rotate(0, 100 * Time.deltaTime, 0, 0);
        //Left swipe.        

        Debug.Log("Left Swipe");

    }



    void SwipeRight()
    {
        //m1a1
       // transform.rotation = Quaternion.Slerp(B.rotation, A.rotation, timeCount);
        //timeCount = timeCount + Time.deltaTime;
        //m1a1
       transform.rotation *= Quaternion.AngleAxis(20, Vector3.right);
        player.transform.Rotate(0, -100 * Time.deltaTime, 0, 0);
        //Right swipe.        

        Debug.Log("Right Swipe");

    }

    void SwipeUp()
    {
        //m1a1
       //transform.rotation = Quaternion.Slerp(C.rotation, D.rotation, timeCount);
        //timeCount = timeCount + Time.deltaTime;
        //m1a1

        //up swipe. 
       transform.rotation *= Quaternion.AngleAxis(20, Vector3.up);
        Debug.Log("up Swipe");
         player.transform.Rotate(0, 0, +360 * Time.deltaTime);
    }

    void SwipeDown()
    {
        //player.position += new Vector3(0, -2, 0);
        //Left swipe  
        //m1a1
        //transform.rotation = Quaternion.Slerp(D.rotation, C.rotation, timeCount);
        //timeCount = timeCount + Time.deltaTime;
        //m1a1

        Debug.Log("Down Swipe");
      
        transform.rotation *= Quaternion.AngleAxis(20, Vector3.down);
        player.transform.Rotate(0, 0, -360 * Time.deltaTime);
    }
    // Enter the necessary actions, functions in these Voids END.


    private float Distance;  //Minimum distance for a Swipe.


    void Start()
    {

        Distance = Screen.height * 15 / 100; //DragDistance.

    }


    // Swipe Controls System.
    void Update()
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
                LastPos = touch.position;  //last touch position.

                //Check distance.
                if (Mathf.Abs(LastPos.x - FirstPos.x) > Distance || Mathf.Abs(LastPos.y - FirstPos.y) > Distance)
                {

                    if (Mathf.Abs(LastPos.x - FirstPos.x) > Mathf.Abs(LastPos.y - FirstPos.y))
                    {

                        if ((LastPos.x > FirstPos.x))
                        {

                            // Swipe Right!
                            SwipeRight();

                        }
                        else
                        {

                            // Swipe Left!
                            SwipeLeft();

                        }
                    }
                    else
                    {

                        if (LastPos.y > FirstPos.y)
                        {

                            // Swipe Up!
                            SwipeUp();

                        }
                        else
                        {

                            // Swipe Down!
                            SwipeDown();

                        }
                    }
                }
                else
                {

                    // Tap!
                    SwipeTap();

                }
            }
        }



        // Debug Control from Arrows KeyBoard.     
        if (Input.GetKeyUp("up"))
        {

            //Example KeyBoard. Up Swipe.
            SwipeUp();

        }

        if (Input.GetKeyUp("down"))
        {

            //Example KeyBoard. Down Swipe.
            SwipeDown();

        }

        if (Input.GetKeyUp("left"))
        {

            //Example KeyBoard. Left Swipe.
            SwipeLeft();

        }

        if (Input.GetKeyUp("right"))
        {

            //Example KeyBoard. Right Swipe.
            SwipeRight();

        }

        if (Input.GetKeyUp("space"))
        {

            //Example KeyBoard. Tap!
            SwipeTap();

        }
        // Debug Control from Arrows KeyBoard END.


    }
}