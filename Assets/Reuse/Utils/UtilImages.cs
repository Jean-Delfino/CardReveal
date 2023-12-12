using System.Collections.Generic;
using UnityEngine;

namespace Reuse.Utils
{
    public static class UtilImages
    {
        public class Block
        {
            public Color Color;
            public int ColIndex;

            public Block(Color color, int colIndex)
            {
                this.Color = color;
                this.ColIndex = colIndex;
            }
        }
        public class ImageResult
        {
            public List<List<Block>> Blocks;
            public int PixelCount;
            public int MaxRow;
            public int MaxCol;
        }

        private static void SetMaxRowAndCol(ImageResult result, int row, int col)
        {
            if (result.MaxCol < col) result.MaxCol = col;
            if (result.MaxRow < row) result.MaxRow = row;
        }

        public static ImageResult GetDifferentPixelsThenColor(Texture2D imageConvert, Color defaultColor, bool includeEmptyCol = false)
        {
            var result = new ImageResult
            {
                Blocks = new List<List<Block>>()
            };

            // Loop through the rows (height) of the image
            for (int i = 0; i < imageConvert.height; i++)
            {
                var row = new List<Block>();

                // Loop through the columns (width) of the image
                for (int j = 0; j < imageConvert.width; j++)
                {
                    Color pixelColor = imageConvert.GetPixel(j, i);

                    // Check if the pixel color is different from the default color
                    if (pixelColor != defaultColor)
                    {
                        // Add to the inner list
                        Block block = new Block(pixelColor, j);
                        row.Add(block);

                        result.PixelCount++;
                        SetMaxRowAndCol(result, i, j);
                    }
                }

                // Add the inner list to the main list
                if(row.Count > 0 || includeEmptyCol) result.Blocks.Add(row);
            }

            return result;
        }
    }
}