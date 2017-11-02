using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour {
	public GameObject cubePrefab;
	public bool plane;
	public int cubePositionX, cubePositionY;


	void OnMouseDown() {
		GameController.ProcessClick (gameObject);
	}

}
