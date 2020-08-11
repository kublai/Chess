using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectPiece : MonoBehaviour
{

    private GameObject status;
    private GlobalConfig globalConfig;

    private GameObject marker1;



    // Start is called before the first frame update
    void Start()
    {
        marker1 = GameObject.Find("Marker1");
        status = GameObject.Find("Board");
        globalConfig = status.GetComponent<GlobalConfig>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown(){   
        Debug.Log("Pieza tocada");
        GameObject.Destroy(GameObject.Find("marker"));
        removePosibleMovesMarkers(globalConfig.selectedPieceCode);
        string pieceCode = gameObject.name; 
        //destroy markers
        
        
        if(globalConfig.turn != getTeam(pieceCode)){
            return; //ignore touch if not the correct team
        }
        if (globalConfig.markerStatus == 1){   //marker is on
            if (globalConfig.selectedPieceCode == pieceCode){ 
                globalConfig.markerStatus = 0;
                return;
            }else{ 
                //show marker for the piece
                globalConfig.selectedPieceCode = pieceCode;            
                showMarker(pieceCode);
                //show options to move the piece
                showPosibleMovesMarkers(pieceCode);
                return;
            }
        }else{//marker is off, turn it on
            globalConfig.selectedPieceCode = pieceCode;    
            globalConfig.markerStatus = 1;        
            //show a marker if user clicks over a piece
            showMarker(pieceCode);
            showPosibleMovesMarkers(pieceCode);
        }
    }
    void showPosibleMovesMarkers(string pieceCode){ 
        List<Location> posibleMoves = getPosibleMoves(pieceCode);
        Color markerColor = new Color(241f/255f, 241f/255f, 160f/255f, 1f);
        int i=0;
        foreach(Location pos in posibleMoves){
            createMarkerObject(pos.getRow(),pos.getCol(),"posibleMove_" + i);
            i++;
        }
    }

    void removePosibleMovesMarkers(string pieceCode){
        List<Location> posibleMoves = getPosibleMoves(pieceCode);
        for (int i=0; i<posibleMoves.Count; i++){
            GameObject marker = GameObject.Find("posibleMove_" + i);
            Destroy(marker);
        }
    }
    

    string getTeam(string pieceCode){
        int num = Int32.Parse(pieceCode.Substring(2,1));
        if (num>2){
            return "black";
        }else{
            return "white";
        }
    }

    void showMarker(string pieceCode){  
        int [] pos = piecePosition(pieceCode);
        createMarkerObject(pos[0],pos[1],"marker");
        globalConfig.markerStatus = 1;
    }

    int[] piecePosition(string pieceCode)
    {
        int[] piecePos = {-1,-1}; //this theoretically never should be returned.
        for(int r=0; r<=7;r++){
            for(int c=0; c<=7;c++){
                if(globalConfig.getPiecePosition(r,c) == pieceCode){
                    piecePos[0] = r;
                    piecePos[1] = c;
                    return piecePos;
                }
            }
        }
        return piecePos;
    }

    void createMarkerObject(int row, int col, string markerName){
        row = -(3 + (row - 4) * 6);
        col = 3 + (col - 4) * 6; 
        Color markerColor = new Color(10f/255f, 241f/255f, 30f/255f, 1f);
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        marker.GetComponent<Renderer>().material.color = markerColor;
        marker.transform.localScale = new Vector3(3f,0.01f,3f);
        marker.transform.position = new Vector3(col,0.01f,row); 
        marker.name = markerName;
    }

    List<Location> getPosibleMoves(string pieceCode){
        List<Location> result = new List<Location>();
        if(pieceCode == ""){
            return result;
        }
        string type = pieceCode.Substring(0,1);        // type of piece, P,R,K,B,Q,X
        string turn = globalConfig.turn;               // black or white
        int[] position = piecePosition(pieceCode);     // row,col
        int row = position[0];
        int col = position[1];
        //Debug.Log(type);
        //Debug.Log(row +"-"+col);
        switch(type){
            case "P": //peon
                if(turn == "white"){
                    addPos(row-1,col,ref result);
                    if(row == 6){
                        addPos(4,col, ref result);
                    }
                }else{
                    addPos(row+1,col,ref result);
                    if(row == 1){
                        addPos(3,col, ref result);
                    }
                }
                break;
            case "R": //rock
                getMovesRock(row,col, ref result);
                break;
            case "K": //knight
                getMovesKnight(row,col, ref result);
                break;
            case "B": //bishop
                getMovesBishop(row,col, ref result);
                break;        
            case "Q": //Queen
                getMovesRock(row,col, ref result);
                getMovesBishop(row,col, ref result);
                break;        
            case "X": //King
                break;        
        }
        return result;
    }

    void getMovesKnight(int row, int col, ref List<Location> result){
        addPos(row-2,col-1, ref result);
        addPos(row-2,col+1, ref result);
        addPos(row-1,col+2, ref result);
        addPos(row+1,col+2, ref result);
        addPos(row+2,col+1, ref result);
        addPos(row+2,col-1, ref result);
        addPos(row+1,col-2, ref result);
        addPos(row-1,col-2, ref result);
    }
    void getMovesRock(int row, int col, ref List<Location> result){
        int counter=col;
        bool continueFlag=true;
        //free positions to the right
        while( continueFlag && counter < 7 ){
            counter++;
            continueFlag = addPos(row,counter, ref result);
        }
        //free positions to the left
        continueFlag=true;
        counter=col;
        while( continueFlag && counter > 0 ){
            counter--;
            continueFlag = addPos(row,counter, ref result);
        }
        //free positions up
        continueFlag=true;  
        counter=row;   
        while( continueFlag && counter > 0 ){
            counter--;
            continueFlag = addPos(counter,col, ref result);
        }
        //free positions down
        continueFlag =true;  
        counter=row;   
        while( continueFlag && counter < 7 ){
            counter++;
            continueFlag = addPos(counter,col, ref result);
        }
    }

    void getMovesBishop(int row, int col, ref List<Location> result){
        int counter=0;
        bool continueFlag=true;
        //free positions up - right
        while( continueFlag){
            counter++;
            continueFlag = addPos(row-counter,col+counter, ref result);
        }
        //free positions to down - right
        continueFlag=true;
        counter=0;
        while( continueFlag ){
            counter++;
            continueFlag = addPos(row+counter,col+counter, ref result);
        }
        //free positions down - left
        continueFlag=true;  
        counter=0;   
        while( continueFlag ){
            counter++;
            continueFlag = addPos(row+counter,col-counter, ref result);
        }
        //free positions up - left
        continueFlag =true;  
        counter=0;   
        while( continueFlag  ){
            counter++;
            continueFlag = addPos(row-counter,col-counter, ref result);
        }
    }
    /*
    * Check if it's possible to move a piece to a position 
    * true: if was posible to add
    * false: otherwise
    */
    bool addPos(int row, int col, ref List<Location> result){
        //position is inside the board?
        if ( col<0 || col >7 || row<0 || row>7){
            return false; //do not add to the list of posible locations, out of the board
        }

        if(globalConfig.board[row,col] == "   "){
            Location aux = new Location(row,col);
            result.Add(aux);
            return true;
        }else if(getTeam(globalConfig.board[row,col]) != globalConfig.turn){
            Location aux = new Location(row,col);
            result.Add(aux);
            return false; //mark location and stop adding
        }else{
            return false; //stop adding
        }
    }
}

