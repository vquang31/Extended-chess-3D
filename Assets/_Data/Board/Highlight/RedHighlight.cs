using NUnit.Framework;
using System;
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
                Vector2Int targetPosition2d = ConvertMethod.Pos3dToPos2d(move) + attackDirection;
                if(SearchingMethod.IsSquareValid(targetPosition2d) == false) continue;
                if (ConvertMethod.Pos2dToPos3d(targetPosition2d) == Position)
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

        if (InputBlocker.IsPointerOverUI()) { return; }
        base.OnMouseDown();
        Click();
    }

    public void Click()
    {

        //checkValidAttack();
        SelectPieceUIManager.Instance.moveButton.SetActive(true);
        SelectPieceUIManager.Instance.attackButton.SetActive(true);
        //
        // code here
        // if hp of TargetPiece <= 10% maxHp 
        // then show kill button
        if(BoardManager.Instance.TargetPiece != null)
        {
            Piece targetPiece = BoardManager.Instance.TargetPiece.GetComponent<Piece>();
            if (targetPiece != null)
            {
                if (targetPiece.Hp <= targetPiece.MaxHp * Const.PERCENTAGE_OF_MAX_HP_TO_KILL 
                    || targetPiece.Hp <=  BoardManager.Instance.selectedPiece.gameObject.GetComponent<Piece>().AttackPoint)
                {
                    SelectPieceUIManager.Instance.killButton.SetActive(true);
                }
                else
                {
                    SelectPieceUIManager.Instance.killButton.SetActive(false);
                }
            }
        }
        else
        {
            SelectPieceUIManager.Instance.killButton.SetActive(false);
        }
        MovePieceToValidAttackPosition();
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
            if (validAttackPositions[index] == BoardManager.Instance.selectedPosition) 
                    SelectPieceUIManager.Instance.moveButton.SetActive(false);
            return;
        }
        piece.FakeMove(validAttackPositions[0]);
        if (validAttackPositions[0] == BoardManager.Instance.selectedPosition)
            SelectPieceUIManager.Instance.moveButton.SetActive(false);
    }
}
