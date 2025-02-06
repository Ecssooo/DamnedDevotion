using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Transform> _slots = new List<Transform>();

    private Card[,] _board = new Card[4,3];
    private Transform[,] _slotsTab = new Transform[4, 3];
    
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
    
}