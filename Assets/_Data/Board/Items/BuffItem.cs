using System.Collections;
using UnityEngine;

public class BuffItem : NewMonoBehaviour ,IAnimation
{

    protected override void Start()
    {
        InitValue();
    }

    protected virtual void InitValue(){}

    //[SerializeField]
    //private Camera _mainCamera;

    public virtual string Description()
    {
        return "";
    }



    private Vector3Int _position;

    public Vector3Int Position
    {
        get => _position;
        set => _position = value;
    }

    protected override void LoadComponents()
    {
        //_mainCamera = Camera.main;
    }

    public void SetPosition(Vector3Int pos)
    {
        Position = pos;
        float height = GeneratorSquare.Instance.SquarePrefab1.transform.localScale.y / 2;
        transform.position = new Vector3(0, 1, 0) * (height) + new Vector3(Position.x, (float)Position.y / 2 + 0.1f, Position.z);
    }

    public virtual void ApplyEffect(Piece piece)
    {
        // Apply effect to the piece
        // For example, increase the piece's attack power or defense
        // This is just a placeholder for the actual effect logic
        Debug.Log($"Applying effect to {piece.name} at position {Position}");
    }


    //private void LateUpdate()
    //{
    //    Vector3 cameraPosition = _mainCamera.transform.position;
    //    cameraPosition.y = transform.position.y;
    //    transform.LookAt(cameraPosition);
    //    transform.Rotate(0, 180f, 0);
    //}


    public void ChangeHeight(int n)
    {
        _position.y += n;
        StartCoroutine(ChangeHeightRoutine(n));
    }


    IEnumerator ChangeHeightRoutine(int n)
    {
        Vector3 start = transform.position;
        Vector3 target = start + Vector3.up * (float)n / 2;
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Chờ 1 frame trước khi tiếp tục
        }
        transform.position = target; // Đảm bảo đạt đúng vị trí../ m,7
    }

    protected void OnMouseDown()
    {
        if (InputBlocker.IsPointerOverUI()) return;
        ClickSquare.Instance.selectSquare(SearchingMethod.FindSquareByPosition(Position));
    }
}
