using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorScript : MonoBehaviour {

	public GameObject[] availableRooms;
	public List<GameObject> currentRooms;
	private float screenWidthInPoints;

	// Use this for initialization
	void Start () {
		float height = 2.0f * Camera.main.orthographicSize;
		screenWidthInPoints = height * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		GenerateRoomIfRequired ();
	}

	void AddRoom(float farthestRoomEndX)
	{
		// Pick random index of the room type (Prefab) to generate.
		int randomRoomIndex = Random.Range (0, availableRooms.Length);

		// Creates a room object from the array of available rooms using the random index above.
		GameObject room = (GameObject)Instantiate (availableRooms [randomRoomIndex]);

		// Get the size of the floor inside the room, which is equal to the room's width.
		float roomWidth = room.transform.FindChild ("floor").localScale.x;

		// Calculate the position of the center of the new room to add.
		float roomCenter = farthestRoomEndX + roomWidth/2;

		// Sets the position of the new room. The x and y coordinates don't matter.
		room.transform.position = new Vector3 (roomCenter, 0, 0);

		// Add the room to the list of current rooms.
		currentRooms.Add (room);
	}

	void GenerateRoomIfRequired()
	{
		// Creates a new list to store rooms that need to be removed.
		List<GameObject> roomsToRemove = new List<GameObject> ();


		bool addRooms = true;


		float playerX = transform.position.x;


		float removeRoomX = playerX - screenWidthInPoints;


		float addRoomX = playerX + screenWidthInPoints;


		float farthestRoomEndX = 0;


		foreach(var room in currentRooms)
		{
			float roomWidth = room.transform.FindChild ("floor").localScale.x;
			float roomStartX = room.transform.position.x - roomWidth / 2;
			float roomEndX = roomStartX + roomWidth;

			if (roomStartX > addRoomX) 
			{
				addRooms = false;
			}

			if (roomEndX < removeRoomX) 
			{
				roomsToRemove.Add (room);
			}

			farthestRoomEndX = Mathf.Max (farthestRoomEndX, roomEndX);

		}

		foreach (var room in roomsToRemove) 
		{
			currentRooms.Remove (room);
			Destroy (room);
		}

		if (addRooms) 
		{
			AddRoom (farthestRoomEndX);
		}
	}
}
