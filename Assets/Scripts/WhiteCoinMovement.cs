using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCoinMovement : MonoBehaviour {

	public Vector3 right = new Vector3 (-4.0f, 0, -4.0f);
	public Vector3 left = new Vector3 (4.0f, 0, -4.0f);
	Ray ray;
	RaycastHit hit;
	bool onMouse;
	bool isKing;

	void Start () {
		onMouse = false;
		isKing = false;
	}

	void OnMouseOver(){
		onMouse = true;
	}

	void OnMouseExit(){
		onMouse = false;
	}

	bool canMove(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Renderer rend = GetComponent<Renderer> ();
		if (rend.material.color == Color.yellow && onMouse) {
			return true; 
		} 
		else {
			return false;
		}

	}

	bool canKill(){
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Renderer rend = GetComponent<Renderer> ();
		if (rend.material.color == Color.red && onMouse) {
			return true; 
		} 
		else {
			return false;
		}
	}

	private GameObject whichBlackThere(Vector3 position){
		GameObject[] blacks;
		blacks = GameObject.FindGameObjectsWithTag ("Black");
		foreach (GameObject coin in blacks) {
			if (coin.transform.position == position) {
				return coin;
			} 
		}
		return null;
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

	void Update () {
		Vector3 p1 = transform.position + right;
		Vector3 p2 = transform.position + left;
		Vector3 p3 = transform.position + right + right;
		Vector3 p4 = transform.position + left + left;
		Renderer rend = GameObject.FindGameObjectWithTag ("Turn").GetComponent<Renderer> ();
		if (canKill ()) {
			if (Input.GetKeyDown (KeyCode.RightArrow) && isBlackThere (p1)) {
				transform.position = p3;
				Destroy (whichBlackThere (p1));
				rend.material.SetColor ("_Color", Color.red);
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) && isBlackThere (p2)) {
				transform.position = p4;
				Destroy (whichBlackThere (p2));
				rend.material.SetColor ("_Color", Color.red);
			}
		} 
		else {
			if (canMove ()) {
				if (Input.GetKeyDown (KeyCode.RightArrow) && isSpaceClear (p1)) {
					transform.position = p1;
					rend.material.SetColor ("_Color", Color.red);
				}
				if (Input.GetKeyDown (KeyCode.LeftArrow) && isSpaceClear (p2)) {
					transform.position = p2;
					rend.material.SetColor ("_Color", Color.red);
				}
			}
		}
	}
}
