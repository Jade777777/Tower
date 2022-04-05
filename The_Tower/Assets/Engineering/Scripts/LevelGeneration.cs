using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject startingRoom, endRoom;
    public float moveAmount, startTimeBtwRoom;

    private float timeBtwRoom;
    private int direction, currentFloor, floorLength, currentLength, lastHeight;
    //private bool stopGen = false;

    void Start()
    {
        floorLength = Random.Range(5,10);
        currentLength = 1;

        currentFloor = PersistantGameManager.Instance.currentLevel;
        if (currentFloor % 2 == 1) {
            direction = 1;
        }
        else {
            direction = -1;
        }

        Instantiate(startingRoom, transform.position, Quaternion.identity);
    }

    private void Update() {
        if (timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move() {

        currentLength += 1;

        //move left
        if (direction == 1) {
            Vector3 newPos = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
        //move right
        if (direction == -1) {
            Vector3 newPos = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
            transform.position = newPos;
        }

        if (currentLength < floorLength ) {
            int randomHeight = Random.Range(0, 3);
            int randomRoom = randomHeight + (lastHeight * 3);
            Instantiate(rooms[randomRoom], transform.position, Quaternion.identity);
            lastHeight = randomHeight;
        }
        else if (currentLength == floorLength) {
            int randomRoom = 1 + (lastHeight * 3);
            Instantiate(rooms[randomRoom], transform.position, Quaternion.identity);
        }
        else {
            Instantiate(endRoom, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
