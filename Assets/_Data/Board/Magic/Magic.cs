using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Magic : NewMonoBehaviour
{
    private int _cost;
    private int _maxQuanity;
    private int _type;

    public int Type
    {
        get => _type;
        set => _type = value;
    }
    public int Cost
    {
        get => _cost;
        set => _cost = value;
    }

    public int MaxQuantity
    {
        get => _maxQuanity;
        set => _maxQuanity = value;
    }   

    protected override void Start()
    {
        InitValue();
    }
    protected virtual void InitValue() { }

    public virtual string Description()
    {
        return "";
    }
    public virtual void Cast()
    {
        Player player = TurnManager.Instance.GetCurrentPlayer();
        player.IncreaseMana(-Cost * MagicCastManager.Instance.PositionCasts.Count);
        ApplyEffect();
    }

    protected void ApplyEffect()
    {
        foreach (Vector3Int position in MagicCastManager.Instance.PositionCasts)
        {
            ApplyEffectToSquare(position);
        }
    }

    protected virtual void ApplyEffectToSquare(Vector3Int position)
    {
        EffectManager.Instance.PlayEffect(Type, position);
        return;
    }
}

