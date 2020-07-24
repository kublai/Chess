using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : MonoBehaviour
{
    private GameObject status;
    private GlobalConfig globalConfig;

    private GameObject marker1;



    // Start is called before the first frame update
    void Start()
    {
        marker1 = GameObject.Find("Marker1");
        status = GameObject.Find("GameStatus");
        globalConfig = status.GetComponent<GlobalConfig>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown(){       
        if (globalConfig.markerStatus == 1){
            string pieceCode = gameObject.GetComponent<PieceConfig>().pieceCode;
            if (globalConfig.selectedPieceCode == pieceCode){
                globalConfig.markerStatus = 0;
                marker1.transform.position= new Vector3(0,-1,0); //out of screen
                return;
            }
        }
        globalConfig.selectedPieceCode = gameObject.GetComponent<PieceConfig>().pieceCode;
        float row = gameObject.GetComponent<PieceConfig>().pieceRow;
        float col = gameObject.GetComponent<PieceConfig>().pieceCol;
        //show a marker if user clicks over a piece
        showMarkerPosition(row, col);
        
    }

    void showMarkerPosition(float row, float col){  
        globalConfig.markerStatus = 1;
        int posX = 3;
        int posY = 3;
        col = posX + (col - 5) * 6; 
        row = posY + (row - 5) * 6;
        marker1.transform.position = new Vector3(col,0.1f,row); 
    }
}
