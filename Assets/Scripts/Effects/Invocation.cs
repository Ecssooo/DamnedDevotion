using System.Collections;
using UnityEngine;

public class Invocation : MonoBehaviour
{
    [SerializeField] private Card miniMonsterPrefab;


    public IEnumerator InvokeMiniMonster()
    {
        GameManager.Instance.Effect = Effects.INVOKE;
        yield return new WaitForSeconds(.3f);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        PlaceCardAtMousePosition();
    }

    public void AddInvokeAction()
    {
        GameManager.Instance.Effect = Effects.INVOKE;
        //EffectActions.Instance.CreateAction(miniMonsterPrefab, )
    }
    
    public void PlaceCardAtMousePosition()
    {
        Vector2 mousePosition = GetMousePosition();
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        if (hitCollider == null) return;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (GameManager.Instance.Board.SlotsTab[i, j] == hitCollider.transform)
                {
                    Vector2Int boardPosition = new Vector2Int(i, j);
                    
                    
                    if (GameManager.Instance.Board.SlotEmpty(boardPosition))
                    {
                        Card newCard = Instantiate(miniMonsterPrefab);
                        newCard.PositionOnBoard = boardPosition;
                        Action invokeAction = EffectActions.Instance.CreateAction(newCard, boardPosition);
                        invokeAction._effect = Effects.INVOKE;
                        ListAction.Instance.AddAction(invokeAction);

                        GameManager.Instance.Board.SetSlots(newCard);
                        GameManager.Instance.ActionCount.Decrement(1);
                        this.enabled = false;
                        GameManager.Instance.Effect = Effects.NONE;
                        PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQBg");
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
