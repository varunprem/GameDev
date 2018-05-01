using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void ChooseAlgorithm(){

        SceneManager.LoadScene("Curves");
        //if (number == 1)
        //    PlayerPrefs.SetInt("Decastleju", 1);

        //else if (number == 2)
        //    PlayerPrefs.SetInt("Bernstein", 1);

        //else if (number == 3)
        //    PlayerPrefs.SetInt("Newton", 1);

        //else if (number == 4)
        //    PlayerPrefs.SetInt("SplineInterpolation", 1);

        ////if (number == 5)
            PlayerPrefs.SetInt("SplineInterpolation3D", 1);
    }
}
