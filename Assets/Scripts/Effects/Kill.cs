using UnityEngine;

public class Kill : Effect
{
    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        this.GetComponent<CircleCollider2D>().enabled = false;
        Collider2D card = Physics2D.OverlapCircle(transform.position, _circleCollider2D.radius);
        this.GetComponent<CircleCollider2D>().enabled = true;
        Debug.Log(card);
        if (card != null && card.CompareTag("Human")){
            Human human = card.GetComponent<Human>();
            if (human != null)
            {
                human.OnDie();
            }
        }
    }
}
