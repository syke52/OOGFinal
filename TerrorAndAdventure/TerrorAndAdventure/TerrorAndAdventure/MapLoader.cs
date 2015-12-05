using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TerrorAndAdventure
{
    class MapLoader
    {
        public static void MapLoader(string filePath)
        {
            //Create a new XML document and load the selected save file
            XmlDocument loadFile = new XmlDocument();
            loadFile.Load(filePath);
            XmlNodeList settings = loadFile.SelectNodes("/nPuzzleGame/*");

            //Iterate over each node in the XML file, and save it as the appropriate setting
            foreach (XmlNode item in settings)
            {
                switch (item.Name)
                {
                    //Save the Board Size properties
                    case "tileSet":
                        //Square.BoardLengthX = int.Parse(item.Attributes["width"].Value);
                        //Square.BoardLengthY = int.Parse(item.Attributes["height"].Value);
                        //Square.SquaresArray = new Square[Square.BoardLengthX, Square.BoardLengthX];
                        break;
                    //Save the position of the Open Square
                    case "OpenSquare":
                        //Square.OpenX = int.Parse(item.Attributes["openX"].Value);
                        //Square.OpenY = int.Parse(item.Attributes["openY"].Value);
                        break;
                    //Create the necessary squares for the board
                    case "SquaresArray":
                        foreach (XmlNode square in loadFile.SelectNodes("//SquaresArray/*"))
                        {
                            int x = int.Parse(square.Attributes["squareX"].Value);
                            int y = int.Parse(square.Attributes["squareY"].Value);
                            int value = int.Parse(square.Attributes["squareValue"].Value);
                            //Square currentSquare = new Square(x, y, value);
                        }
                        break;
                    //Create the move counter
                    case "MoveCounter":
                        //Square.MoveCounter = int.Parse(item.Attributes["counter"].Value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

