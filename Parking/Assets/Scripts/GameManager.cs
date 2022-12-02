using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    Vector3 startPos;
    Vector3 lastPos;
    Ray ray;
    public Camera cam;
    public RaycastHit hitInfo;
    float dragDistance;
    public float speed=15;
    public bool moveF, moveB = false;
    public bool f;
    public static GameObject current=null;
    public CollisionController CC;
    public GameObject[] wayPoints;
    private void Awake()
    {
        Instance= this;
    }
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        CC = CollisionController.instance;
        dragDistance = Screen.height * 15 / 100;
        
    }

    
    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
            ray = cam.ScreenPointToRay(startPos);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            lastPos = Input.GetTouch(0).position;
            
            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                //speed = 0;

                if (hitInfo.collider.gameObject.tag == "Car")
                {
                    if (Mathf.Abs(lastPos.x - startPos.x) > dragDistance || Mathf.Abs(lastPos.y - startPos.y) > dragDistance)
                    {
                        //speed = 15;
                        hitInfo.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                       
                        if (Mathf.Abs(lastPos.x - startPos.x) > Mathf.Abs(lastPos.y - startPos.y))
                        {
                            // X-AXIS Movement

                            if (hitInfo.collider.transform.eulerAngles.y==90)
                            {
                                print("here 1");
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
                                print("here 2");
                                print(hitInfo.collider.transform.rotation.y);
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
                            
                        }
                        else if(Mathf.Abs(lastPos.x - startPos.x) < Mathf.Abs(lastPos.y - startPos.y))
                        {
                            // Y-AXIS Movement
                            if(hitInfo.collider.transform.rotation.y == 180f)
                            {
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
                        
                        if (current == null)
                        {
                        current=hitInfo.collider.gameObject;
                            print("GM");

                        }
                        print("Swipe");
                        f= true;
                    }
                    else
                    {
                        print("Tap");
                    }

                }
            }

        }
        if (moveF == true)
        {

            Forward();
            moveB = false;

        }

       else if (moveB == true)
        {

            Back();
            moveF = false;

        }

    }
    void Back()
    {
        if (hitInfo.collider.CompareTag("Car") /*&& hitInfo.collider.gameObject == this.gameObject*/)
            //hitInfo.collider.GetComponent<Rigidbody>().velocity = transform.forward * -speed;
            hitInfo.collider.transform.Translate(-transform.forward*speed * Time.deltaTime);

    }
    void Forward()
    {
        if (hitInfo.collider.CompareTag("Car") /*&& hitInfo.collider.gameObject == this.gameObject*/)
            //hitInfo.collider.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            hitInfo.collider.transform.Translate(transform.forward*speed * Time.deltaTime);

    }
}
    
