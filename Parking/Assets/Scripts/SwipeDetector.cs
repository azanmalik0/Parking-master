using HedgehogTeam.EasyTouch;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwipeDetector : MonoBehaviour
{
    
    public static SwipeDetector Instance;
    public bool  moveF,moveB;
    public Camera cam;
    public Collider targetVeh;


    private void Update()
    {
        if (moveF == true)
        {
            if (targetVeh.GetComponent<Collider>().CompareTag("Car"))
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().Forward();

        }

        else if (moveB == true)
        {
            if (targetVeh.GetComponent<Collider>().CompareTag("Car"))
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().Back();


        }
    }
    private void Awake()
    {
        Instance= this;
        cam = FindObjectOfType<Camera>();
    }
    
    void OnEnable()
    {

        EasyTouch.On_SwipeStart += On_SwipeStart;
        EasyTouch.On_Swipe += On_Swipe;
        EasyTouch.On_SwipeEnd += On_SwipeEnd;
    }
    // Unsubscribe
    void OnDisable()
    {
        EasyTouch.On_SwipeStart -= On_SwipeStart;
        EasyTouch.On_Swipe -= On_Swipe;
        EasyTouch.On_SwipeEnd -= On_SwipeEnd;
    }
    // Unsubscribe
    void OnDestroy()
    {
        EasyTouch.On_SwipeStart -= On_SwipeStart;
        EasyTouch.On_Swipe -= On_Swipe;
        EasyTouch.On_SwipeEnd -= On_SwipeEnd;
    }

    private void On_SwipeEnd(Gesture gesture)
    {
        targetVeh = null;
        print("Swipe End");
       
    }

    private void On_Swipe(Gesture gesture)
    {
        print("Swipe");
        targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().speed = 800;
        targetVeh.GetComponent<Collider>().gameObject.GetComponent<Rigidbody>().isKinematic = false;

        

        //}
        if (targetVeh.GetComponent<Collider>().transform.eulerAngles.y == 180)
        {

            if (gesture.swipe == EasyTouch.SwipeDirection.Up)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;


                moveB = true;
                moveF = false;
                Debug.Log("UP");


            } 
            else if (gesture.swipe == EasyTouch.SwipeDirection.UpLeft)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;


                moveB = true;
                moveF = false;
                Debug.Log("UPLEFT");


            } 
            else if (gesture.swipe == EasyTouch.SwipeDirection.UpRight)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;


                moveB = false;
                moveF = true;
                Debug.Log("UPRIGHT");


            }

            else if (gesture.swipe == EasyTouch.SwipeDirection.Down)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;

                moveF = true;
                moveB = false;
                Debug.Log("DOWN");



            }  
            else if (gesture.swipe == EasyTouch.SwipeDirection.DownRight)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;

                moveF = true;
                moveB = false;
                Debug.Log("DOWNRIGHT");



            } 
            else if (gesture.swipe == EasyTouch.SwipeDirection.DownLeft)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;

                moveF = true;
                moveB = false;
                Debug.Log("DOWNLEFT");



            } 
            else if (gesture.swipe == EasyTouch.SwipeDirection.Right)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;


                moveB = false;
                moveF = true;
                Debug.Log("Right");


            }

            else if (gesture.swipe == EasyTouch.SwipeDirection.Left)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;

                moveF = false;
                moveB = true;
                Debug.Log("Left");



            }
        }
        else
        {
            if (gesture.swipe == EasyTouch.SwipeDirection.Up)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;
                moveF = false;
                moveB = true;



                Debug.Log("Up");

            }
            if (gesture.swipe == EasyTouch.SwipeDirection.UpLeft)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;
                moveF = false;
                moveB = true;



                Debug.Log("UpLeft");

            }
            if (gesture.swipe == EasyTouch.SwipeDirection.UpRight)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;
                moveF = false;
                moveB = true;



                Debug.Log("UpRight");

            }

            else if (gesture.swipe == EasyTouch.SwipeDirection.Down)
            {

                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;
                moveF = true;
                moveB = false;

                Debug.Log("Down");




            }
            else if (gesture.swipe == EasyTouch.SwipeDirection.DownLeft)
            {

                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;
                moveF = true;
                moveB = false;

                Debug.Log("DownLeft");




            }
            else if (gesture.swipe == EasyTouch.SwipeDirection.DownRight)
            {

                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;
                moveF = true;
                moveB = false;

                Debug.Log("DownRight");




            }

            if (gesture.swipe == EasyTouch.SwipeDirection.Left)
            {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;
                moveF = true;
                moveB = false;

                Debug.Log("Left");

            }

            else if (gesture.swipe == EasyTouch.SwipeDirection.Right)
            {

                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;
                moveF = false;
                moveB = true;

                Debug.Log("Right");




            }
        }
        



        



    }

    private void On_SwipeStart(Gesture gesture)
    {
        Debug.Log("swipeStart");
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Car"))
                {
                    targetVeh = hit.collider;
                    Debug.Log("Here");

                }
            }
        }
    }

    public void inLevelButtons(string str)
    {
        if (str == "Retry")
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}
