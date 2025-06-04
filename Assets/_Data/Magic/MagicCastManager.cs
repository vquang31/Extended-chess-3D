using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class MagicCastManager : Singleton<MagicCastManager>
{

    /// <summary>
    ///  0: Not casting magic
    ///  1: Preparing to cast magic
    ///  2: Casting magic
    /// </summary>
    [SerializeField] private int _isCasting = 0;
    [SerializeField] private int _quantity = 0;
    [SerializeField] private Magic selectedMagic;
    [SerializeField] private List<Vector3Int> _positionCast = new();


    public int IsCasting
    {
        get => _isCasting;
        set => _isCasting = value;
    }

    public int Quantity
    {
        get => _quantity;
        set => _quantity = value;
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
        _quantity = 0;
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
        Quantity += 1;
        _positionCast.Add(position);

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