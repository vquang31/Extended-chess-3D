using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.UIElements;

public class MagicCastManager : Singleton<MagicCastManager>
{

    /// <summary>
    ///  0: Not casting magic
    ///  1: Preparing to cast magic
    ///  2: Casting magic
    /// </summary>
    [SerializeField] private int _isCasting = 0;
    [SerializeField] private Magic selectedMagic;
    [SerializeField] private List<Vector3Int> _positionCast = new();


    public int IsCasting
    {
        get => _isCasting;
        set => _isCasting = value;
    }

    public Magic SelectedMagic
    {
        get => selectedMagic;
    }

    public List<Vector3Int> PositionCasts
    {
        get => _positionCast;
    }


    protected override void ResetValues()
    {
        base.ResetValues();
        selectedMagic = null;
        _positionCast.Clear();
    }

    public void SetSelectedMagic(Magic magic)
    {
        IsCasting = 2;
        selectedMagic = magic;
    }

    public void ResetCasting()
    {
        _isCasting = 1;
        ResetValues();
        BoardManager.Instance.CancelHighlightAndSelectedChess();
    }



    public void PrepareCast(Vector3Int position)
    {
        CastMagicUIManager.Instance.ShowCastButton();
        HighlightManager.Instance.HighlightMagic(position);
        _positionCast.Add(position);

    }
     
    public void RemoveCast(Vector3Int position)
    {

        _positionCast.Remove(position);
        HighlightManager.Instance.ClearHighlightAt(position);
        SquareManager.Instance.DisplayAll();

        //HighlightManager.Instance.ClearHighlights();
        //foreach (var pos in _positionCast)
        //{
        //    HighlightManager.Instance.HighlightMagic(pos);
        //}

    }

    public void CastMagic()
    {
        selectedMagic.Cast();
    }




    public void EndCasting()
    {
        _isCasting = 0;
        ResetValues();
        CastMagicUIManager.Instance.ResetUI();
        BoardManager.Instance.CancelHighlightAndSelectedChess();
    }

}