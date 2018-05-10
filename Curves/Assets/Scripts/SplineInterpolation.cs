using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineInterpolation : MonoBehaviour
{

    double[,] T_Matrix_X = new double[30, 30];
    double[,] T_Matrix_Y = new double[30, 30];

    GaussianElliminationSolver solver;

    CurveHandler_2D _curveHandler;
    List<Transform> ControlPoints = new List<Transform>();

    // Use this for initialization
    void Start(){

        _curveHandler = GameObject.Find("Canvas").GetComponent<CurveHandler_2D>();
        ControlPoints = _curveHandler.ControlPoints;
        solver = GameObject.Find("GaussianSolver").GetComponent<GaussianElliminationSolver>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////Spline Interpolation - Calculate Coefficients using the solver /////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Spline_InterpolatePolynomial(){

        T_Matrix_X = new double[ControlPoints.Count + 2, ControlPoints.Count + 3];
        T_Matrix_Y = new double[ControlPoints.Count + 2, ControlPoints.Count + 3];

        if (ControlPoints.Count < 1)
            return;

        for (int i = 0; i < (ControlPoints.Count + 2); i++){

            for (int j = 0; j < (ControlPoints.Count + 3); j++){

                if (i == 0 && j == 0){

                    T_Matrix_X[i, j] = 1.0;
                    T_Matrix_Y[i, j] = 1.0;
                }
                else{

                    if (j <= 3){

                        T_Matrix_X[i, j] = Mathf.Pow(i, j);
                        T_Matrix_Y[i, j] = Mathf.Pow(i, j);
                    }
                    else{

                        if (i < (j - 3)){

                            T_Matrix_X[i, j] = 0.0;
                            T_Matrix_Y[i, j] = 0.0;
                        }
                        else{
                            T_Matrix_X[i, j] = Mathf.Pow(i - (j - 3), 3);
                            T_Matrix_Y[i, j] = Mathf.Pow(i - (j - 3), 3);
                        }
                    }
                }

            }
            if (i < ControlPoints.Count){

                T_Matrix_X[i, (ControlPoints.Count + 2)] = ControlPoints[i].position.x;
                T_Matrix_Y[i, (ControlPoints.Count + 2)] = ControlPoints[i].position.y;
            }
            else{

                T_Matrix_X[i, (ControlPoints.Count + 2)] = 0.0;
                T_Matrix_Y[i, (ControlPoints.Count + 2)] = 0.0;

                for (int j = 0; j <= (ControlPoints.Count + 1); j++){

                    switch (j){

                        case 0:
                            T_Matrix_X[i, j] = 0.0;
                            T_Matrix_Y[i, j] = 0.0;
                            break;

                        case 1:
                            T_Matrix_X[i, j] = 0.0;
                            T_Matrix_Y[i, j] = 0.0;
                            break;
                        case 2:
                            T_Matrix_X[i, j] = 2.0;
                            T_Matrix_Y[i, j] = 2.0;
                            break;
                        case 3:
                            if (i == ControlPoints.Count){

                                T_Matrix_X[i, j] = 0.0;
                                T_Matrix_Y[i, j] = 0.0;
                            }
                            else{

                                T_Matrix_X[i, j] = 6 * (i - 2);
                                T_Matrix_Y[i, j] = 6 * (i - 2);
                            }
                            break;
                        default:
                            if (i == ControlPoints.Count){

                                T_Matrix_X[i, j] = 0.0;
                                T_Matrix_Y[i, j] = 0.0;
                            }
                            else{

                                T_Matrix_X[i, j] = 6 * ((ControlPoints.Count - 1) - (j - 3));
                                T_Matrix_Y[i, j] = 6 * ((ControlPoints.Count - 1) - (j - 3));
                            }
                            break;
                    }
                }
            }
        }
        solver.GaussianEllimination_Solver((ControlPoints.Count + 2), (ControlPoints.Count + 3), T_Matrix_X);
        solver.GaussianEllimination_Solver((ControlPoints.Count + 2), (ControlPoints.Count + 3), T_Matrix_Y);
    }

    // Calculate X(t) and Y(t) and Z(t)
    public double X_or_Y_or_Z_Of_T(float t, double[,] Result_Matrix){

        double point_Value = 0.0;
        for (int i = 0; i < (ControlPoints.Count + 3); i++){

            if (i == 0)
                point_Value += Result_Matrix[i, ControlPoints.Count + 2];

            else if (i <= 3){

                point_Value += Result_Matrix[i, ControlPoints.Count + 2] * Mathf.Pow(t, i);
            }
            else{

                if (t < (i - 3))
                {
                    //Debug.Log("End of the line between two points");
                }
                else
                    point_Value += Result_Matrix[i, ControlPoints.Count + 2] * Mathf.Pow((t - i + 3), 3.0f);
            }
        }

        return point_Value;
    }

    // Return X(t) and Y(t) in a Vector 2 point
    public Vector3 pointCalculated(float t){

        Vector3 newPos = new Vector3();

        newPos.x = (float)X_or_Y_or_Z_Of_T(t, T_Matrix_X);
        newPos.y = (float)X_or_Y_or_Z_Of_T(t, T_Matrix_Y);

        return newPos;
    }

}
