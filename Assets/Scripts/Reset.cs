using UnityEngine;

public class Reset : MonoBehaviour
{
    public void ResetGame()
    {
        GameManager.Instance.Board.ResetBoard();
    }
}
