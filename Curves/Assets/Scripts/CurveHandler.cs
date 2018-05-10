using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurveHandler : MonoBehaviour
{
 
    public List<Transform> ControlPoints = new List<Transform>();
    LineRenderer myline;
    public GameObject polyline_obj;
    public UIHandler ui;
    LineRenderer polyline;
    public int polynomialDegree = 1;
    SplineInterpolation3D spline;

   void Start()
    {
        CheckWhichAlgorithm();
        myline = ui.line.GetComponent<LineRenderer>();
        polyline = polyline_obj.GetComponent<LineRenderer>();
    }

    void Update()
    {

    }
   

    public void DrawCurve()
    {
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


        myline.positionCount = 0;
      
   
            for (int i = 0; i <= loops; i++){
                myline.positionCount++;

                    // Which t position are we at?
                    float t = i * resolution;
               
                    Vector3 newPos;
            
                    newPos = spline.pointCalculated(t);
 
                    myline.SetPosition(i, new Vector3(newPos.x, newPos.y, newPos.z));
               
            }
            if (ControlPoints.Count>1){

                polyline.positionCount = ControlPoints.Count;
                for (int i = 0; i < ControlPoints.Count; i++){

                polyline.SetPosition(i, new Vector3(ControlPoints[i].position.x, ControlPoints[i].position.y, ControlPoints[i].position.z));

                }
            }
    }


    void CheckWhichAlgorithm(){

            spline = GameObject.Find("Canvas").GetComponent<SplineInterpolation3D>();

    }

    public void GoBackToMenu(){

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }







}