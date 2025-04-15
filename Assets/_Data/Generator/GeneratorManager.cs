using Unity.Cinemachine;
using UnityEngine;

public class GeneratorManager : Singleton<GeneratorManager>
{
    private CinemachineCamera cinemachineCamera;

    private GeneratorPiece generatorPiece;
    private GeneratorSquare generatorSquare;
    private GeneratorItemBuff generatorItemBuff;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        generatorPiece = GameObject.Find("Generator").GetComponent<GeneratorPiece>();
        generatorSquare = GameObject.Find("Generator").GetComponent<GeneratorSquare>();
        generatorItemBuff = GameObject.Find("Generator").GetComponent<GeneratorItemBuff>();
    }
    protected override void Start()
    {
        generatorSquare.Generate();
        Debug.Log("Generate SQUARE");
        generatorPiece.Generate();
        Debug.Log("Generate CHESS");
    }

}
