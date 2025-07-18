using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectOnSquare : NewNetworkBehaviour, IAnimation
{
    [SerializeField][SyncVar] private Vector3Int _position = new(0, 0, 0);
    public Vector3Int Position
    {
        get => _position;
        set => _position = value;
    }

    [Command(requiresAuthority = false)]
    public virtual void SetPosition(Vector3Int pos)
    {

    }


    public virtual void MouseSelected()
    {

    }


    public void ChangeHeight(int n, float duration)
    {
        StartCoroutine(ChangeHeightRoutine(n, duration));
    }

    IEnumerator ChangeHeightRoutine(int n, float duration)
    {
        Vector3 start = transform.position;
        Vector3 target = start + Vector3.up * (float)n / 2;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null; // Chờ 1 frame trước khi tiếp tục
        }
        transform.position = target; // Đảm bảo đạt đúng vị trí../ m,7
        SetPosition(new Vector3Int(Position.x, Position.y + n, Position.z));

    }
}
