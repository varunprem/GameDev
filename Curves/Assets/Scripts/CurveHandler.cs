using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CurveHandler : MonoBehaviour
{
 
    public List<Transform> ControlPoints = new List<Transform>();
    LineRenderer myline;
    public GameObject polyline_obj;
    public UIHandler ui;
    LineRenderer polyline;
    public int polynomialDegree = 1;
    SplineInterpolation3D spline;
    DeCastleju_Algorithm NLI;
    BernstienBasis bernstien;
    NewtonForm newton;
    SplineInterpolation splineInterpolation;

    int checkAlgo;
   void Start()
    {
        CheckWhichAlgorithm();
        myline = ui.line.GetComponent<LineRenderer>();
        polyline = polyline_obj.GetComponent<LineRenderer>();
    }

    void Update()
    {

      //  DrawCurve();
    }
   

    public void DrawCurve()
    {
        //if(checkAlgo == 5)
            spline.Spline_InterpolatePolynomial();

        if (ControlPoints.Count < 2)
            return;

        //The start position of the line
        Vector3 lastPos = ControlPoints[ControlPoints.Count-1].position;

        //     float resolution = .002f;
        float resolution = .005f ;

        //How many loops?
        int loops = Mathf.FloorToInt(1.0f / resolution);
        loops *= (ControlPoints.Count-1);

        // Debug.Log(ControlPoints.Count);

        myline.positionCount = 0;
         //myline.positionCount = (int)loops + 1;
        //List<Vector2> Temp = new List<Vector2>();
       // while (degree++ < ControlPoints.Count)
        {
            for (int i = 0; i <= loops; i++)
            {
                myline.positionCount++;
                // Which t position are we at?
                float t = i * resolution;
                //     print(t);
                // print("loops" + loops);
               // Vector2 newPoint;
                Vector3 newPos;
                /*if(checkAlgo == 1)
                    newPoint = NLI.NestedLinearInterpolation(ControlPoints, ControlPoints.Count - 1, t);
                else if(checkAlgo == 2)
                    newPoint = bernstien.BernsteinBasis(ControlPoints, ControlPoints.Count - 1, t);
                else if(checkAlgo == 3)
                    newPoint = newton.Interpolation_Polynomial(t);
                else if(checkAlgo == 5)
                {*/
                    newPos = spline.pointCalculated(t);
 
                    myline.SetPosition(i, new Vector3(newPos.x, newPos.y, newPos.z));
                //}
               // else
                //    myline.SetPosition(i, new Vector3(newPoint.x, newPoint.y, 12));

                // lastPos = newPos;   //   Save this pos so we can draw the next line segment
            }
        }
        if (ControlPoints.Count>1)
        {
            polyline.positionCount = ControlPoints.Count;
            for (int i = 0; i < ControlPoints.Count; i++)
            {
                polyline.SetPosition(i, new Vector3(ControlPoints[i].position.x, ControlPoints[i].position.y, ControlPoints[i].position.z));
            }
        }
    }


    void CheckWhichAlgorithm(){

       /* if (PlayerPrefs.HasKey("Decastleju")){

            checkAlgo = 1;
            NLI = GameObject.Find("Canvas").GetComponent<DeCastleju_Algorithm>();
        }
        else if (PlayerPrefs.HasKey("Bernstein")){

            checkAlgo = 2;
            bernstien = GameObject.Find("Canvas").GetComponent<BernstienBasis>();
        }
        else if (PlayerPrefs.HasKey("Newton")){

            checkAlgo = 3;
            newton = GameObject.Find("Canvas").GetComponent<NewtonForm>();
        }
        else if (PlayerPrefs.HasKey("SplineInterpolation")){

            checkAlgo = 4;
            splineInterpolation = GameObject.Find("Canvas").GetComponent<SplineInterpolation>();
        }*/
        //else if (PlayerPrefs.HasKey("SplineInterpolation3D")){
      
           // checkAlgo = 5;
            spline = GameObject.Find("Canvas").GetComponent<SplineInterpolation3D>();
        //}
    }
   
   
   

   

    

}