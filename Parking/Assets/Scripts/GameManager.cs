using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Ray ray2;
    private RaycastHit hitStopInfo;
    public GameObject ray2point;


    public Image Rpanel;
    public Transform currentPlayer;
    public static GameManager Instance;
    Vector3 startPos;
    Vector3 lastPos;
    Ray ray;
    public Camera cam;
    public RaycastHit hitInfo;
    float dragDistance;
    public bool moveF, moveB = false;
    public bool f, swipeActive;

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
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            lastPos = Input.GetTouch(0).position;
            ray = cam.ScreenPointToRay(startPos);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            lastPos = Input.GetTouch(0).position;

            if (Physics.Raycast(ray, out hitInfo, 100f))
            {



                if (hitInfo.collider.gameObject.tag == "Car")
                {
                    if (Mathf.Abs(lastPos.x - startPos.x) > dragDistance || Mathf.Abs(lastPos.y - startPos.y) > dragDistance)
                    {

                        hitInfo.collider.gameObject.GetComponent<Movement>().speed = 800;
                        hitInfo.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        currentPlayer = hitInfo.collider.transform;

                        if (Mathf.Abs(lastPos.x - startPos.x) > Mathf.Abs(lastPos.y - startPos.y))
                        {


                            // X-AXIS Movement

                            if (hitInfo.collider.transform.eulerAngles.y == 90)
                            {
                                //print("here");
                                if (startPos.x < lastPos.x)
                                {
                                    moveF = true;
                                    moveB = false;
                                }
                                else if (startPos.x > lastPos.x)
                                {

                                    moveB = true;
                                    moveF = false;

                                }
                            }
                            else
                            {
                                if (startPos.x > lastPos.x)
                                {
                                    hitInfo.collider.gameObject.GetComponent<Movement>().frontRaycastActive = true;
                                    moveF = true;
                                    moveB = false;
                                }
                                else if (startPos.x < lastPos.x)
                                {
                                    hitInfo.collider.gameObject.GetComponent<Movement>().backRaycastActive = true;

                                    moveB = true;
                                    moveF = false;

                                }
                            }

                        }
                        else if (Mathf.Abs(lastPos.x - startPos.x) < Mathf.Abs(lastPos.y - startPos.y))
                        {
                            // Y-AXIS Movement
                            if (hitInfo.collider.transform.eulerAngles.y == 180f)
                            {

                                if (startPos.y < lastPos.y)
                                {

                                    //print("here");
                                    hitInfo.collider.gameObject.GetComponent<Movement>().backRaycastActive = true;


                                    moveB = true;
                                    moveF = false;
                                }
                                if (startPos.y > lastPos.y)
                                {

                                    //print("here");
                                    hitInfo.collider.gameObject.GetComponent<Movement>().frontRaycastActive = true;

                                    moveF = true;
                                    moveB = false;

                                }

                            }

                            else
                            {
                                //print("here");
                                if (startPos.y > lastPos.y)
                                {



                                    moveB = true;
                                    moveF = false;
                                }
                                if (startPos.y < lastPos.y)
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
            }



        }
        if (moveF == true)
        {
            if (hitInfo.collider.CompareTag("Car"))
                hitInfo.collider.gameObject.GetComponent<Movement>().Forward();

        }

        else if (moveB == true)
        {
            if (hitInfo.collider.CompareTag("Car"))
                hitInfo.collider.gameObject.GetComponent<Movement>().Back();


        }

    }
    public void inLevelButtons(string str)
    {
        if (str == "Retry")
        {
            SceneManager.LoadScene("Backup", LoadSceneMode.Single);
        }
    }

}

