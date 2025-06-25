using SkiaSharp;
using Steganographer.Helpers;
using System.Text;

namespace Steganographer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            // Check if file exists

            string filePath = FilePathBox.Text;

            if (!ValidateFilePath(filePath))
                return;

            string msg = MessageBox.Text + ".stego";

            if (string.IsNullOrEmpty(msg))
            {
                System.Windows.Forms.MessageBox.Show("Please provide a message to hide.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> bits = BinaryHelper.GetBits(msg);

            // Show binary representation

            BitsBox.Text = bits[0].ToString();

            for (int i = 1; i < bits.Count; i++)
            {
                if (i % 8 == 0)
                {
                    BitsBox.Text += " ";
                }

                BitsBox.Text += bits[i];
            }

            // Hide bits

            int bitsWritten = 0;

            using (FileStream stream = File.OpenRead(filePath))
            {
                SKBitmap bitmap = SKBitmap.Decode(stream);

                // Check if there are enough pixels

                int width = bitmap.Width;
                int height = bitmap.Height;

                if (width * height < bits.Count)
                {
                    System.Windows.Forms.MessageBox.Show("Not enough pixels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        if (bitsWritten >= bits.Count)
                            break;

                        // Hide the bit in the LSB of the red channel

                        SKColor pixel = bitmap.GetPixel(x, y);
                        SKColor newPixel = pixel.WithRed((byte)((pixel.Red & 0xFE) | bits[bitsWritten++]));
                        bitmap.SetPixel(x, y, newPixel);
                    }
                }

                using (FileStream hiddenStream = File.Open($"{filePath}.stego.png", FileMode.Create))
                {
                    SKData encodedData = bitmap.Encode(SKEncodedImageFormat.Png, 100);

                    encodedData.SaveTo(hiddenStream);
                }

                System.Windows.Forms.MessageBox.Show($"Successfully hidden {bits.Count} bits.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FilePathBox_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Images|*.png",
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FilePathBox.Text = dialog.FileName;
                FilePathBox.SelectionStart = FilePathBox.Text.Length;
                FilePathBox.SelectionLength = 0;
            }
        }

        private void ExtractBtn_Click(object sender, EventArgs e)
        {
            string filePath = FilePathBox.Text;

            if (!ValidateFilePath(filePath))
                return;

            List<int> bits = [];
            List<byte> lastSixByes = [];

            using (FileStream stream = File.Open(filePath, FileMode.Open))
            {
                SKBitmap bitmap = SKBitmap.Decode(stream);

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        // Get LSB

                        SKColor pixel = bitmap.GetPixel(x, y);
                        int lsb = pixel.Red & 1;

                        bits.Add(lsb);

                        if (bits.Count % 8 == 0)
                        {
                            byte b = 0;

                            for (int i = 7; i >= 0; i--)
                            {
                                b |= (byte)(bits[bits.Count - i - 1] << i);
                            }

                            lastSixByes.Add(b);

                            if (lastSixByes.Count == 6)
                            {
                                if (Encoding.UTF8.GetString(lastSixByes.ToArray()) != ".stego")
                                {
                                    lastSixByes.Remove(lastSixByes.First());
                                }
                                else
                                {
                                    // Found the delimiter so we found the whole message, exit
                                    goto Decode;
                                }
                            }
                        }
                    }
                }
            }

            Decode:
            List<byte> bytes = [];

            // - 8 * 6 to exclude the ".stego" delimiter which is 6 bytes long
            for (int i = 0; i < bits.Count - 8 * 6; i += 8)
            {
                byte b = 0;

                for (int x = 0; x < 8; x++)
                {
                    if (i + x > bits.Count - 1)
                        break;

                    b |= (byte)(bits[i + x] << (7 - x));
                }

                bytes.Add(b);
            }

            string msg = Encoding.UTF8.GetString(bytes.ToArray());

            MessageBox.Text = msg;
        }

        private static bool ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath) || Directory.Exists(filePath))
            {
                System.Windows.Forms.MessageBox.Show("Please provide a valid file path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }
    }
}
