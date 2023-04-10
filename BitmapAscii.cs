using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Text;

class BitmapAscii
{
    // FIELDS
    private string _asciiText = "";

    #region
    // PUBLIC METHODS
    public string Asciitize (BitmapMaker bmpImage, int kernelWidth, int kernelHeight, int numberOfChars)
    {
        // This method should accept a BitmapMaker instance and return
        // a string containing the ascii text version of the picture

        // Create a BitmapAscii object
        BitmapAscii bitmapAscii = new BitmapAscii();

        // Create string to store ascii text
        StringBuilder asciiText = new StringBuilder();

        // Loop through image
        for (int i = 0; i < bmpImage.Height; i += kernelHeight)
        {
            for (int j = 0; j < bmpImage.Width; j += kernelWidth)
            {
                // Set height increment
                int heightInc = i + kernelHeight;

                // If height increment is greater than image height
                if (heightInc > bmpImage.Height)
                {
                    // Set height increment to image height
                    heightInc = bmpImage.Height;
                }// End if

                // Set width increment
                int widthInc = j + kernelWidth;

                // If width increment is greater than image width
                if (widthInc > bmpImage.Width)
                {
                    // Set width increment to image width
                    widthInc = bmpImage.Width;
                }// End if

                // Create a list to store colors
                List<Color> colorList = new List<Color>();

                // Loop through kernel
                for (int y = i; y < heightInc; y++)
                {
                    for (int x = j; x < widthInc; x++)
                    {
                        // Get color of each pixel
                        Color color = bmpImage.GetPixelColor(x, y);

                        // Add color to color list
                        colorList.Add(color);
                    }// End for
                }// End for

                // Get the average color of the color list
                double avgColorNum = bitmapAscii.AveragePixel(colorList);

                // Get the ascii character from the value of the average color
                string asciiChar = bitmapAscii.GrayToString(avgColorNum, numberOfChars);

                // Add the ascii character to the ascii string
                asciiText.Append(asciiChar);
            }// End for

            // Start a new line in the ascii string
            asciiText.Append("\n");
        }// End for

        // Store the asciiText in the private field
        _asciiText = asciiText.ToString();

        // Return the complete ascii string
        return _asciiText;
    }// End Asciitize

    public override string ToString()
    {
        // Overload the ToString() method to return a string containing
        // the entire ascii version of the image

        // Return the asciiText
        return _asciiText;
    }// End override ToString()
    #endregion

    #region
    // PRIVATE METHODS
    private double AveragePixel(int r, int g, int b)
    {
        // Average color's r,g,b value
        double avg = (r + g + b) / 3;

        // Normalize the average color
        double avgNormalized = avg / 255;

        // Return normalized average
        return avgNormalized;
    }// End AveragePixel (int, int, int)

    private double AveragePixel(Color color)
    {
        // This method should accept components for a color instance and return
        // a normalized value (0-1) of the grey value calculated from the RGB values

        // Average color's r,g,b value
        double avg = (color.R + color.G + color.B) / 3;

        // Normalize the average color
        double avgNormalized = avg / 255;

        // Return normalized average
        return avgNormalized;
    }// End AveragePixel(Color)

    private double AveragePixel(List<Color> colorList)
    {
        // This method should accept a list of Colors and return a normalized value
        // (0-1) of the grey value calculated from the Colors RGB value

        // Create variable to store total average
        double totalAvg = 0;

        // If list count is 0
        if (colorList.Count == 0)
        {
            // Return default
            return default;
        }// End if
        else
        {
            // Loop through each color in list
            foreach (Color color in colorList)
            {
                // Average color's r,g,b value
                double avg = (color.R + color.G + color.B) / 3;

                // Add the average to the total
                totalAvg += avg;
            }// End foreach

            // Divide the total average by number of colors added
            totalAvg /= colorList.Count;

            // Noramlize average
            totalAvg /= 255;

            // Return normalized total average
            return totalAvg;
        }// End else
    }// End AveragePixel(Color)

    private string GrayToString(double number, int numberOfChars)
    {
        // Should accept a normalized value (0-1) and return a string containing
        // the ascii character mapped to the range the value falls in

        // Check normalized value and return ascii character
        if (numberOfChars == 0)
        {
            if (0 <= number && number < 0.16)
            {
                return "@";
            }// End if
            else if (number < 0.33)
            {
                return "%";
            }// End else if
            else if (number < 0.50)
            {
                return "*";
            }// End else if
            else if (number < 0.66)
            {
                return ":";
            }// End else if
            else if (number < 0.83)
            {
                return ".";
            }// End else if
            else if (number <= 1)
            {
                return " ";
            }// End else if
        }// End if

        else if (numberOfChars == 1)
        {
            if (0 <= number && number < 0.10)
            {
                return "@";
            }// End if
            else if (number < 0.20)
            {
                return "%";
            }// End else if
            else if (number < 0.30)
            {
                return "$";
            }// End else if
            else if (number < 0.40)
            {
                return "#";
            }// End else if
            else if (number < 0.50)
            {
                return "X";
            }// End else if
            else if (number < 0.60)
            {
                return "*";
            }// End else if
            else if (number < 0.70)
            {
                return "+";
            }// End else if
            else if (number < 0.80)
            {
                return ":";
            }// End else if
            else if (number < 0.90)
            {
                return ".";
            }// End else if
            else if (number <= 1)
            {
                return " ";
            }// End else if
        }// End if

        else if (numberOfChars == 2)
        {
            if (0 <= number && number < 0.05)
            {
                return "@";
            }// End if
            else if (0.05 <= number && number < 0.10)
            {
                return "%";
            }// End else if
            else if (0.10 <= number && number < 0.15)
            {
                return "$";
            }// End else if
            else if (0.15 <= number && number < 0.20)
            {
                return "#";
            }// End else if
            else if (0.20 <= number && number < 0.25)
            {
                return "8";
            }// End else if
            else if (0.25 <= number && number < 0.30)
            {
                return "X";
            }// End else if
            else if (0.30 <= number && number < 0.35)
            {
                return "G";
            }// End else if
            else if (0.35 <= number && number < 0.40)
            {
                return "9";
            }// End else if
            else if (0.40 <= number && number < 0.45)
            {
                return "S";
            }// End else if
            else if (0.45 <= number && number < 0.50)
            {
                return "Z";
            }// End else if
            else if (0.50 <= number && number < 0.55)
            {
                return "C";
            }// End else if
            else if (0.55 <= number && number < 0.60)
            {
                return "7";
            }// End else if
            else if (0.60 <= number && number < 0.65)
            {
                return "v";
            }// End else if
            else if (0.65 <= number && number < 0.70)
            {
                return "?";
            }// End else if
            else if (0.70 <= number && number < 0.75)
            {
                return "*";
            }// End else if
            else if (0.75 <= number && number < 0.80)
            {
                return "+";
            }// End else if
            else if (0.80 <= number && number < 0.85)
            {
                return ";";
            }// End else if
            else if (0.85 <= number && number < 0.90)
            {
                return ":";
            }// End else if
            else if (0.90 <= number && number < 0.95)
            {
                return ".";
            }// End else if
            else if (0.95 <= number && number <= 1)
            {
                return " ";
            }// End else if
        }// End if
        
        else
        {
            // If outside 0-1 value
            throw new Exception("Value must be between 0 and 1");
        }// End else

        return string.Empty;
    }// End GrayToString()
    #endregion

}// End class