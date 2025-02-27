using UnityEngine;

public class Switch : MonoBehaviour
{
    private static Card _firstCard;
    private static Card _secondCard;

    // private void OnMouseDown()
    // {
    //     if (enabled)
    //     {
    //         Vector2 mousePosition = GetMousePosition();
    //         Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
    //         if (hitCollider != null)
    //         {
    //             Card clickedCard = hitCollider.GetComponentInParent<Card>();
    //             if (clickedCard != null)
    //             {
    //                 if (_firstCard == null)
    //                 {
    //                     _firstCard = clickedCard;
    //                 }
    //                 else if (_secondCard == null)
    //                 {
    //                     _secondCard = clickedCard;
    //                     GameManager.Instance.Board.SwitchCard(_firstCard, _secondCard);
    //                     _firstCard = null;
    //                     _secondCard = null;
    //                 }
    //             }
    //         }
    //     }
    // }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
