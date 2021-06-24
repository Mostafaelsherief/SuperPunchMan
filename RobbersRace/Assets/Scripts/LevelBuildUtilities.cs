using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuildUtilities : MonoBehaviour
{
    int RowNumber; int ColumnNumber;
    public bool upperLeft=true, upperRight=true, lowerLeft=true, lowerRight=true;
    int numberOfMissingTiles;
    private List<Vector2> emptyFloorTiles = new List<Vector2>();
    public List<Vector2>GetEmptyFloorTilesList()
    {
        return emptyFloorTiles;
    }

   public LevelBuildUtilities(int rowNumber, int columnNumber,int noOfMissingTiles)
    {
        RowNumber = rowNumber; ColumnNumber = columnNumber;
        numberOfMissingTiles = noOfMissingTiles;
        FillEmptyTilesList();
    }
    void FillEmptyTilesList()
    {
        if (upperLeft) LeftUpperCornerQuarterDiamondBuild();
        if (upperRight) RightUpperCornerQuarterDiamondBuild();
        if (lowerLeft) LeftBottomCornerQuarterDiamondBuild();
        if (lowerRight) RightBottomCornerQuarterDiamondBuild();
    }

    void LeftUpperCornerQuarterDiamondBuild()
    {
        Vector2 startPosition = new Vector2(RowNumber, numberOfMissingTiles);
        for (int i = 0; i <numberOfMissingTiles; i++)
            for (int j = numberOfMissingTiles-i; j > 0; j--)
            {
                Vector2 currentEmptyFloorTile = (startPosition - Vector2.up * i) + (startPosition - Vector2.right * j);
                emptyFloorTiles.Add(currentEmptyFloorTile);
            }

    }
    void RightUpperCornerQuarterDiamondBuild()
    {
        Vector2 startPosition = new Vector2(RowNumber, ColumnNumber);

        for (int i = 0; i < numberOfMissingTiles; i++)
            for (int j = 0; j < i; j++)
            {
                Vector2 currentEmptyFloorTile = (startPosition - Vector2.up * i) + (startPosition - Vector2.right * j);
                emptyFloorTiles.Add(currentEmptyFloorTile);
            }
    }
    void RightBottomCornerQuarterDiamondBuild()
    {
        Vector2 startPosition = new Vector2(0, ColumnNumber);

        for (int i = 0; i < numberOfMissingTiles; i++)
            for (int j = 0; j < i; j++)
            {
                Vector2 currentEmptyFloorTile = (startPosition + Vector2.up * i) + (startPosition - Vector2.right * j);
                emptyFloorTiles.Add(currentEmptyFloorTile);
            }
    }
    void LeftBottomCornerQuarterDiamondBuild()
    {
        Vector2 startPosition = new Vector2(0, numberOfMissingTiles);

        for (int i = 0; i < numberOfMissingTiles; i++)
            for (int j = 0; j < i; j++)
            {
                Vector2 currentEmptyFloorTile = (startPosition + Vector2.up * i) + (startPosition - Vector2.right * j);
                emptyFloorTiles.Add(currentEmptyFloorTile);
            }
    }
}

