using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    int counter = 0;

    //-------------ENUMS-------------
    enum Colors
    {
        Red=0, Green=1,
    }

    string a;
    string b;

    private void Start()
    {
        StartCoroutine(Delay());
    }
    void Update()
    {//------------ NULL-Coalesing Operator ?? Example---------

        //-----this-----

        //if(a!=null)
        //{
        //    b =a;
        //}
        //else
        //{
        //    a = "Death";
        //}

        //Debug.Log(b);

        //--------Can be this-------

        //a = b ?? "Death";
        //Debug.Log(a);


        //---------ENUMS--------

        //Debug.Log((int)Colors.Red);


        //---------------Ternary Conditional Operator ?:-----------


        //Debug.Log((power>=10)?"Great":"Scram!!!");
       

    }
    //---------------Ternary Conditional Operator ?:-----------
    IEnumerator Delay()
    {
        while (true)
        {
            Debug.Log(counter);
            counter++;
            counter=(counter > 3) ? 0 : counter;
            yield return new WaitForSeconds(1);
        }
    }
}
