using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Piece Database", menuName = "Inventory System/Piece/Database")]
public class PieceDatabaseObject : ScriptableObject, ISerializationCallbackReceiver {

    public PieceObject[] pieceObjects;
    public Dictionary<int, PieceObject> GetPiece = new Dictionary<int, PieceObject>(); //Lookup for Piece by Id

    public void OnAfterDeserialize() {
      foreach (int i in System.Enum.GetValues(typeof(PieceType))) {
        pieceObjects[i].data.type = (PieceType)System.Enum.Parse(typeof(PieceType), PieceType.GetName(typeof(PieceType), i));
        GetPiece.Add(i, pieceObjects[i]);
      }
    }

    public void OnBeforeSerialize() {
      GetPiece = new Dictionary<int, PieceObject>();
    }
}
