using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RedHighlight: Highlight
{
    List<Vector3Int> validAttackPositions = new List<Vector3Int>();

    protected override void Start()
    {
        base.Start();

        if (gameObject.name.Contains("Prefab")) return;
        Piece piece = BoardManager.Instance?.selectedPiece?.gameObject?.GetComponent<Piece>();
        List<Vector2Int> attackDirections = piece.GetAttackDirection();
        List<Vector3Int> moves = piece.GetValidMoves(); 
        moves.Add(piece.Position);
        foreach (var move in moves)
        {
            foreach(var attackDirection in attackDirections)
            {
                Vector2Int targetPosition2d = Method2.Pos3dToPos2d(move) + attackDirection;
                if(Method2.Pos2dToPos3d(targetPosition2d) == Position)
                {
                    if (piece.CheckValidAttack(move, targetPosition2d))
                    {
                        validAttackPositions.Add(move);
                    }
                }

            }
        }
    }


    protected override void OnMouseDown()
    {
        if (!canClick) return;
        base.OnMouseDown();
        Click();
    }



    public void Click()
    {
        MovePieceToValidAttackPosition();
        //checkValidAttack();
        SelectPieceUIManager.Instance.attackButton.SetActive(true);
        SelectPieceUIManager.Instance.moveButton.SetActive(true);
        SelectPieceUIManager.Instance.killButton.SetActive(true);
    }


    public void MovePieceToValidAttackPosition()
    {
        Piece piece = BoardManager.Instance.selectedPiece.gameObject.GetComponent<Piece>();
        if (validAttackPositions.Contains(piece.Position))
        {
            int index = validAttackPositions.IndexOf(piece.Position);
            index++;
            if (index == validAttackPositions.Count)
            {
                index = 0;
            }

            piece.FakeMove(validAttackPositions[index]);
            return;
        }
        piece.FakeMove(validAttackPositions[0]);
    }
}
