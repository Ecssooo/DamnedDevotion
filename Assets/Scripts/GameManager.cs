using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Effects _effect;
    public Effects Effect
    {
        get => _effect;
        set => _effect = value;
    }
    #region Instance

    private static GameManager _instance;

    public static GameManager Instance { get => _instance; }

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EffectList.Effects = Effects.NONE;
        EffectList.MoveCard = false;
        EffectList.SwapCard = false;

    }
    #endregion

    [SerializeField] private Board _board;
    public Board Board { get => _board; }

    [SerializeField] private LevelDatabase _levelDatabase;
    public LevelDatabase LevelDatabase {get => _levelDatabase; }

    [SerializeField] private ListAction _listAction;
    public ListAction ListActions { get => _listAction; }

    [SerializeField] private ActionCount _actionCount;
    public ActionCount ActionCount { get => _actionCount; }

    private int _monsterScore;
    public int MonsterScore { get => _monsterScore; set => _monsterScore = value; }


    [SerializeField] private List<GameObject> _effectBG = new List<GameObject>();

    private GameState _gameState;
    public GameState GameState { get => _gameState; set => _gameState = value; }

    private List<Card> _miniMonsterCards = new List<Card>();
    public List<Card> MiniMonsterCards { get => _miniMonsterCards; set => _miniMonsterCards = value; }

    private int humanKill;
    public int HumanKill { get => humanKill; set => humanKill = value; }

    [SerializeField] private Button _buttonReady;
    public Button ButtonReady
    {
        get => _buttonReady;
        
    }
    
    [SerializeField] private SpriteRenderer _sprite;
    private void Update()
    {
        if (GameState == GameState.Busy) Effect = Effects.NONE;
        UpdateEffectUI();
    }

    private void UpdateEffectUI()
    {
        switch (_effect)
        {
            case(Effects.MOVE): 
                _effectBG[0].SetActive(true);
                _effectBG[1].SetActive(false);
                _effectBG[2].SetActive(false);
                break;
            case(Effects.SWAP): 
                _effectBG[0].SetActive(false);
                _effectBG[1].SetActive(true);
                _effectBG[2].SetActive(false);
                break;
            case(Effects.INVOKE): 
                _effectBG[0].SetActive(false);
                _effectBG[1].SetActive(false);
                _effectBG[2].SetActive(true);
                break;
            case(Effects.NONE): 
                _effectBG[0].SetActive(false);
                _effectBG[1].SetActive(false);
                _effectBG[2].SetActive(false);
                break;
        }

        if (humanKill >= 20) PlayGamesController.Instance.UnlockAchievement("CgkImLeVnfkcEAIQDw");
    }
    
    private IEnumerator ApplyFonduNoir()
    {
        // float a = 0;
        // for (int  i = 0;  i < 256;  i++)
        // {
        //     a++;
        //     sprite.color = new Color(0, 0, 0, a);
        // }
        while (true){
            if (_sprite.color.a >= 255) yield break;
            Color color = _sprite.color;
            color.a += Time.deltaTime;
            _sprite.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator RemoveFonduNoir()
    {
        while (true)
        {
            if (_sprite.color.a <= 0) yield break;
            Color color = _sprite.color;
            color.a -= Time.deltaTime;
            _sprite.color = color;
            yield return new WaitForSeconds(1);
        }
    }

    public void CoroutineApplyFonduNoir()
    {
        StartCoroutine(ApplyFonduNoir());
    }

    public void CoroutineRemoveFonduNoir()
    {
        StartCoroutine(RemoveFonduNoir());
    }
}