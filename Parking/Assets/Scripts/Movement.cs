using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    Ray navRay;
    Ray frontRay;
    Ray backRay;
    RaycastHit hitStopInfo;
    public LayerMask obstacleMask;
    public LayerMask Mask;
    public bool frontRaycastActive=true;
    public bool backRaycastActive=true;


    public NavMeshAgent agent;
    public Transform exitPoint;


    public static Movement Instance;
    public float speed;
    public bool F, B;
    private GameManager GM;
    private Transform body;
    public bool exitFromEnv = false;
    private Transform previousHitObject;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GM = GameManager.Instance;
        GM = GameManager.Instance;
        body = gameObject.transform.GetChild(0);
    }
    private void Update()
    {
        //---------Forward Navmesh Raycast------------

        navRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(navRay, out hitStopInfo, 3.5f, Mask))
        {
            Debug.DrawLine(navRay.origin, hitStopInfo.point, Color.green);
            StartCoroutine(NavmeshDelay());
        }
        else
        {
            Debug.DrawLine(navRay.origin, navRay.origin + navRay.direction * 5, Color.red);

        }
        //---------Backward Navmesh Raycast------------

        navRay = new Ray(transform.position, -transform.forward);
        if (Physics.Raycast(navRay, out hitStopInfo, 3.5f, Mask))
        {
            Debug.DrawLine(navRay.origin, hitStopInfo.point, Color.green);
            StartCoroutine(NavmeshDelay());
        }
        else
        {
            Debug.DrawLine(navRay.origin, navRay.origin + navRay.direction * 5, Color.red);

        }
        //--------------Forward Raycast -----------------


        frontRay = new Ray(new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), transform.forward);
        if (Physics.Raycast(frontRay, out hitStopInfo, 2.5f, obstacleMask) )
        {
            if(this.gameObject.GetComponent<NavMeshAgent>().enabled == false && frontRaycastActive == true)
            {
                //print("here");
            Debug.DrawLine(frontRay.origin, hitStopInfo.point, Color.green);
            StartCoroutine(ForwardDelay());
               
            }

        }
        else
        {
            Debug.DrawLine(frontRay.origin, frontRay.origin + frontRay.direction * 5, Color.red);

        }


        //-------------Backward Raycast-------------------


        backRay = new Ray(new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), -transform.forward);
        if (Physics.Raycast(backRay, out hitStopInfo, 2.5f, obstacleMask) )
        {
            if(this.gameObject.GetComponent<NavMeshAgent>().enabled == false && backRaycastActive == true)
            {
                //print("here");
            Debug.DrawLine(backRay.origin, hitStopInfo.point, Color.green);
            StartCoroutine(BackwardDelay());
               
            }

        }
        else
        {
            Debug.DrawLine(backRay.origin, backRay.origin + backRay.direction * 5, Color.red);

        }

        //---------------------------------





        if (F == true)
        {

            transform.GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.deltaTime;

        }
        else if (B == true)
        {
            transform.GetComponent<Rigidbody>().velocity = transform.forward * -speed * Time.deltaTime;

        }

    }
    public void Back()
    {

        F = false; B = true;

    }
    public void Forward()
    {

        F = true; B = false;
    }



    private void OnCollisionEnter(Collision collision)
    {

        if (exitFromEnv == false)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                //print("Obstacle");
                //if (previousHitObject == null || collision.transform != previousHitObject)
                //{
                //    previousHitObject = collision.transform;

                //    F = false;
                //    B = false;

                //    //----Animation-----

                //    //collision.gameObject.transform.DORotate(new Vector3(23, collision.gameObject.transform.eulerAngles.y, 0), 0.2f).OnComplete(() =>
                //    //{
                //    //    collision.gameObject.transform.DORotate(new Vector3(0, collision.gameObject.transform.eulerAngles.y, 0), 0.0f);
                //    //});
                //    Vibration.Vibrate(20);
                //    speed = 0;
                    
                //}



            }
            if (collision.gameObject.CompareTag("Car") && GameManager.Instance.currentPlayer == this.transform && exitFromEnv == false)
            {
                //print("Car");

                //previousHitObject = null;
                //if (GM.f == true)
                //{


                //    F = false;
                //    B = false;
                //    //----Animation-----

                //    //body.DORotate(new Vector3(2.5f, body.eulerAngles.y, 0), 0.2f).OnComplete(() =>
                //    //{
                //    //    body.DORotate(new Vector3(0, body.eulerAngles.y, 0), 0.0f);
                //    //});

                //    Vibration.Vibrate(20);
                //    collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //    GM.f = false;
                //    speed = 0;
                //    //agent.SetDestination(collision.gameObject.transform.position);
                //    //print("culprit");
                    

                //}
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("ExitEnv"))
        {
            //print("Exitfrom env");
            this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = true;
            agent.SetDestination(exitPoint.position);
            exitFromEnv = true;

        }
        if (other.gameObject.CompareTag("Exit"))
        {
            gameObject.SetActive(false);
        }
        

    }
    IEnumerator NavmeshDelay()
    {
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        speed = 800;
        

    }
    IEnumerator ForwardDelay()
    {
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        frontRaycastActive = false;
        

    }
    IEnumerator BackwardDelay()
    {
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        backRaycastActive = false;
        

    }

}
