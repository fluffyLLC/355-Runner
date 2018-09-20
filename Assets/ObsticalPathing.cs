using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalPathing : MonoBehaviour {

    public static int[,] ObsticleStates = new int[54, 8];
    static int[,] PrevObsticleStates = new int[54, 8];
    //static int[] oneAndTwo = new int[] { 1, 2 };


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public static void AddRow(int[] row) {
        Shift();

        for (int j = 0; j <= 7; j++) {
            ObsticleStates.SetValue(row[j], 0, j);
        }

        PrevObsticleStates = ObsticleStates;
    }

    static void Shift() {
        for (int i = 53; i > 0; i--) {
            for (int a = 0; a <= 7; a++) {
                ObsticleStates.SetValue(PrevObsticleStates[i - 1, a], i, a);
            }
        }
    }


    public static int GetIndex(int[] row) {
        for (int i = 0; i < 54; i++) {
            for (int a = 0; a <= 7; a++) {
                if (ObsticleStates[i, a] != row[a]) {
                    break;
                }
                if (a == 7) {
                    return i;
                }
            }
        }
        Debug.Log("Error: No playfeild index found, default to 0");
        return 0;
    }

    static int[] FirstRow() {
        int[] firstRow = new int[8];
        for (int i = 0; i < 8; i++) {
            firstRow[i] = ObsticleStates[0, i];
        }
        return firstRow;
    }

    public static int[] CheckObsticles() {



        return FirstRow();
    }
    /*
    static ConditionDeadLane() {
        int range 

    }
    */

    static bool CheckColom(int colom, int[] acceptedValues, int numRows = 54) {
        int numAcceptedV = acceptedValues.Length;
        for (int i = 1; i <= numRows; i++) {
            if (ObsticleStates[i, colom] == acceptedValues[1]) {
                return true;
            }
            if (numAcceptedV == 2) {
                if (ObsticleStates[i, colom] == acceptedValues[2]) {
                    return true;
                }
            }
        }
        return false;
    }
}
