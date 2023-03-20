using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Drag;

public class RoadPiece : MonoBehaviour {

    Boolean rotateable = false;

    // Start is called before the first frame update
    void Start()
    {
        rotateable = true;
    }

    private void Update()
    {
        // Test with to snap to simple rectangle
        //GameObject square = GameObject.Find("Square");
        //snapToSquare(piece, square);
        if (rotateable && Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Rotate(0, 0, 15);

        }
        else if(rotateable && Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Rotate(0, 0, -15);

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            rotateable = false;
        }



    }


    //private void OnMouseDown()
    //{
    //    var clickedObject = gameObject;
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        clickedObject.transform.Rotate(0, 0, -15);

    //    }
    //}



    /**
     * method to snap object to square - test
     */
    //void snapToSquare(RoadPiece current, GameObject square)
    //{
    //    Vector2 sqr_pos = square.transform.position;
    //    print("Inside from right" + (sqr_pos.x + 550 > current.position.x)); 
    //    print("Inside from left" + (sqr_pos.x - 550 < current.position.x)); 
    //    print("Inside from top" + (sqr_pos.y + 550 > current.position.y)); 
    //    print("Inside from bottom" + (sqr_pos.y + 550 < current.position.y)); 
    //    if (sqr_pos.x + 550 > current.position.x && sqr_pos.x - 550 < current.position.x && sqr_pos.y + 550 > current.position.y && sqr_pos.y - 550 < current.position.y)
    //    {
    //        print("in");
    //        positionPiece(current, sqr_pos, 0);
    //    }
    //    current.position = current.transform.position;
    //}



}