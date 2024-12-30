using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameScript;

public class ResultScript : MonoBehaviour
{
    private string pieceId;
    [SerializeField] private GameObject rb;
    [SerializeField] private GameObject textbox;

    private void KingDead(string newpieceId)
    {
        rb.SetActive(true);
        pieceId = newpieceId;

        if (pieceId.Contains("w"))
        {
            textbox.GetComponent<TextMeshProUGUI> ().text = "Quân Đen Thắng";
        }

        if (pieceId.Contains("b"))
        {
            textbox.GetComponent<TextMeshProUGUI> ().text = "Quân Trắng Thắng";
        }
    }

    void Start()
    {
        KingScript.KingDestroyed += KingDead;
    }

    private void OnDestroy()
    {
        KingScript.KingDestroyed -= KingDead;
    }
}
