using UnityEngine;

    public class SwordedKnight : Card
{
    public enum AttackDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    [SerializeField] int _value;
    public int Value
    {
        get { return _value; }
    }
    [SerializeField] private AttackDirection _attackDir;
    private Vector3 _attackDirection;

    public void Attack()
    {
        switch (_attackDir)
        {
            case AttackDirection.UP:
                _attackDirection = Vector3.up;
                break;
            case AttackDirection.DOWN:
                _attackDirection = Vector3.down;
                break;
            case AttackDirection.LEFT:
                _attackDirection = Vector3.left;
                break;
            case AttackDirection.RIGHT:
                _attackDirection = Vector3.right;
                break;
        }
        this.GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _attackDirection);
        this.GetComponent<BoxCollider2D>().enabled = true;
        Debug.DrawRay(transform.position, _attackDirection * 3, Color.black, 1f);
        Debug.Log(hit.collider);
        if (hit.collider != null && hit.collider.CompareTag("Human"))
        {
            Human human = hit.collider.GetComponent<Human>();
            if (human != null)
            {
                human.OnDie();
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("SwordedKnight clicked");
        Attack();
    }
}
