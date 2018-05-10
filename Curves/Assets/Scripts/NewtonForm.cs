using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtonForm : MonoBehaviour {

    CurveHandler_2D _curveHandler;
    List<Transform> ControlPoints = new List<Transform>();
	// Use this for initialization
	void Start () {
        _curveHandler = GameObject.Find("Canvas").GetComponent<CurveHandler_2D>();
        ControlPoints = _curveHandler.ControlPoints;
	}

    //Polygon Interpolation-Newton's Form

    public Vector2 Interpolation_Polynomial(float t){

        Vector2 _point_Position = new Vector2(0.0f, 0.0f);
        if (ControlPoints.Count < 2)
            return _point_Position;

        float[,] _X_PolyTable = new float[ControlPoints.Count, (ControlPoints.Count + 2)];
        float[,] _Y_PolyTable = new float[ControlPoints.Count, (ControlPoints.Count + 2)];

        for (int i = 0; i < ControlPoints.Count; i++){

            _X_PolyTable[i, 0] = i;
            _Y_PolyTable[i, 0] = i;
            _X_PolyTable[i, 1] = ControlPoints[i].position.x;
            _Y_PolyTable[i, 1] = ControlPoints[i].position.y;
        }

        // Divided Difference Table 
        for (int j = 2; j < (ControlPoints.Count + 2); j++){

            for (int i = 0; i < (ControlPoints.Count - j + 1); i++){

                _X_PolyTable[i, j] = (_X_PolyTable[(i + 1), (j - 1)] - _X_PolyTable[i, (j - 1)]) / (_X_PolyTable[(i + j - 1), 0] - _X_PolyTable[i, 0]);
                _Y_PolyTable[i, j] = (_Y_PolyTable[(i + 1), (j - 1)] - _Y_PolyTable[i, (j - 1)]) / (_Y_PolyTable[(i + j - 1), 0] - _Y_PolyTable[i, 0]);
            }
        }

        // Newton Form
        for (int i = 0; i < ControlPoints.Count; i++){

            float _temp_X, _temp_Y, _products = 1.0f;
            _temp_X = _X_PolyTable[0, (i + 1)];
            _temp_Y = _Y_PolyTable[0, (i + 1)];

            for (int j = 0; j < i; j++){

                _products = _products * (t - _X_PolyTable[j, 0]);   // t-t0
            }
            _point_Position.x += _products * _temp_X;
            _point_Position.y += _products * _temp_Y;
        }
        return _point_Position;      // Final Point stored in drawcurve function from Interpolate Polynomial
    }
}
