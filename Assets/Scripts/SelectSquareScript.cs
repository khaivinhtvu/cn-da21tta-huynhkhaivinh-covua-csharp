using UnityEngine;
using static GameScript;

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

    private void OnDestroy()
    {
        GameScript.RaiseClearAllSquare -= ClearSquare;
    }
}
