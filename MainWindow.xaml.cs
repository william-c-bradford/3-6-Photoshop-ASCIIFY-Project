using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _3_6_Photoshop_ASCIIFY_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Fields
        private string? _loadedImage;
        private int _numberOfChars = 0;

        public MainWindow()
        {
            InitializeComponent();
        }// End MainWindow
        
        private void BtnASCIIFY_Click(object sender, RoutedEventArgs e)
        {
            // Clear the ascii text box
            txtASCII.Text = string.Empty;

            // Create new BitmapAscii object
            BitmapAscii bitmapAscii = new BitmapAscii();

            // If image has been loaded
            if (_loadedImage != null )
            {
                // Set BitmapMaker object to loaded image
                BitmapMaker bitmapMaker = new BitmapMaker(_loadedImage);
                
                // Convert the loaded image to grayscale
                BitmapMaker bmpGrayscale = ConvertToGrayscale(bitmapMaker);

                // Asciitize the bitmap image and store in string
                string asciiString = bitmapAscii.Asciitize(bmpGrayscale, 
                    Int32.Parse(txtKernelWidth.Text), Int32.Parse(txtKernelHeight.Text),
                    _numberOfChars);
            
                // Display ascii string in ascii text box
                txtASCII.Text += asciiString;
            }// End if
        }// End btnASCIIFY_Click()

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            // Clear the image box
            imgMain.Source = null;

            // Clear the ascii text box
            txtASCII.Text = string.Empty;

            // Clear the loaded image path
            _loadedImage = null;
        }// End btnClearAll_Click()

        private void BtnCopyAscii_Click(object sender, RoutedEventArgs e)
        {
            // If the ascii text box is not empty
            if (txtASCII.Text != string.Empty)
            {
                // Copy the ascii text to the clipboard
                Clipboard.SetText(txtASCII.Text);
            }// End if
        }// End btnCopyAscii_Click

        private void FontPlus_Click(object sender, RoutedEventArgs e)
        {
            // Get the font size
            int fontSizeNum = Int32.Parse(txtFontSize.Text);

            // Font maximum 100
            if (fontSizeNum < 100)
            {
                // Increase font size
                fontSizeNum++;
            }// End if

            // Display the font size in the text box
            txtFontSize.Text = fontSizeNum.ToString();

            // Change the font size of the ascii text box
            txtASCII.FontSize = fontSizeNum;
        }// End fontPlus_Click()

        private void FontMinus_Click(object sender, RoutedEventArgs e)
        {
            // Get the font size
            int fontSizeNum = Int32.Parse(txtFontSize.Text);

            // Font minimum 1
            if (fontSizeNum > 1)
            {
                // Decrease font size
                fontSizeNum--;
            }// End if

            // Display the font size in the text box
            txtFontSize.Text = fontSizeNum.ToString();

            // Change the font size of the ascii text box
            txtASCII.FontSize = fontSizeNum;
        }// End fontMinus_Click()

        private void MuiOpen_Click(object sender, RoutedEventArgs e)
        {
            // Create open file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Setup parameters for open file dialog
            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "PNG Files (.png)|*.png";

            // Show file dialog
            bool? result = openFileDialog.ShowDialog();

            // Process dialog results to determine if a file was opened
            if (result == true)
            {
                // Store file path
                _loadedImage = openFileDialog.FileName;

                // Call LoadImage method
                LoadImage(_loadedImage);

                // Clear the ascii text box
                txtASCII.Text = string.Empty;
            }// End if
        }// End muiOpen_Click()

        private void LoadImage(string path)
        {
            // Create a bitmap image to load the data
            BitmapMaker bmpImage = new BitmapMaker(path);

            // Create a grayscale copy of the bitmap
            BitmapMaker bmpCopy = ConvertToGrayscale(bmpImage);

            // Create a new bitmap of the original image
            WriteableBitmap wbmImage = bmpImage.MakeBitmap();

            // Set image control to display the original image
            imgMain.Source = wbmImage;
        }// End LoadImage()

        public static BitmapMaker ConvertToGrayscale(BitmapMaker bmpOriginal)
        {
            // Create a blank bitmap the same size as the original image
            BitmapMaker bmpNewImage = new BitmapMaker(bmpOriginal.Width, bmpOriginal.Height);

            // Loop through all the pixels in the original image
            for (int x = 0; x < bmpOriginal.Width; x++)
            {
                for (int y = 0; y < bmpOriginal.Height; y++)
                {
                    // Get the color of the pixel at this location
                    Color originalColor = bmpOriginal.GetPixelColor(x, y);

                    // Calculate the grayscale value of the pixel
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));

                    // Convert the grayscale value to a byte
                    byte grayScaleByte = (byte)grayScale;

                    // Create a new color using the grayscale value for each component
                    Color grayScaleColor = Color.FromArgb(originalColor.A, grayScaleByte, grayScaleByte, grayScaleByte);

                    // Set the pixel in the new bitmap to the new color
                    bmpNewImage.SetPixel(x, y, grayScaleColor);
                }// End for
            }// End for

            // Return the new grayscale image
            return bmpNewImage;
        }// End ConvertToGrayscale()

        private void PlusKernelWidth_Click(object sender, RoutedEventArgs e)
        {
            // Get the kernel width
            int kernelWidthNum = Int32.Parse(txtKernelWidth.Text);

            // Kernel width maximum 30
            if (kernelWidthNum < 30)
            {
                // Increase kernel width
                kernelWidthNum++;
            }// End if

            // Display the kernel width in the text box
            txtKernelWidth.Text = kernelWidthNum.ToString();
        }// End plusKernelWidth_Click()

        private void MinusKernelWidth_Click(object sender, RoutedEventArgs e)
        {
            // Get the kernel width
            int kernelWidthNum = Int32.Parse(txtKernelWidth.Text);

            // Kernel width minimum 1
            if (kernelWidthNum > 1)
            {
                // Decrease kernel width
                kernelWidthNum--;
            }// End if

            // Display the kernel width in the text box
            txtKernelWidth.Text = kernelWidthNum.ToString();
        }// End minusKernelWidth_Click()

        private void PlusKernelHeight_Click(object sender, RoutedEventArgs e)
        {
            // Get the kernel height
            int kernelHeightNum = Int32.Parse(txtKernelHeight.Text);

            // Kernel height maximum 30
            if (kernelHeightNum < 30)
            {
                // Increase kernel height
                kernelHeightNum++;
            }// End if

            // Display the kernel height in the text box
            txtKernelHeight.Text = kernelHeightNum.ToString();
        }// End plusKernelHeight_Click()

        private void MinusKernelHeight_Click(object sender, RoutedEventArgs e)
        {
            // Get the kernel height
            int kernelHeightNum = Int32.Parse(txtKernelHeight.Text);

            // Kernel height minimum 1
            if (kernelHeightNum > 1)
            {
                // Decrease kernel height
                kernelHeightNum--;
            }// End if

            // Display the kernel height in the text box
            txtKernelHeight.Text = kernelHeightNum.ToString();
        }// End minusKernelHeight_Click()

        private void CharPlus_Click(object sender, RoutedEventArgs e)
        {
            // If characters used are not max
            if (_numberOfChars < 2)
            {
                // Increase number of characters used
                _numberOfChars++;

                // Set the number of characters text box
                UpdateCharNumText();
            }// End if
        }// End charPlus_Click()

        private void CharMinus_Click(object sender, RoutedEventArgs e)
        {
            // If characters used are not min
            if (_numberOfChars > 0)
            {
                // Decrease number of characters used
                _numberOfChars--;

                // Set the number of characters text box
                UpdateCharNumText();
            }// End if
        }// End charMinus_Click()

        private void UpdateCharNumText()
        {
            // Check number of characters
            if (_numberOfChars == 0)
            {
                txtCharNum.Text = "6";
            }// End if
            else if (_numberOfChars == 1)
            {
                txtCharNum.Text = "10";
            }// End else if
            else if (_numberOfChars == 2)
            {
                txtCharNum.Text = "20";
            }// End else if
        }// End CharNumberText()
    }// End class
}// End namespace
