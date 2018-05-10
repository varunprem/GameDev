using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]

public class DragObject : MonoBehaviour
{

    private Vector3 screenPoint;
    CurveHandler curve;
    CurveHandler_2D _curve;

    void Start(){
        if (PlayerPrefs.HasKey("Bernstein") || PlayerPrefs.HasKey("DeCastleju") ||
            PlayerPrefs.HasKey("NewtonForm") || PlayerPrefs.HasKey("SplineInterpolation")){

            _curve = GameObject.Find("Canvas").GetComponent<CurveHandler_2D>();

        }

        if (PlayerPrefs.HasKey("SplineInterpolation3D")){

            curve = GameObject.Find("Canvas").GetComponent<CurveHandler>();
        }

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
        if(PlayerPrefs.HasKey("SplineInterpolation3D"))
            curve.DrawCurve();

        if (PlayerPrefs.HasKey("Bernstein") || PlayerPrefs.HasKey("DeCastleju") ||
            PlayerPrefs.HasKey("NewtonForm") || PlayerPrefs.HasKey("SplineInterpolation"))
                _curve.DrawCurve();

    }

}