using System.Collections;
using Layout;
using Reuse.Utils;
using UnityEngine;

public class CardSpawner
{
    public IEnumerator SpawnCards(Transform spawnLocation, Card cardPrefab, LevelDefinition levelDefinition)
    {
        var cardLayout = levelDefinition.GetLevelLayout();
        var cardStartingPoint = new Vector3(levelDefinition.GetStartingPointX(), levelDefinition.GetStartingPointY());
        var cardSpacing = levelDefinition.GetSpacings();

        (int x, int y) middlePoint = (0,0);
        SetMiddlePoint(ref middlePoint, cardLayout);
        
        bool destroyDeckAtEnd = (cardLayout.PixelCount % 2 == 1); //Odd number 
        
        for (int i = 0; i < cardLayout.Blocks.Count; i++)
        {
            var rowOfCards = cardLayout.Blocks[i];
            var yPosition = GetCardYPosition(i, middlePoint.y, cardStartingPoint.y, cardSpacing.y);
            for (int j = 0; j < rowOfCards.Count; j++)
            {
                if (destroyDeckAtEnd && i == middlePoint.y && j == middlePoint.x)
                {
                    continue;
                }
                
                var cardClone = Object.Instantiate(cardPrefab, spawnLocation);
                var xPosition = GetCardXPosition(rowOfCards[j].ColIndex, middlePoint.x, cardStartingPoint.x, cardSpacing.x);
                
                CardManager.Instance.SetCardVisual(cardClone);
                CardManager.Instance.CardAnimationAdd(cardClone, new Vector3(xPosition, yPosition, 0), cardStartingPoint);
                yield return null;
            }
        }
        
        //Set camera bounds
        CardManager.Instance.SetBoardBoundaries(
            GetCardXPosition( 0, middlePoint.x, cardStartingPoint.x, cardSpacing.x),
            GetCardYPosition( 0, middlePoint.y, cardStartingPoint.y, cardSpacing.y),
            GetCardXPosition( cardLayout.MaxRow, middlePoint.x, cardStartingPoint.x, cardSpacing.x),
            GetCardYPosition( cardLayout.MaxCol, middlePoint.y, cardStartingPoint.y, cardSpacing.y));

        CardManager.Instance.SetCardsAmount(cardLayout.PixelCount);
        CardManager.Instance.CardAnimationStart();
        
        yield return null;
    }

    private static float GetCardXPosition(int x, int middlePointX, float cardStartingPointX, float cardSpacingX)
    {
        return cardStartingPointX + ((x - middlePointX) * cardSpacingX);
    }

    private static float GetCardYPosition(int y, int middlePointY, float cardStartingPointY, float cardSpacingY)
    {
        return cardStartingPointY + ((y - middlePointY) * cardSpacingY);
    } 

    private void SetMiddlePoint(ref (int x, int y) middlePoint, UtilImage.ImageResult cardLayout)
    {
        middlePoint.x = Mathf.FloorToInt(cardLayout.MaxCol / 2.0f);
        middlePoint.y = Mathf.FloorToInt(cardLayout.MaxRow / 2.0f);
    }
}
