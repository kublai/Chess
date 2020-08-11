
public class Location{
    private int _row;
    private int _col;

    public Location(int row, int col){
        _row = row;
        _col = col;
    }
    public int getRow(){
        return _row;
    }

    public void setRow(int row){
        _row = row;
    }

     public int getCol(){
        return _col;
    }

    public void setCol(int col){
        _col = col;
    }

    public bool isInsideBoard(){
        if ( _col>=0 && _col <8 && _row>=0 && _col<8){
            return true;
        }else{
            return false;
        }
    }

}