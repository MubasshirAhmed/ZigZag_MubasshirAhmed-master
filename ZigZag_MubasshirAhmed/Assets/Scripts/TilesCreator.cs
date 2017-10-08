using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private Renderer[] rend;

    private Color colorStart = new Color32(0, 236, 207, 255);
    private Color colorEnd = new Color32(233, 114, 3, 0);
    private float duration = 5.0f;

    private bool colorChange;

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
        StartCoroutine(ITileColorChange());
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
        else if (_startFallingCount == _tileCount)
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

    private IEnumerator ITileColorChange()
    {
        while (true)
        {
            colorChange = false;
            yield return new WaitForSeconds(5.0f);
            colorChange = true;     
        }
    }

    private void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        if (!colorChange)
            _simpleTiles.Select(c => { c.transform.GetChild(0).transform.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp); return c; }).ToList();
        else
            _simpleTiles.Select(c => { c.transform.GetChild(0).transform.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(colorEnd, colorStart, lerp); return c; }).ToList();
    }
}
