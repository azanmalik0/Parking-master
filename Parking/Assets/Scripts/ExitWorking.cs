//using DG.Tweening;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ExitWorking : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        
        
        other.gameObject.transform.Rotate(0, other.gameObject.transform.rotation.y + 90, 0);
        

    }
   
}
