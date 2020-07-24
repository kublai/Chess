using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    public string selectedPieceCode;
    public int turn=1;
    
    public int markerStatus = 0;
    public float newposX; 
    public float newposZ;

    public float speed = 10f;

    public GameObject activePiece;

    // Start is called before the first frame update
    void Start()
    {
        
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
                PieceConfig pc = activePiece.GetComponent<PieceConfig>();
                pc.pieceCol = newposX; // >
                pc.pieceRow = newposZ; // ^
                activePiece = null;
                markerStatus = 0;

            }
        }
    }
}
