using UnityEngine;

public class Invocation : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Board board;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceCardAtMousePosition();
        }
    }

    private void PlaceCardAtMousePosition()
    {
        Vector2 mousePosition = GetMousePosition();
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider == null) return;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board.SlotsTab[i, j] == hitCollider.transform)
                {
                    Vector2Int boardPosition = new Vector2Int(i, j);
                    if (board.SlotEmpty(boardPosition))
                    {
                        Card newCard = Instantiate(cardPrefab);
                        newCard.PositionOnBoard = boardPosition;
                        board.SetSlots(newCard);
                    }
                }
            }
        }
    }


    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
