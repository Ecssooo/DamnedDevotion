using UnityEngine;

public class TutoLevel1 : TutoLevelBase
{
    private int action = 0;

    [SerializeField] private GameObject scene1;
    [SerializeField] private GameObject scene2;
    
    public override void DoTuto()
    {
        if (action == 0 && GameManager.Instance.Effect == Effects.MOVE)
        {
            scene1.SetActive(false);
            scene2.SetActive(true);
            action = 1;
        }
        else if (action == 1 && ListAction.Instance.ListActions.Count > 0)
        {
            if(ListAction.Instance.ListActions[0]._card.CardType == CardType.KNIGHTSWORD)
            {
                scene2.SetActive(false);
                action = 2;
            }
        }
    }
}
