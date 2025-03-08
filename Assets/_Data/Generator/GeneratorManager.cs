using UnityEngine;

public class GeneratorManager : Singleton<GeneratorManager>
{
    private GeneratorPiece generatorPiece;
    private GeneratorSquare generatorSquare;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        generatorPiece = GameObject.Find("Generator").GetComponent<GeneratorPiece>();
        generatorSquare = GameObject.Find("Generator").GetComponent<GeneratorSquare>(); 
    }
    void Start()
    {
        generatorSquare.Generate();
        Debug.Log("Generate SQUARE");
        generatorPiece.Generate();
        Debug.Log("Generate CHESS");
    }

}
