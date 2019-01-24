using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

	public Vector3 right = new Vector3 (-4.0f, 0, -4.0f);
	public Vector3 left = new Vector3 (4.0f, 0, -4.0f);
//	public bool turn;

	void Start(){
		Renderer rend = GameObject.FindGameObjectWithTag ("Turn").GetComponent<Renderer> ();
		rend.material.SetColor ("_Color", Color.white);
	}

	private bool isSpaceClear(Vector3 position)
	{
		Collider[] intersecting = Physics.OverlapSphere(position, 0.01f);
		if (!(intersecting.Length == 0))
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	private bool isBlackThere(Vector3 position){
		GameObject[] blacks;
		blacks = GameObject.FindGameObjectsWithTag ("Black");
		foreach (GameObject coin in blacks) {
			if (coin.transform.position == position) {
				return true;
			} 
		}
		return false;
	}	

	private bool isWhiteThere(Vector3 position){
		GameObject[] whites;
		whites = GameObject.FindGameObjectsWithTag ("White");
		foreach (GameObject coin in whites) {
			if (coin.transform.position == position) {
				return true;
			} 
		}
		return false;
	}

	private GameObject whichWhiteThere(Vector3 position){
		GameObject[] whites;
		whites = GameObject.FindGameObjectsWithTag ("White");
		foreach (GameObject coin in whites) {
			if (coin.transform.position == position) {
				return coin;
			} 
		}
		return null;
	}


	private void highlightWhites(){
		GameObject[] whites;
		Renderer r = GameObject.FindGameObjectWithTag ("Finish").GetComponent<Renderer> ();
		whites = GameObject.FindGameObjectsWithTag ("White");
		int w = 0;
		foreach (GameObject coin in whites) {
			Vector3 p1 = coin.transform.position + right;
			Vector3 p2 = coin.transform.position + left;
			Vector3 p3 = coin.transform.position + right + right;
			Vector3 p4 = coin.transform.position + left + left;
			Renderer rend = coin.GetComponent<Renderer> ();
			if ((isBlackThere (p1) && isSpaceClear (p3)) || (isBlackThere (p2) && isSpaceClear (p4))) {
				foreach (GameObject coina in whites) {
					Renderer renda = coina.GetComponent<Renderer> ();
					renda.material.SetColor("_Color", Color.white);
				}	
				rend.material.SetColor ("_Color", Color.red);
				break;
			} 
			else {
				if (isSpaceClear(p2) || isSpaceClear (p1)) {
					rend.material.SetColor ("_Color", Color.yellow);
				} 
				else {
					rend.material.SetColor ("_Color", Color.white);
				}
			}
		}
		foreach (GameObject coin in whites) {
			Renderer rend = coin.GetComponent<Renderer> ();
			if (rend.material.color != Color.white) {
				w = w + 1;
			}
		}
		if (w == 0) {
			r.material.SetColor("_Color", Color.green);
		}
	}

	private void clearWhites(){
		GameObject[] whites;
		whites = GameObject.FindGameObjectsWithTag ("White");
		foreach (GameObject coin in whites) {
			Renderer rend = coin.GetComponent<Renderer> ();
			rend.material.SetColor ("_Color", Color.white);
		}
	}

	private void blackMove(){
		Renderer rendererer = GameObject.FindGameObjectWithTag ("Finish").GetComponent<Renderer> ();
		Renderer rend = GameObject.FindGameObjectWithTag ("Turn").GetComponent<Renderer> ();
		int r;
		int rand;
		GameObject[] blacks;
		blacks = GameObject.FindGameObjectsWithTag ("Black");
		List<GameObject> btk = new List<GameObject> ();
		List<GameObject> btm = new List<GameObject> ();
		GameObject chosenCoin;
		foreach (GameObject coin in blacks) {
			Vector3 p1 = coin.transform.position - right;
			Vector3 p2 = coin.transform.position - left;
			Vector3 p3 = coin.transform.position - right - right;
			Vector3 p4 = coin.transform.position - left - left;
			if ((isWhiteThere (p1) && isSpaceClear (p3)) || (isWhiteThere (p2) && isSpaceClear (p4))) {
				btk.Add (coin);
			} 
			else {
				if (isSpaceClear(p2) || isSpaceClear (p1)) {
					btm.Add (coin);
				} 
			}
		}
		if (btm.Count == 0 && btk.Count == 0) {
			rendererer.material.SetColor("_Color", Color.green);
		}
		if (btk.Count == 0) {
			r = Random.Range (0, btm.Count);
			chosenCoin = btm [r];
			Vector3 p1 = chosenCoin.transform.position - right;
			Vector3 p2 = chosenCoin.transform.position - left;
			if (isSpaceClear (p1) && isSpaceClear (p2)) {
				rand = Random.Range (0, 2);
				if (rand == 0) {
					chosenCoin.transform.position = p1; 
				}
				if (rand == 1) {
					chosenCoin.transform.position = p2; 
				}
			} else {
				if (isSpaceClear (p1)) {
					chosenCoin.transform.position = p1;
				}
				if (isSpaceClear (p2)) {
					chosenCoin.transform.position = p2;
				}
			}
		} 
		else {
			r = Random.Range (0, btk.Count);
			chosenCoin = btk [r];
			Vector3 p1 = chosenCoin.transform.position - right;
			Vector3 p2 = chosenCoin.transform.position - left;
			Vector3 p3 = chosenCoin.transform.position - right - right;
			Vector3 p4 = chosenCoin.transform.position - left - left;
			if(isWhiteThere(p1) && isSpaceClear(p3)) {
				chosenCoin.transform.position = p3;
				Destroy(whichWhiteThere(p1));
			}
			if (isWhiteThere (p2) && isSpaceClear (p4)) {
				chosenCoin.transform.position = p4;
				Destroy(whichWhiteThere(p2));
			}
			
		}
		rend.material.SetColor ("_Color", Color.white);
	}

	void Update () {
		Renderer rend = GameObject.FindGameObjectWithTag ("Turn").GetComponent<Renderer> ();
		if (rend.material.color == Color.white) {
			highlightWhites ();
		}
		if (rend.material.color == Color.red ) {
			clearWhites ();
			blackMove ();
		}
	}
}
