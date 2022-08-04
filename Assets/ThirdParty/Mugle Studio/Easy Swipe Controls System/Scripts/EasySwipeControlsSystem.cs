/////////////////////////////////////////////////////////////////////////////////////////////
//
//	EasySwipeControlsSystem.cs
//	© Artem Goldov (Mugle Studio). All Rights Reserved.
//	http://www.mugle.ru
//
//	Description: "Easy Swipe Controls System" - the simplest solution to create and control 
//  Swipes and Taps on Mobile Devices.
/////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using UnityEngine.UI;
using UnityEngine;

public class EasySwipeControlsSystem : Singleton<EasySwipeControlsSystem> {    
       
    //Example text Object.
    public Text ExampleStatusText;  

    //Vectors Position.
    private Vector3 FirstPos;    //First position.
    private Vector3 LastPos;     //Last position.


    // Swipes and tap working on mobile device. For debug use "Arrows" for swipes and "Space" for Tap. 


    // Enter the necessary actions, functions in these Voids.
    void SwipeTap()
    {

        //Tap       
        ExampleStatusText.text = "Touch. Tap!";
        Debug.Log("Touch. Tap!");

    }

    void SwipeLeft()
    {

        //Left swipe.        
        ExampleStatusText.text = "Touch. Left Swipe";
        Debug.Log("Left Swipe");

    }

    void SwipeRight()
    {

        //Left swipe.        
        ExampleStatusText.text = "Touch. Right Swipe";
        Debug.Log("Left Swipe");

    }

    void SwipeUp()
    {

        //Left swipe. 
        ExampleStatusText.text = "Touch. Up Swipe";
        Debug.Log("Left Swipe");

    }

    void SwipeDown()
    {

        //Left swipe        
        ExampleStatusText.text = "Touch. Down Swipe";
        Debug.Log("Left Swipe");

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
