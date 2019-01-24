using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeclareWinner : MonoBehaviour {


	void Update () {
		Renderer r = GameObject.FindGameObjectWithTag ("Finish").GetComponent<Renderer> ();
		GameObject[] blacks;
		blacks = GameObject.FindGameObjectsWithTag ("Black");
		GameObject[] whites;
		whites = GameObject.FindGameObjectsWithTag ("White");
		if (blacks.Length == 0) {
			SceneManager.LoadScene (2);
		}
		if (whites.Length == 0) {
			SceneManager.LoadScene (3);
		}
		if (r.material.color == Color.green) {
			if (whites.Length > blacks.Length) {
				SceneManager.LoadScene (2);
			} 
			else {
				SceneManager.LoadScene (3);
			}
		}
	}
}
