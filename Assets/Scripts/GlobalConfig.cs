using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    public string selectedPieceCode;
    public string turn = "white";
    public int markerStatus = 0;
    public float newposX; 
    public float newposZ;
    public float speed = 10f;
    public GameObject activePiece;
    public string[,] board; 


    // Start is called before the first frame update
    void Start()
    {
        turn = "white";
        board = new string[,]
                    {
                        {"RA8","KB8","BC8","QD8","XE8","BF8","KG8","RH8"},
                        {"   ","PB7","PC7","PD7","PE7","PF7","PG7","PH7"},
                        {"   ","   ","   ","   ","   ","   ","   ","   "},
                        {"PA7","   ","   ","QD1","   ","BF1","   ","   "},
                        {"   ","   ","   ","   ","   ","   ","   ","   "},
                        {"   ","   ","   ","   ","   ","   ","   ","   "},
                        {"PA2","PB2","PC2","PD2","PE2","PF2","PG2","PH2"},
                        {"RA1","KB1","BC1","   ","XE1","   ","KG1","RH1"},
                    };
    }

    // Update is called once per frame
    void Update()
    {
        float newX = 3 + (newposX - 5) * 6;
        float newZ = 3 + (newposZ - 5) * 6;
        Vector3 newPosition = new Vector3(newX,1,newZ);
        if(activePiece){     
            float step = speed * Time.deltaTime;
            activePiece.transform.position = Vector3.MoveTowards(activePiece.transform.position, newPosition, step);
            if (activePiece.transform.position == newPosition){
                
                activePiece = null;
                markerStatus = 0;
            }
        }
    }

    public void setPiecePosition(int row, int col, string value){
        board[row,col]= value;
    }

    public string getPiecePosition(int row, int col){
        return board[row,col];
    }
}
