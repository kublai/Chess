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

    private ArrayList listPieces = new ArrayList(); 


    void Start()
    {
        listPieces.Add("BC1");
        listPieces.Add("RE1");
        listPieces.Add("KG1");
        listPieces.Add("PA2");
        listPieces.Add("PA2");
        listPieces.Add("QD1");
        listPieces.Add("RH1");
        listPieces.Add("RH8");
        listPieces.Add("QD8");
        listPieces.Add("PH7");
        listPieces.Add("KB8");
        listPieces.Add("RE8");
        listPieces.Add("BC8");
        listPieces.Add("BF8");
        listPieces.Add("KG8");
        listPieces.Add("PH7");
        listPieces.Add("PF7");
        listPieces.Add("PE7");
        listPieces.Add("PD7");
        listPieces.Add("PC7");
        listPieces.Add("PB7");
        listPieces.Add("PA7");
        listPieces.Add("BF1");
        listPieces.Add("KB1");
        listPieces.Add("RA1");
        listPieces.Add("PD2");
        listPieces.Add("PB2");
        listPieces.Add("PC2");
        listPieces.Add("PE2");
        listPieces.Add("PF2");
        listPieces.Add("PG2");
        listPieces.Add("PH2");
        listPieces.Add("RA8");
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
            Debug.Log("ray detected");
            
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

    void takePiece()
    {
        
    }

    bool sameTeamPiece(){
        return true;
    }
    
    //move the piece to the new position
    void movePiece(int x, int z){
            //move the piece (If we have a piece selected)
            globalConfig.newposX = x;
            globalConfig.newposZ = z;
            if (globalConfig.selectedPieceCode != ""){
               // PieceConfig pc = GameObject.Find(globalConfig.selectedPieceCode).GetComponent<PieceConfig>();
               // if (globalConfig.newposX != pc.pieceCol || globalConfig.newposZ != pc.pieceRow) { 
               //     globalConfig.activePiece = GameObject.Find(globalConfig.selectedPieceCode);
               // }
                
            }
    }
    //Verify if the new position in Occupied
    bool newPositionOccupied(){
        GameObject piece;
        foreach( string item in listPieces){
            piece = GameObject.Find(item);
            //int r = (int)piece.GetComponent<PieceConfig>().pieceRow; //row of the piece
            //int c = (int)piece.GetComponent<PieceConfig>().pieceCol; //column of the piece
            //if (r == flatz && c ==flatx){
            //    return true;
            //}
        }
        return false;
    }
}
