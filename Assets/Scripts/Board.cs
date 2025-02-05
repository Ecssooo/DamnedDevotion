using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Transform> _slots = new List<Transform>();

    private Card[,] _board = new Card[3,3];
    private Transform[,] _slotsTab = new Transform[3, 3];

    public GameObject card;

    private void Start()
    {
        InitSlotTab();
        SetSlots(card.GetComponent<Card>());
    }


    /// <summary>
    /// Transform slot list into 2D array
    /// </summary>
    void InitSlotTab()
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
    void ResetBoard()
    {
        if (_board == null) return;
        
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[i,j] = null;
            }
        }
    }

    
    /// <summary>
    /// Add a Card to a slot in board
    /// Not implemented yet : Instantiate Cards to slot in world
    /// </summary>
    /// <param name="card">Cards type, Can be null</param>
    void SetSlots(Card card = null)
    {
        if (PositionInBounds(card.PositionOnBoard))
        {
            _board[card.PositionOnBoard.x, card.PositionOnBoard.y] = card;
            if(card != null) Instantiate(card.gameObject, _slotsTab[card.PositionOnBoard.x, card.PositionOnBoard.y]);
        }
    }

    
    /// <summary>
    /// Setup card on board
    /// </summary>
    /// <param name="level">Level to setup</param>
    // void SetLevel(Level level)
    // {
    //     foreach (var card in level.CardsList)
    //     {
    //         SetSlots(card);
    //     }
    // }
    
    /// <summary>
    ///  Check if position is in board
    /// </summary>
    /// <param name="position">Position to check</param>
    /// <returns>Is in board</returns>
    bool PositionInBounds(Vector2Int position)
    {
        return position.x < _board.GetLength(1) && 
               position.x >= 0 && 
               position.y < _board.GetLength(0) && 
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