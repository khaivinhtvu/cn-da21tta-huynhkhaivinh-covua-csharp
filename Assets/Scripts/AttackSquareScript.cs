using UnityEngine;

public class AttackSquareScript : MonoBehaviour
{
    public delegate void AttackSquareClicked(float x, float y);
    public static event AttackSquareClicked RaiseAttackSquareClicked;

    private void ClearSquare()
    {
        Object.Destroy(gameObject);
        GameScript.RaiseClearAllSquare -= ClearSquare;
    }

    void Start()
    {
        GameScript.RaiseClearAllSquare += ClearSquare;
    }

    private void OnDestroy()
    {
        GameScript.RaiseClearAllSquare -= ClearSquare;
    }

    private void OnMouseDown()
    {
        Debug.Log("attack square clicked");
        RaiseAttackSquareClicked?.Invoke(transform.position.x, transform.position.y);
    }
}
