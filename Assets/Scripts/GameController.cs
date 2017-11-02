using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject cubePrefab;
	new Vector3 cubePosition;
	GameObject currentCube;
	public Text CargoPointsText;
	public static GameObject activePlane;
	public static GameObject cloud;
	public static int airplaneX, airplaneY;
	int gridWidth;
	int gridHeight;
	static GameObject[ , ] grid;

	// variables to keep track of plane turns and cargo
	int numOfTurns;
	int cargoTons;
	int maxCargo = 90;
	int cargoPoints;
	float turnTime;
	public static int deliveryDepotX, deliveryDepotY;



	// Use this for initialization
	void Start () {
		gridWidth = 16;
		gridHeight = 9;
		grid = new GameObject[gridWidth, gridHeight];
		numOfTurns = 1;
		cargoTons = 0; // plane starts off with 0 ton of cargo
		turnTime = 1.5f; // plane turns every 1.5 seconds;
		deliveryDepotX = 15;
		deliveryDepotY = 0;

		// starting location of airplane
		airplaneX = 0;
		airplaneY = 8;

		for (int y = 0; y < gridHeight; y++) {
			for (int x = 0; x < gridWidth; x++) {
				cubePosition = new Vector3 (x * 2 - 16, y * 2 - 10, 0);
				currentCube = Instantiate (cubePrefab, cubePosition, Quaternion.identity);
				currentCube.GetComponent<cubeController>().cubePositionX = x;
				currentCube.GetComponent<cubeController>().cubePositionY = y;
				grid [x, y] = currentCube;

				if (x == airplaneX && y == airplaneY) {
					currentCube.GetComponent<Renderer> ().material.color = Color.red;
					currentCube.GetComponent<cubeController> ().plane = true;
					activePlane = currentCube;
				}

				if (x == deliveryDepotX && y == deliveryDepotY) {
					currentCube.GetComponent<Renderer> ().material.color = Color.black;
				}



			}

		}

	}

	public static void ProcessClick (GameObject currentCube) {

		// deactivate active plane
		if (currentCube == activePlane && currentCube.GetComponent<cubeController>().plane){
			currentCube.GetComponent<Renderer> ().material.color = Color.blue;
			activePlane = null;
		}
		// activate deactivated plane
		else if (currentCube != activePlane && currentCube.GetComponent<cubeController>().plane) {
			currentCube.GetComponent<Renderer> ().material.color = Color.red;
			activePlane = currentCube;
		} 

		// else if we click on a cloud, if a plane is active, the cloud becomes activePlane, and activePlane becomes cloud
		else if (currentCube.GetComponent<cubeController> ().plane == false) {

			if (activePlane != null) {
				activePlane.GetComponent<Renderer> ().material.color = Color.white;
				activePlane.GetComponent<cubeController> ().plane = false;
				activePlane = null;


				currentCube.GetComponent<Renderer> ().material.color = Color.red;
				currentCube.GetComponent<cubeController> ().plane = true;
				activePlane = currentCube;


			} 


		}
		if (grid [deliveryDepotX, deliveryDepotY].GetComponent<cubeController> ().plane == false) {
			grid [deliveryDepotX, deliveryDepotY].GetComponent<Renderer> ().material.color = Color.black;
		}


	}

	// Update is called once per frame
	void Update () {
		
		if (Time.time > turnTime * numOfTurns)  {
			numOfTurns++; // As time in the game increases by 1.5 seconds and the plane continues to make turns,

			// check if cube is a plane
			if (grid[airplaneX, airplaneY].GetComponent<cubeController>().plane == true && activePlane){
				// increase cargo in the plane by 10 tons
				cargoTons += 10;

				// if cargoTons is more than maxCargo, then set value of cargoTons equal to value of maxCargo
				if (cargoTons > maxCargo) {
					cargoTons = maxCargo;

				}
			
			}

			// checks if plane is at deliveryDepot
			if (grid [deliveryDepotX, deliveryDepotY].GetComponent<cubeController> ().plane == true) {
				// scores +1 cargo
				cargoPoints += cargoTons;
				// removes all cargo into deliveryDepot
				cargoTons = 0;
			}


			}
			CargoPointsText.text = "Tons of Cargo: " + cargoTons + " Cargo Points: " + cargoPoints;
		}
}
