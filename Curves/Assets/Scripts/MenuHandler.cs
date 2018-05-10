using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void ChooseAlgorithm(int num){

      
        if (num == 1){

            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Curve2D");
            PlayerPrefs.SetInt("Bernstein", 1);
        }

        else if (num == 2){

            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Curve2D");
            PlayerPrefs.SetInt("DeCastleju", 1);
        }

        else if (num == 3){

            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Curve2D");
            PlayerPrefs.SetInt("NewtonForm", 1);
        }

        else if (num == 4){

            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Curve2D");
            PlayerPrefs.SetInt("SplineInterpolation", 1);
        }

        else if (num == 5){

            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Curves3D");
            PlayerPrefs.SetInt("SplineInterpolation3D", 1);
        }
    }
}
