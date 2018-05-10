using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject source;
    public GameObject wayPoint_Prefab;
    public GameObject _degreeText;
    GameObject beizer;
    public int count = 0;
    public  GameObject line;
    bool create = true;
    void Start()
    {

        _degreeText = GameObject.Find("PointCount");

        beizer = GameObject.Find("Canvas");
    }
    void Draw()
    {
        
    }
	void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            count++;
            if (count >= 21)
            {
                create = false;
                count = 21;
            }
            CreateWayPoints();
        }
         _degreeText.GetComponent<Text>().text = "Point Count :- "+ count.ToString();

    }
    public void CreateWayPoints()
    {
        if (create)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 12.0f;

            var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (PlayerPrefs.HasKey("Bernstein") || PlayerPrefs.HasKey("DeCastleju") ||
             PlayerPrefs.HasKey("NewtonForm") || PlayerPrefs.HasKey("SplineInterpolation"))
            {

                beizer.gameObject.GetComponent<CurveHandler_2D>().ControlPoints.Add(Instantiate(wayPoint_Prefab, objectPos, Quaternion.identity).transform);
                beizer.gameObject.GetComponent<CurveHandler_2D>().DrawCurve();

            }
            if (PlayerPrefs.HasKey("SplineInterpolation3D")){

                beizer.gameObject.GetComponent<CurveHandler>().ControlPoints.Add(Instantiate(wayPoint_Prefab, objectPos, Quaternion.identity).transform);
                beizer.gameObject.GetComponent<CurveHandler>().DrawCurve();

            }
         //   beizer.Interpolate_Splines();
        }      
    }



    //public void SliderEvent()
    //{
    //    ///Debug.Log(SliderObj.value);
    //   Vector2 obj =  beizer.NestedLinearInterpolation(beizer.ControlPoints, beizer.ControlPoints.Count - 1, .5f);
    //    source.transform.position = new Vector3(obj.x, obj.y, 12);
    //    //source.transform.position.z = 12;
    //}
}
