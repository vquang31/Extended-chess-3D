using Mirror;
using Unity.Cinemachine;
using UnityEngine;

public class GeneratorManager : NetworkSingleton<GeneratorManager>
{
    private CinemachineCamera cinemachineCamera;

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
    
    public void Generate()
    {
        GenerateSquare();
        GeneratePiece();

        if (generateMap)
        {
            generatorSquare.GenerateMap();
            Debug.Log("Generate MAP");
        }
    }

    public void GenerateSquare()
    {
        generatorSquare.Generate();
        Debug.Log("Generate SQUARE");
    }

    [Server]
    public void GeneratePiece()
    {
        generatorPiece.Generate();
        Debug.Log("Generate CHESS");
    }



}
