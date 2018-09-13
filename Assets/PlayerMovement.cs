using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float laneWidth = 2;
    int lane = 0;
    float horizontalClamp = 3.85f;
    Vector3 Velocity;
    float friction = 0.85f;
    float acceleration = 4f;
    float jumImpulse = 15;
    //float verticalPrevious;
    float floorLock = 0;
    float gravity = 40;
    bool grounded = true;

    enum MovmentState {
        run,
        jump,
        glide
    }

    MovmentState playerState;



    void Start() {
        playerState = MovmentState.run;
    }

    // Update is called once per frame
    void Update() {


        switch (playerState) {

            case MovmentState.run:
                DoRun();
                checkForJump();
                break;
            case MovmentState.jump:
                DoJump();
                checkIfGrounded();
                break;
            case MovmentState.glide:
                DoGlide();
                break;
            default:
                print("error: MovmentState out of bounds");
                break;
        }


    }


    void DoRun() {
        float leftRight = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Horizontal")) {
            if (leftRight == -1) {//if pressing a
                Velocity.x += -acceleration; // * Time.deltaTime;
            } else if (leftRight == 1) {// if pressing right d
                Velocity.x += acceleration;// * Time.deltaTime;
            }
        } //else {
        Velocity.x *= friction; //* Time.deltaTime;
        //}

        float x = transform.position.x + Velocity.x * Time.deltaTime;
        transform.position = new Vector3(x, 0, 0);

        if (transform.position.x > horizontalClamp || transform.position.x < -horizontalClamp) {
            print("clamping");
            float clampPosition = Mathf.Clamp(transform.position.x, -horizontalClamp, horizontalClamp);
            transform.position = new Vector3(clampPosition, 0, 0);
            Velocity.x = 0;

        }

        

    }

    void checkForJump() {
        float jump = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Vertical") && jump > 0) {
            playerState = MovmentState.jump;
            Velocity.x = 0;
        }
    }


    void DoJump() {
        //print("Jumping");
       // float jump = Input.GetAxisRaw("Vertical");

        if ( grounded) {
           // print("applying jump");
            grounded = false;
            Velocity.y += jumImpulse;
        }

        Velocity.y -= gravity * Time.deltaTime;
       

        float y = transform.position.y + Velocity.y * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, y, 0);
        //print(transform.position.y);


    }

    void checkIfGrounded() {
        if (transform.position.y <= 0) {
            transform.position = new Vector3(transform.position.x, floorLock, 0);
            Velocity.y = 0;
            grounded = true;
            playerState = MovmentState.run;
        }
    }

    void DoGlide() {

    }


    void OverlappingAABB(AABB other) {
        if (other.tag == "Coin") {
            Coin coin = other.GetComponent<Coin>();
            switch (coin.type) {
                case Coin.CoinType.bigCoin:
                    break;
                case Coin.CoinType.littleCoin:
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
        }

    }
}
