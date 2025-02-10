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
        RaycastHit2D hit2d = new RaycastHit2D(); // Initialize hit2d
        switch (_attackDir)
        {
            case AttackDirection.UP:
                _attackDirection = Vector3.up;
                hit2d = Physics2D.Raycast(
                    new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), 
                    _attackDirection);
                break;
            case AttackDirection.DOWN:
                _attackDirection = Vector3.down;
                hit2d = Physics2D.Raycast(transform.position, _attackDirection);
                break;
            case AttackDirection.LEFT:
                _attackDirection = Vector3.left;
                hit2d = Physics2D.Raycast(transform.position, _attackDirection);
                break;
            case AttackDirection.RIGHT:
                _attackDirection = Vector3.right;
                hit2d = Physics2D.Raycast(transform.position, _attackDirection);
                break;
        }
        Debug.DrawRay(transform.position, _attackDirection * 1.5f, Color.black, 1f);
        Debug.Log(hit2d.collider);
        if (hit2d.collider != null && hit2d.collider.CompareTag("Human"))
        {
            Human human = hit2d.collider.GetComponent<Human>();
            if (human != null)
            {
                Destroy(hit2d.collider.gameObject);
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
