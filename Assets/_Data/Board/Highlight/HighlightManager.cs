using System;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : Singleton<HighlightManager> 
{
    protected GameObject redHighlightPrefab;
    protected GameObject greenHighlightPrefab;
    protected GameObject blueHighlightPrefab;
    protected GameObject purpleHighlightPrefab;
    public List<GameObject> highlights = new ();


    protected override void LoadComponents()
    {
        base.LoadComponents();
        redHighlightPrefab = GameObject.Find("Prefab_RedSquare");
        greenHighlightPrefab = GameObject.Find("Prefab_GreenSquare");
        blueHighlightPrefab = GameObject.Find("Prefab_BlueSquare");
        purpleHighlightPrefab = GameObject.Find("Prefab_PurpleSquare");
    }

    public void HighlightValidMoves(List<Vector3Int> positions)
    {
        foreach (var pos in positions)
        {
            SquareManager.Instance.HideSquare(pos);
            GameObject highlight = Instantiate(blueHighlightPrefab, pos, Quaternion.identity);

            highlight.GetComponent<AbstractSquare>().SetPosition(pos);
            highlight.name = "HighlightM_" + (char)('a' + pos.x - 1) + pos.z.ToString();
            highlight.transform.parent = GameObject.Find("Highlights").transform;
            highlights.Add(highlight);
        }
    }

    public void HighlightValidAttacks(List<Vector3Int> positions)
    {
        foreach (var pos in positions)
        {
            SquareManager.Instance.HideSquare(pos);
            GameObject highlight = Instantiate(redHighlightPrefab, pos, Quaternion.identity);

            highlight.GetComponent<AbstractSquare>().SetPosition(pos);
            highlight.name = "HighlightA_" + (char)('a' + pos.x - 1) + pos.z.ToString();
            highlight.transform.parent = GameObject.Find("Highlights").transform;
            highlights.Add(highlight);
        }
    }
    public void ClearHighlights()
    {
        foreach (GameObject highlight in highlights)
            Destroy(highlight);
        highlights.Clear();
    }

    public  void HighlightSelf(Vector3Int position)
    {
        SquareManager.Instance.HideSquare(position);
        GameObject highlight = Instantiate(greenHighlightPrefab, position, Quaternion.identity);
        highlight.GetComponent<AbstractSquare>().SetPosition(position);
        highlight.name = "HighlightS_" + (char)('a' + position.x - 1) + position.z.ToString();
        highlight.transform.parent = GameObject.Find("Highlights").transform;
        highlights.Add(highlight);
    }

    public void HighlightMagic(Vector3Int position)
    {
        SquareManager.Instance.HideSquare(position);
        GameObject highlight = Instantiate(purpleHighlightPrefab, position, Quaternion.identity);
        highlight.GetComponent<AbstractSquare>().SetPosition(position);
        highlight.name = "HighlightM_" + (char)('a' + position.x - 1) + position.z.ToString();
        highlight.transform.parent = GameObject.Find("Highlights").transform;
        highlights.Add(highlight);
    }


}
