using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool _simpleTileCondiotn;

    private void OnCollisionEnter(Collision other)
    {
        _simpleTileCondiotn = TilesCreator.MyInstance.SimpleTiles.Find(x => x.gameObject.name.Equals(other.gameObject.name));
        if(_simpleTileCondiotn)
            TilesCreator.MyInstance.TilesFallingAndTransform();
    }
}
