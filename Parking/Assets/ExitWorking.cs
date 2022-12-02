using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWorking : MonoBehaviour
{
    public Transform[] wayPoint;
    private GameManager GM;
   //[SerializeField] public int current;
    private void Start()
    {
        GM=GameManager.Instance;

        //for(int i=0;i<=wayPoint.Length;i++)
        //{
        //    wayPoint[i] = GameObject.FindGameObjectsWithTag("WayPoint");
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogError("H " + other.gameObject.transform.rotation.y);
        
        other.gameObject.transform.Rotate(0, other.gameObject.transform.rotation.y + 90, 0);
        //////current = System.Array.IndexOf(wayPoint,other.gameObject);

        ////int i = 0;
        ////other.gameObject.GetComponent<CollisionController>().MoveTo(wayPoint[i]);
        ////if (GM.hitInfo.collider.transform.position.x == wayPoint[i].transform.position.x)
        ////{
        ////    i++;
        ////}

    }
   
}
