using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    public GameObject selectedPiece;


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
        BoardManager.Instance.selectedPiece = null;
    }

}
