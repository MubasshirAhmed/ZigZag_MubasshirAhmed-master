using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesCreator : MonoBehaviour
{
    private static TilesCreator _myInstance;

    private Vector3 _spawnTilePosition = new Vector3(-1.5f, 0, 2.5f);
    [SerializeField]
    private GameObject _spawnAbleTile;
    private GameObject _currentSimpleTile;
    [SerializeField]
    private GameObject _allTilesParent;
    private int _randomNumberForTiles;
    private int _randomNumberForDiamonds;
    private List<GameObject> _simpleTiles;
    private int _currentBallTileNumber;
    private int _maxTileNumber = 50;

    private int _startTransformCount = 15;
    private int _startFallingCount = 2;
    private bool _tileTransformCondition;
    private bool _tileFallingCondition;
    private int _tileCount;
    private int _nextFallAbleTile, _nextTransformAbleTile;

    void Start()
    {
        _simpleTiles = new List<GameObject>();
        TileCreate();
    }
    public static TilesCreator MyInstance
    {
        get
        {
            if (_myInstance == null)
                _myInstance = FindObjectOfType<TilesCreator>();
            return _myInstance;
        }
    }

    public List<GameObject> SimpleTiles
    {
        get { return _simpleTiles; }
    }

    private void TileCreate()
    {
        for (int i = 0; i < _maxTileNumber; i++)
        {
            _currentSimpleTile = Instantiate(_spawnAbleTile);
            _currentSimpleTile.transform.parent = _allTilesParent.transform;
            _simpleTiles.Add(_currentSimpleTile);
            _simpleTiles[i].name = "Tile_" + i;
            RandomTilePosition(i);
        }
    }

    private void RandomTilePosition(int i)
    {
        _simpleTiles[i].transform.position = _spawnTilePosition;
        RandomDiamond(i);
        _randomNumberForTiles = Random.Range(0, 2);
        _spawnTilePosition = (_randomNumberForTiles == 0) ? new Vector3(_spawnTilePosition.x - 1, 0, _spawnTilePosition.z) : new Vector3(_spawnTilePosition.x, 0, _spawnTilePosition.z + 1);
    }

    private void RandomDiamond(int i)
    {
        _randomNumberForDiamonds = Random.Range(0, 10);
        if (_randomNumberForDiamonds == 0)
            _simpleTiles[i].transform.GetChild(1).gameObject.SetActive(true);
        else
            _simpleTiles[i].transform.GetChild(1).gameObject.SetActive(false);
    }

    public void TilesFallingAndTransform()
    {
        if (_startTransformCount == _tileCount)
            _tileTransformCondition = true;
        if (_startFallingCount == _tileCount)
            _tileFallingCondition = true;
        if (_tileFallingCondition)
        {
            _nextFallAbleTile = (_maxTileNumber + (_tileCount - _startFallingCount)) % _maxTileNumber;
            _simpleTiles[_nextFallAbleTile].GetComponent<Rigidbody>().isKinematic = false;
        }

        if (_tileTransformCondition)
        {
            _nextTransformAbleTile = (_maxTileNumber + (_tileCount - _startTransformCount)) % _maxTileNumber;
            _simpleTiles[_nextTransformAbleTile].GetComponent<Rigidbody>().isKinematic = true;
            RandomTilePosition(_nextTransformAbleTile);
        }
        _tileCount++;
        if (_tileCount >= _maxTileNumber)
            _tileCount = 0;
    }
}
