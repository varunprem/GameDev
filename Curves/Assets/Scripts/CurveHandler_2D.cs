using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurveHandler_2D : MonoBehaviour
{

    public List<Transform> ControlPoints = new List<Transform>();
    LineRenderer myline;
    public GameObject polyline_obj;
    public UIHandler ui;
    LineRenderer polyline;
    public int polynomialDegree = 1;
    BernstienBasis bernstien;
    DeCastleju_Algorithm NLI;
    NewtonForm newton;
    SplineInterpolation spline;
    bool BB = false, Nested = false, NEWTON = false, spline2D = false;
    public Text header;
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
        if (PlayerPrefs.HasKey("SplineInterpolation"))
            spline.Spline_InterpolatePolynomial();

        if (ControlPoints.Count < 2)
            return;

        //The start position of the line
        Vector3 lastPos = ControlPoints[ControlPoints.Count - 1].position;

        //     float resolution = .002f;
        float resolution = .005f;

        //How many loops?
        int loops = Mathf.FloorToInt(1.0f / resolution);

        if(PlayerPrefs.HasKey("NewtonForm") || PlayerPrefs.HasKey("SplineInterpolation"))
            loops *= (ControlPoints.Count - 1);


        myline.positionCount = 0;
        {
            for (int i = 0; i <= loops; i++)
            {
                myline.positionCount++;

                // Which t position are we at?
                float t = i * resolution;

                Vector2 newPos = new Vector2();
                if(BB)
                    newPos = bernstien.BernsteinBasis(ControlPoints, ControlPoints.Count - 1, t);
                else if(Nested)
                    newPos = NLI.NestedLinearInterpolation(ControlPoints, ControlPoints.Count - 1, t);
                else if(NEWTON)
                    newPos = newton.Interpolation_Polynomial(t);
                else if(spline2D){
                    newPos = spline.pointCalculated(t);
                }

                myline.SetPosition(i, new Vector3(newPos.x, newPos.y, 12));
                //}
                // else
                //    myline.SetPosition(i, new Vector3(newPoint.x, newPoint.y, 12));

                 lastPos = newPos;   //   Save this pos so we can draw the next line segment
            }
        }
        if (ControlPoints.Count > 1)
        {
            polyline.positionCount = ControlPoints.Count;
            for (int i = 0; i < ControlPoints.Count; i++)
            {
                polyline.SetPosition(i, new Vector3(ControlPoints[i].position.x, ControlPoints[i].position.y, ControlPoints[i].position.z));
            }
        }
    }


    void CheckWhichAlgorithm(){

        header = GameObject.Find("Heading").GetComponent<Text>();
        if (PlayerPrefs.HasKey("Bernstein")){
            header.text = "BB Form".ToString();
            SetAllBoolFalse();
            bernstien = GameObject.Find("Canvas").GetComponent<BernstienBasis>();
            BB = true;

        }

        else if (PlayerPrefs.HasKey("DeCastleju")){

            header.text = "DeCastleju's Algorithm".ToString();
            SetAllBoolFalse();
            NLI = GameObject.Find("Canvas").GetComponent<DeCastleju_Algorithm>();
            Nested = true;

        }
        else if (PlayerPrefs.HasKey("NewtonForm")){

            header.text = "Newton's Form".ToString();
            SetAllBoolFalse();
            newton = GameObject.Find("Canvas").GetComponent<NewtonForm>();
            NEWTON = true;

        }
        else if (PlayerPrefs.HasKey("SplineInterpolation")){

            header.text = "Spline Interpolation 2D".ToString();
            SetAllBoolFalse();
            spline = GameObject.Find("Canvas").GetComponent<SplineInterpolation>();
            spline2D = true;

        }

    }

    void SetAllBoolFalse(){

        BB = false;
        Nested = false;
        NEWTON = false;
        spline2D = false;

    }
    public void GoBackToMenu(){

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }







}