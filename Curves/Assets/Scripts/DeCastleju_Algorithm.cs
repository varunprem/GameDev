using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeCastleju_Algorithm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    //The De Casteljau's Algorithm
    public Vector2 NestedLinearInterpolation(List<Transform> PolyNomial, int degree, float t){

        Vector2 InterPolatedValue = new Vector2(0.0f, 0.0f);
        List<Vector2> PreviousTemp = new List<Vector2>();

        for (int i = 0; i <= degree; ++i){

            PreviousTemp.Add(PolyNomial[i].position);
        }

        for (int i = degree; i > 0; --i){

            List<Vector2> Temp = new List<Vector2>();
            for (int j = 0; j < i; ++j){

                InterPolatedValue = PreviousTemp[j] * (1 - t) + PreviousTemp[(j + 1)] * t;
                Temp.Add(InterPolatedValue);
            }

            PreviousTemp.Clear();
            for (int k = 0; k < i; ++k){

                PreviousTemp.Add(Temp[k]);
            }
        }
        return PreviousTemp[0];
    }
}
