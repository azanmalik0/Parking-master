using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Movement : MonoBehaviour
{
    Vector3 startPos;
    Vector3 lastPos;
    float dragDistance;
    public Camera cam;
    RaycastHit hitInfo;
    Ray ray;
    public bool moveF, moveB = false;
    [SerializeField]
    Transform body;
    //Rigidbody rb;
    public float speed;

    public bool f = true;




    private void Start()
    {
        //rb= GetComponent<Rigidbody>();
        dragDistance = Screen.height * 15 / 100;
        body = gameObject.transform.GetChild(0);
        print(f);
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

            if (Physics.Raycast(ray, out hitInfo,100f))
            {
                
                if (hitInfo.collider.gameObject.tag == "Car"&& hitInfo.collider.gameObject==this.gameObject)
                {
                    if (Mathf.Abs(lastPos.x - startPos.x) > dragDistance || Mathf.Abs(lastPos.y - startPos.y) > dragDistance)
                    {
                        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        if (Mathf.Abs(lastPos.x - startPos.x) > Mathf.Abs(lastPos.y - startPos.y))
                        {
                            


                            if (startPos.x < lastPos.x)
                            {
                                moveF = true;
                                //moveB= false;


                            }
                            else if (startPos.x > lastPos.x)
                            {
                                moveB = true;
                                //moveF= false;


                            }
                        }
                        else
                        {
                            if (startPos.y < lastPos.y)
                            {
                                moveB = true;
                                //moveF = false;



                            }
                            if (startPos.y > lastPos.y)
                            {
                                moveF = true;
                                //moveB = false;
                                


                            }
                        }
                        print("Swipe");
                        f = true;
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
            
            //moveB = false;
            Down();
            
        }

        if (moveB == true)
        {

            //moveF = false;
            Up();
            
        }

    }
    void Down()
    {
        if (hitInfo.collider.CompareTag("Car") && hitInfo.collider.gameObject == this.gameObject)
            //hitInfo.collider.transform.Translate(0, 0, 10 * Time.deltaTime);
            hitInfo.collider.GetComponent<Rigidbody>().velocity = transform.forward * -speed;
            //hitInfo.collider.GetComponent<Rigidbody>().MovePosition(transform.forward * speed);
    }
    void Up()
    {
        if (hitInfo.collider.CompareTag("Car") && hitInfo.collider.gameObject == this.gameObject)
            //hitInfo.collider.transform.Translate(0, 0, -10 * Time.deltaTime);
            hitInfo.collider.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            //hitInfo.collider.GetComponent<Rigidbody>().MovePosition(transform.forward * -speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") )
        {
                
            moveB = false;
            moveF = false;

            //----Animation-----

            collision.gameObject.transform.DORotate(new Vector3(23, collision.gameObject.transform.eulerAngles.y, 0), 0.2f).OnComplete(() =>
            {
                collision.gameObject.transform.DORotate(new Vector3(0, collision.gameObject.transform.eulerAngles.y, 0), 0.0f);
            });


            Vibration.Vibrate(20);
            

        }
        if (collision.gameObject.CompareTag("Car") && hitInfo.collider.gameObject)
        {



            

            if (f == true)
            {
                //----Animation---- -

                body.DORotate(new Vector3(2.5f, body.eulerAngles.y, 0), 0.2f).OnComplete(() =>
                {
                    body.DORotate(new Vector3(0, body.eulerAngles.y, 0), 0.0f);
                });

                Vibration.Vibrate(20);

                moveB = false;
                moveF = false;
                print("REached");
               // collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
               // collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                f=false;
            }
            

        }
    }

   
    //IEnumerator moveDelay()
    //{

    //    yield return new WaitForSeconds(0.5f);
    //    this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

    //}

}
