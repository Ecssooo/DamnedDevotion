using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left,
    Top,
    Down
}


public class Board : MonoBehaviour
{
    [SerializeField] private List<Transform> _slots = new List<Transform>();

    public Card[,] _board = new Card[4,3];
    public Transform[,] _slotsTab = new Transform[4, 3];
    
    /// <summary>
    /// Transform slot list into 2D array
    /// </summary>
    public void InitSlotTab()
    {
        int index = 0;

        for (int i = 0; i < _slotsTab.GetLength(0); i++)
        {
            for (int j = 0; j < _slotsTab.GetLength(1); j++)
            {
                _slotsTab[i, j] = _slots[index];
                index++;
            }
        }
    }
    
    /// <summary>
    /// Clear board
    /// </summary>
    public void ResetBoard()
    {
        InitSlotTab();
        if (_board == null) return;
        
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i,j] = null;
                if(_slotsTab[i,j].childCount > 0) DestroyImmediate(_slotsTab[i, j].GetChild(0).gameObject);
            }
        }
    }

    
    /// <summary>
    /// Add a Card to a slot in board
    /// </summary>
    /// <param name="card">Cards type, Can be null</param>
    public void SetSlots(Card card)
    {
        InitSlotTab();

        if (card == null) return;
        if (PositionInBounds(card.PositionOnBoard))
        {
            _board[card.PositionOnBoard.x, card.PositionOnBoard.y] = card;
            card.transform.parent = _slotsTab[card.PositionOnBoard.x, card.PositionOnBoard.y];
            card.transform.localPosition = new Vector3(0,0,0);
        }
    }

    
    /// <summary>
    /// Setup card on board
    /// </summary>
    /// <param name="level">Level to setup</param>
    public void SetLevel(Level level)
    {
        InitSlotTab();
        for (int i = 0; i < level.CardsList.Count; i++)
        {
            if (level.CardsList[i] != null)
            {
                var GO = Instantiate(level.CardsList[i], this.transform);
                var card = GO.GetComponent<Card>();
                card.PositionOnBoard = level.positionCardsList[i];
                SetSlots(card);
            }
        }
    }
    
    /// <summary>
    ///  Check if position is in board
    /// </summary>
    /// <param name="position">Position to check</param>
    /// <returns>Is in board</returns>
    bool PositionInBounds(Vector2Int position)
    {
        return position.x < _board.GetLength(0) && 
               position.x >= 0 && 
               position.y < _board.GetLength(1) && 
               position.y >= 0;
    }

    
    /// <summary>
    /// Check slot available next to
    /// </summary>
    /// <param name="position">Position to check</param>
    /// <returns>Tab of all position available
    /// Vector2Int if yes
    /// Null if not</returns>
    Vector2Int[] DirectionAvailable(Vector2Int position)
    {
        InitSlotTab();
        Vector2Int[] direction = new Vector2Int[4]; 
        
        if (PositionInBounds(position + Vector2Int.right))
        {
            direction[0] = Vector2Int.right;
        }
        if (PositionInBounds(position + Vector2Int.left))
        {
            direction[1] = Vector2Int.left;
        }
        if (PositionInBounds(position + Vector2Int.up))
        {
            direction[2] = Vector2Int.up;
        }
        if (PositionInBounds(position + Vector2Int.down))
        {
            direction[3] = Vector2Int.down;
        }
        
        return direction;
    }


    /// <summary>
    /// Get card in slot close
    /// </summary>
    /// <param name="position">Initial position</param>
    /// <param name="direction">Direction</param>
    /// <returns></returns>
    public Card GetCardClose(Vector2Int position, Direction direction)
    {
        InitSlotTab();
        Card card = null;
        switch (direction)
        {
            case(Direction.Right):
                if (PositionInBounds(position + Vector2Int.right))
                {
                    card = _board[position.x + Vector2Int.right.x, position.y + Vector2Int.right.y];
                }
                break;
            case(Direction.Left):
                if (PositionInBounds(position + Vector2Int.left))
                {
                    card = _board[position.x + Vector2Int.left.x, position.y + Vector2Int.left.y];
                }
                break;
            case(Direction.Top):
                if (PositionInBounds(position + Vector2Int.up))
                {
                    card = _board[position.x + Vector2Int.up.x, position.y + Vector2Int.up.y];
                }
                break;
            case(Direction.Down):
                if (PositionInBounds(position + Vector2Int.down))
                {
                    card = _board[position.x + Vector2Int.down.x, position.y + Vector2Int.down.y];
                }
                break;
        }

        return card;
    }

    /// <summary>
    ///  Check if slot is empty
    /// </summary>
    /// <param name="newPos"></param>
    /// <returns></returns>
    public bool SlotEmpty(Vector2Int newPos)
    {
        InitSlotTab();
        return _board[newPos.x, newPos.y] == null;
    }
    
    /// <summary>
    ///  Move card to slots
    /// </summary>
    /// <param name="card"></param>
    /// <param name="newPos"></param>
    public void MoveCard(Card card, Vector2Int newPos)
    {
        InitSlotTab();
        if (PositionInBounds(newPos) && SlotEmpty(newPos))
        {
            card.PositionOnBoard = newPos;
            SetSlots(card);
        }
    }

    /// <summary>
    /// Switch card position
    /// </summary>
    /// <param name="c1">First card</param>
    /// <param name="c2">Second card</param>
    public void SwitchCard(Card c1, Card c2)
    {
        if (c1 == null || c2 == null) return;
        Vector2Int temp = c1.PositionOnBoard;
        c1.PositionOnBoard = c2.PositionOnBoard;
        c2.PositionOnBoard = temp;

        SetSlots(c1);
        SetSlots(c2);
    }
    
    /// <summary>
    ///  Get position
    /// </summary>
    /// <param name="position">Initial position</param>
    /// <param name="direction">Direction position you want</param>
    /// <returns></returns>
    public Vector2Int GetPositionNextTo(Vector2Int position, Direction direction)
    {
        InitSlotTab();
        switch (direction)
        {
            case(Direction.Right):
                if (PositionInBounds(position + Vector2Int.right)) { return position + Vector2Int.right; }
                break;
            case(Direction.Left):
                if (PositionInBounds(position + Vector2Int.left)) { return position + Vector2Int.left; }
                break;
            case(Direction.Top):
                if (PositionInBounds(position + Vector2Int.up)) { return position + Vector2Int.up; }
                break;
            case(Direction.Down):
                if (PositionInBounds(position + Vector2Int.down)) { return position + Vector2Int.down; }
                break;
        }
        return position;
    }
}