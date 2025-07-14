using Mirror;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GeneratorManager : NetworkSingleton<GeneratorManager>
{

    private GeneratorPiece generatorPiece;
    private GeneratorSquare generatorSquare;
    private GeneratorItemBuff generatorItemBuff;
    
    public bool generateMap = true;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        generatorPiece = GameObject.Find("Generator").GetComponent<GeneratorPiece>();
        generatorSquare = GameObject.Find("Generator").GetComponent<GeneratorSquare>();
        generatorItemBuff = GameObject.Find("Generator").GetComponent<GeneratorItemBuff>();
    }

    [Server]
    public void Generate()
    {
        StartCoroutine(GenerateRoutine());
    }

    [Server]
    private IEnumerator GenerateRoutine()
    {
        GenerateSquare();

        yield return new WaitForSeconds(0.2f);

        GeneratePiece();
        yield return new WaitForSeconds(0.2f);

        if (generateMap)
        {
            generatorSquare.GenerateMap();
            Debug.Log("Generate MAP");
        }
    }

    private void GenerateSquare() 
    {
        generatorSquare.Generate();
        Debug.Log("Generate SQUARE");
    }

    private void GeneratePiece()
    {
        generatorPiece.Generate();
        Debug.Log("Generate CHESS");
    }



}
