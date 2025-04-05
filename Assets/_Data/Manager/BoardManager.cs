using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : Singleton<BoardManager>
{
    public GameObject selectedPiece;
    public Vector3Int selectedPosition;

    public GameObject targetPiece;

    public GameObject TargetPiece
    {
        get => targetPiece;
        set => targetPiece = value;
    }

    public void SelectedPiece(GameObject piece)
    {
        selectedPiece = piece;
        selectedPosition = piece.GetComponent<Piece>().Position;
    }

    public void CancelSelectedPiece()
    {
        selectedPiece = null;
        selectedPosition = Vector3Int.zero;
    }

    public void ReturnSelectedPosition()
    {
        selectedPiece.GetComponent<Piece>().FakeMove(selectedPosition);
    }


    /// <summary>
    ///  this class is used to manage the board 
    ///  contains: square 
    ///            hightlight
    ///            piece 
    ///  
    /// </summary>
    protected GameObject BoardGameObject;

    protected HighlightManager _highlightManager;

    //protected PieceManager _pieceManager;

    protected SquareManager _squareManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        BoardGameObject = GameObject.Find("Board");
        _highlightManager = gameObject.GetComponent<HighlightManager>();
        _squareManager = gameObject.GetComponent<SquareManager>();  
    }

    public void CancelHighlightAndSelectedChess()
    {
        _highlightManager.ClearHighlights();
        _squareManager.DisplayAll();
        BoardManager.Instance.CancelSelectedPiece();
        // UI
        SelectPieceUIManager.Instance.HideUI();
        // Camera
        CameraManager.Instance.CancelTarget();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="side"> side of enemy</param>
    /// <returns></returns>
    public bool IsSquareOccupiedByOpponent(Vector2Int position, int side) {
        if(SearchingMethod.IsSquareValid(position) == false) return false;
        if(SearchingMethod.IsSquareEmpty(position)) return false;
        return SearchingMethod.FindPieceByPosition(position).Side == side;
    }


}
