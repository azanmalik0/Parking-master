using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public static CollisionController instance;
    public GameManager GM;
    Transform body;
    public int Current;
    public bool m;
    private void Awake()
    {
        instance=this;
        

       
    }
    private void Start()
    {
        GM = GameManager.Instance;
        body = gameObject.transform.GetChild(0);
    }
    public void MoveTo(Transform T)
    {
        GM.hitInfo.collider.gameObject.transform.position = Vector3.MoveTowards(GM.hitInfo.collider.gameObject.transform.position, T.transform.position, 0f);
        GM.hitInfo.collider.gameObject.transform.LookAt(T.transform.position);
    }
    //void Move()
    // {

    //     GM.hitInfo.collider.gameObject.transform.position = Vector3.Lerp(GM.hitInfo.collider.gameObject.transform.position, GM.wayPoints[Current + 1].transform.position, 0f);
    //     GM.hitInfo.collider.gameObject.transform.LookAt(GM.wayPoints[Current + 1].transform.position);

    // }
    //private void Update()
    //{
    //    //if (m == true)
    //    //{
    //    //    Move();
    //    //}
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    //if (other.CompareTag("WayPoint"))
    //    //{

    //    //    Current = System.Array.IndexOf(GM.wayPoints, other.gameObject);
    //    //    m = true;

    //    //}
       

    //}
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Obstacle") )
        {
            

            GM.moveB = false;
            GM.moveF = false;

            //----Animation-----

            collision.gameObject.transform.DORotate(new Vector3(23, collision.gameObject.transform.eulerAngles.y, 0), 0.2f).OnComplete(() =>
            {
                collision.gameObject.transform.DORotate(new Vector3(0, collision.gameObject.transform.eulerAngles.y, 0), 0.0f);
            });
            Vibration.Vibrate(20);
          this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
          this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

        }
        if (collision.gameObject.CompareTag("Car") /*&& collision.gameObject == GameManager.current*/)
        {
            //print("R");

            if (GM.f == true)
            {
                GM.moveB = false;
                GM.moveF = false;
                //----Animation-----

                body.DORotate(new Vector3(2.5f, body.eulerAngles.y, 0), 0.2f).OnComplete(() =>
                {
                    body.DORotate(new Vector3(0, body.eulerAngles.y, 0), 0.0f);
                });

                Vibration.Vibrate(20);

               print("Reached");
               GM.speed= 0;
               collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
               this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
               //collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
               this.gameObject.GetComponent<Rigidbody>().isKinematic = false;

                GM.f = false;
            }
        }
    }

    
}
