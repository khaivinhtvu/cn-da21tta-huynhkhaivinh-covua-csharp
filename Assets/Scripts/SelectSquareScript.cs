using UnityEngine;

public class SelectSquareScript : MonoBehaviour
{
    private void ClearSquare()
    {
        Object.Destroy(gameObject);
        GameScript.RaiseClearAllSquare -= ClearSquare;
    }

    void Start()
    {
        GameScript.RaiseClearAllSquare += ClearSquare;
    }
}
