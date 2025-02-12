using UnityEngine;
using UnityEngine.Serialization;

public class SwordedKnight : Card
{

    [SerializeField] int _value;
    public int Value
    {
        get { return _value; }
    }
    // [FormerlySerializedAs("_attackDir")] [SerializeField] private Direction dir;
    // public Direction Dir { get => dir; set => dir = value; }
    private Vector3 _attackDirection;

    public void Attack()
    {
        RaycastHit2D hit2d = new RaycastHit2D(); // Initialize hit2d
        switch (Direction)
        {
            case Direction.UP:
                _attackDirection = Vector3.up;
                hit2d = Physics2D.Raycast(
                    new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, transform.position.z), 
                    _attackDirection);
                break;
            case Direction.DOWN:
                _attackDirection = Vector3.down;
                hit2d = Physics2D.Raycast(transform.position, _attackDirection);
                break;
            case Direction.LEFT:
                _attackDirection = Vector3.left;
                hit2d = Physics2D.Raycast(transform.position, _attackDirection);
                break;
            case Direction.RIGHT:
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
