using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float laneWidth = 2;
    int lane = 0;
    float verticalPrevious;
    float floorLock;
    float gravity;




    void Start() {

    }

    // Update is called once per frame
    void Update() {

        float leftRight = Input.GetAxisRaw("Horizontal");
        float jump = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Horizontal")) {

            // if pressing left a
            if (leftRight == -1) {
                lane--;
            } else if (leftRight == 1) {// if pressing right d
              
                lane++;
            }
            //print(lane);
            lane = Mathf.Clamp(lane, -1, 1);
        }

        if (Input.GetButtonDown("Vertical")) {
            
        }

        float targetX = lane * laneWidth;

        float x = (targetX - transform.position.x) * .1f;
        transform.position += new Vector3(x, 0, 0);



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
