using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]

public class DragObject : MonoBehaviour
{

    private Vector3 screenPoint;
    CurveHandler curve;

    void Start(){

        curve = GameObject.Find("Canvas").GetComponent<CurveHandler>();

    }


    void Update()
    {
        
    }



    void OnMouseDown(){

       screenPoint = Camera.main.WorldToScreenPoint(transform.position);
       // offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(2, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag(){

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) ;
     
        
        transform.position = curPosition;
        curve.DrawCurve();
    }

}