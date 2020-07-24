using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Ray1 : MonoBehaviour
{
    Camera cam;
    Transform trf;
    Vector3 pos = new Vector3(500, 0, 0);
    Vector3 direction = new Vector3(0.0f, 0.0f,0.0f );
    Vector3 origin = new Vector3(0.0f, 0.0f,0.0f );

    private float  k, flatx, flatz;
    private int xsq, ysq;
    private GlobalConfig globalConfig;
    private GameObject statusObj;

    private ArrayList listPieces={"BC1","RE1","KG1","PA2","QD1","RH1","RH8","QD8",
                                  "PH7","KB8","RE8","BC8","BF8","KG8","PH7","PF7",
                                  "PE7","PD7","PC7","PB7","PA7","BF1","KB1","RA1",
                                  "PD2","PB2","PC2","PE2","PF2","PG2","PH2","RA8"};


    void Start()
    {
        cam = GetComponent<Camera>();
        trf = GetComponent<Transform>();
        statusObj = GameObject.Find("GameStatus");
        globalConfig = statusObj.GetComponent<GlobalConfig>();


    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)){
            direction = ray.direction;
            origin = ray.origin;
            //check if a piece is touched 

            //No piece touched, then check in which square the board was touched
            k = (((-1) * trf.position.y) / ray.direction.y) ;
            flatx = (trf.position.x + k * ray.direction.x)/6;
            flatz = (trf.position.z + k * ray.direction.z)/6;
            if (flatx>0){
                flatx = (int)Math.Truncate(flatx)+5;
            }else{
                flatx = (int)Math.Truncate(flatx)+4;
            }
            if (flatz>0){
                flatz = (int)Math.Truncate(flatz)+5;
            }else{
                flatz = (int)Math.Truncate(flatz)+4;
            }
            //check if the position is occupied
            if (newPositionOccupied()){
                if(sameTeamPiece()){ //if the piece is of the same team that is moving => beep
                    beep();
                    return;
                }else{ //if the piece is an enemy piece => take the piece
                    takePiece();
                }
            }
            movePiece((int)flatx, (int)flatz);
        }
        Debug.DrawRay(origin,  direction * 100, Color.yellow);
    }

    // Beep movement no allowed
    void beep(){
        Debug.Log("Beep");
    }

    void takePiece(){
        
    }
    
    //move the piece to the new position
    void movePiece(int x, int z){
            //move the piece (If we have a piece selected)
            globalConfig.newposX = x;
            globalConfig.newposZ = z;
            if (globalConfig.selectedPieceCode != ""){
                PieceConfig pc = GameObject.Find(globalConfig.selectedPieceCode).GetComponent<PieceConfig>();
                if (globalConfig.newposX != pc.pieceCol || globalConfig.newposZ != pc.pieceRow) { 
                    globalConfig.activePiece = GameObject.Find(globalConfig.selectedPieceCode);
                }
                
            }
    }
    //Verify if the new position in Occupied
    bool newPositionOccupied(){
        foreach( var item in listPieces){
                GameObject p = GameObject.Find(item);
                int r = (int)item.GetComponent<PieceConfig>().pieceRow; //row of the piece
                int c = (int)item.GetComponent<PieceConfig>().pieceCol; //column of the piece
                if (r == flatz && c ==flatx){
                    return false;
                }
            }
            return true;
    }
}
