using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianElliminationSolver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public static double AbsoluteValueOf(double number)
    {    // calculate absolute value return a double since unity Mathf.Abs returns float

        double num = number;
        if (number < 0)
            num = -1 * number;        //there are other ways to do that, maybe more efficient but more complex
        return num;

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Gaussian Ellimination Solver- Source http://www.dailyfreecode.com/Code/basic-gauss-elimination-method-gauss-2949.aspx //
    ///////////////////////////////// I have also implemented my own Absoluted Value method since unity returns in float ////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void GaussianEllimination_Solver(int row, int cols, double[,] Augmented_Matrix){

        const double tiny = 0.00001d;
        for (int i = 0; i < (row - 1); i++){

            if (AbsoluteValueOf(Augmented_Matrix[i, i]) < tiny){

                for (int i2 = i + 1; i2 < row; i2++){

                    if (AbsoluteValueOf(Augmented_Matrix[i2, i2]) > tiny){

                        for (int j = 0; j < cols; j++){

                            double temp = Augmented_Matrix[i, j];
                            Augmented_Matrix[i, j] = Augmented_Matrix[i2, j];
                            Augmented_Matrix[i2, j] = temp;
                        }
                        break;
                    }
                }
            }

            if (AbsoluteValueOf(Augmented_Matrix[i, i]) > tiny){

                for (int i2 = i + 1; i2 < row; i2++){

                    double factor = -Augmented_Matrix[i2, i] / Augmented_Matrix[i, i];
                    for (int j = 0; j < cols; j++){

                        Augmented_Matrix[i2, j] = Augmented_Matrix[i2, j] + factor * Augmented_Matrix[i, j];
                    }
                }
            }
        }

        for (int r = row - 1; r >= 0; r--){

            if (Augmented_Matrix[r, r] != 0)
                Augmented_Matrix[r, cols - 1] /= Augmented_Matrix[r, r];

            for (int r2 = r - 1; r2 >= 0; r2--){

                if (Augmented_Matrix[r, r] != 0){

                    Augmented_Matrix[r2, cols - 1] -= (Augmented_Matrix[r2, r]) * Augmented_Matrix[r, cols - 1];
                }
            }
        }
    }
}
