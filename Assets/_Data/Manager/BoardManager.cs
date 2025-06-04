using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.UIElements;

public class BoardManager : Singleton<BoardManager>
{
    public GameObject selectedPiece;
    public Vector3Int selectedPosition;

    private GameObject targetPiece;
    private Vector3Int targetPosition;


    /// <summary>
    ///  this class is used to manage the board 
    ///  contains: square 
    ///            hightlight
    ///            piece 
    ///  
    /// </summary>
    protected GameObject BoardGameObject;

    protected HighlightManager _highlightManager;
  
    protected SquareManager _squareManager;

    //protected PieceManager _pieceManager;

  public GameObject TargetPiece
    {
        get => targetPiece;
        set => targetPiece = value;
    }

    public Vector3Int TargetPosition
    {
        get => targetPosition;
        set => targetPosition = value;
    }

    public void UpdateTargetPieceAndPosition(Vector3Int position)
    {
        TargetPiece = SearchingMethod.FindPieceByPosition(position)?.gameObject;
        TargetPosition = position;
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


    /// <summary>
    /// 
    /// </summary>
    public void ReturnSelectedPosition()
    {
        if(selectedPiece == null) return;
        selectedPiece?.GetComponent<Piece>().FakeMove(selectedPosition);
    }



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
