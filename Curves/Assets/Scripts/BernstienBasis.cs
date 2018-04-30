using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BernstienBasis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Bernsetin Basis
    public Vector2 BernsteinBasis(List<Transform> BernstienPolyNomial, int degree, float t){

        int[,] PascalTringle = new int[25, 25];
        int val = 0;
        Vector2 binomialCoefficient = new Vector2(0.0f, 0.0f);

        for (int i = 0; i <= (degree + 1); ++i){

            val = 1;

            for (int j = 0; j <= i; ++j){

                PascalTringle[i, j] = val;
                // Debug.Log("->" + val);
                val = val * (i - j) / (j + 1);
            }
        }

        for (int i = 0; i <= degree; ++i){

            binomialCoefficient.x += (BernstienPolyNomial[i].position.x * PascalTringle[degree, i]
                 * Mathf.Pow((1 - t), (degree - i)) * Mathf.Pow(t, i));
            binomialCoefficient.y += (BernstienPolyNomial[i].position.y * PascalTringle[degree, i]
               * Mathf.Pow((1 - t), (degree - i)) * Mathf.Pow(t, i));
        }
        return binomialCoefficient;
    }
}
