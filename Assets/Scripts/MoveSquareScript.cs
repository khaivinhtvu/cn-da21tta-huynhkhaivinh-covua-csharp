using UnityEngine;

public class MoveSquareScript : MonoBehaviour
{
    public delegate void MoveSquareClicked(float x, float y);
    public static event MoveSquareClicked RaiseMoveSquareClicked;

    private void ClearSquare()
    {
        Object.Destroy(gameObject);
        GameScript.RaiseClearAllSquare -= ClearSquare;
    }

    private void Start()
    {
        GameScript.RaiseClearAllSquare += ClearSquare;
    }

    private void OnDestroy()
    {
        GameScript.RaiseClearAllSquare -= ClearSquare;
    }

    private void OnMouseDown()
    {
        Debug.Log("move square clicked");
        RaiseMoveSquareClicked?.Invoke(transform.position.x, transform.position.y);
    }
}
