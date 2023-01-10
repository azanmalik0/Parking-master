using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Collider targetVeh;
    public LayerMask rayMask;
    public Image Rpanel;
    public static GameManager Instance;
    Vector3 startPos;
    Vector3 lastPos;
    Ray ray;
    public Camera cam;
     RaycastHit hitinfo;
    float dragDistance;
    public bool moveF, moveB = false;
    public bool f, swipeActive;
    public float targetVehicleMovement;
    public bool targetVehicleIsKinematic;
    public float radius;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        dragDistance = Screen.height * 15 / 500;

    }


    void FixedUpdate()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
            ray = cam.ScreenPointToRay(startPos);
            if (Physics.SphereCast(ray, radius ,out hitinfo, 500, rayMask))
            {
                print("Registered");
                targetVeh = hitinfo.collider;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            lastPos = Input.GetTouch(0).position;

           
                    if (Mathf.Abs(lastPos.x - startPos.x) > dragDistance || Mathf.Abs(lastPos.y - startPos.y) > dragDistance)
                    {
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().speed=800;
                targetVeh.GetComponent<Collider>().gameObject.GetComponent<Rigidbody>().isKinematic=false;


                if (Mathf.Abs(lastPos.x - startPos.x) > Mathf.Abs(lastPos.y - startPos.y))
                        {


                            // X-AXIS Movement

                            if (targetVeh.GetComponent<Collider>().transform.eulerAngles.y == 90)
                            {
                                if (startPos.x > lastPos.x)
                                {
                                    moveF = true;
                                    moveB = false;
                                }
                                else if (startPos.x < lastPos.x)
                                {

                                    moveB = true;
                                    moveF = false;

                                }
                            }
                            else
                            {
                        print("Here");
                                if (startPos.x < lastPos.x)
                                {
                                    targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;
                                    moveF = true;
                                    moveB = false;
                                }
                                else if (startPos.x > lastPos.x)
                                {
                                    targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;

                                    moveB = true;
                                    moveF = false;

                                }
                            }

                        }
                        else if (Mathf.Abs(lastPos.x - startPos.x) < Mathf.Abs(lastPos.y - startPos.y))
                        {
                            // Y-AXIS Movement
                            if (targetVeh.GetComponent<Collider>().transform.eulerAngles.y == 180f)
                            {
                                if (startPos.y < lastPos.y)
                                {

                                    targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().backRaycastActive = true;


                                    moveB = true;
                                    moveF = false;
                                }
                                if (startPos.y > lastPos.y)
                                {

                                    targetVeh.GetComponent<Collider>().gameObject.GetComponent<Movement>().frontRaycastActive = true;

                                    moveF = true;
                                    moveB = false;

                                }

                            }

                            else
                            {
                                if (startPos.y < lastPos.y)
                                {



                                    moveB = true;
                                    moveF = false;
                                }
                                if (startPos.y > lastPos.y)
                                {


                                    moveF = true;
                                    moveB = false;

                                }
                            }
                        }

                        print("Swipe");
                        f = true;
                    }
                    else
                    {

                    }


        }
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
    public void inLevelButtons(string str)
    {
        if (str == "Retry")
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }

}

