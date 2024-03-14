using System.Drawing.Imaging;

namespace Downsizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnDownscale_Click(object sender, EventArgs e)
        {
            double scaleFactor = double.Parse(txtScale.Text) / 100.0;
            Bitmap originalImage = new Bitmap(txtFilePath.Text);
            Bitmap downscaledImage = DownscaleImage(originalImage, scaleFactor);
            //Bitmap downscaledImage = DownscaleImageThreaded(originalImage, scaleFactor);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string savePath = Path.Combine(desktopPath, "downscaledImage.png");
            downscaledImage.Save(savePath, ImageFormat.Png);

            MessageBox.Show($"Image saved to {savePath}");
        }

        private Bitmap DownscaleImage(Bitmap originalImage, double scaleFactor)
        {
            int newWidth = (int)(originalImage.Width * scaleFactor);
            int newHeight = (int)(originalImage.Height * scaleFactor);
            Bitmap newImage = new Bitmap(newWidth, newHeight, originalImage.PixelFormat);

            BitmapData originalData = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height), ImageLockMode.ReadOnly, originalImage.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(originalImage.PixelFormat) / 8;

            byte[] newImageData = new byte[newWidth * newHeight * bytesPerPixel];

            unsafe
            {
                byte* originalPtr = (byte*)originalData.Scan0;

                byte* newImageDataPtr;
                fixed (byte* ptr = newImageData)
                {
                    newImageDataPtr = ptr;
                }

                for (int y = 0; y < newHeight; y++)
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        int startX = x * originalImage.Width / newWidth;
                        int startY = y * originalImage.Height / newHeight;
                        int endX = (x + 1) * originalImage.Width / newWidth;
                        int endY = (y + 1) * originalImage.Height / newHeight;

                        int red = 0;
                        int green = 0;
                        int blue = 0;
                        int pixelCount = 0;

                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                byte* pixel = originalPtr + (j * originalData.Stride) + (i * bytesPerPixel);
                                blue += pixel[0];
                                green += pixel[1];
                                red += pixel[2];
                                pixelCount++;
                            }
                        }

                        byte* newPixel = newImageDataPtr + ((y * newWidth + x) * bytesPerPixel);
                        newPixel[0] = (byte)(blue / pixelCount);
                        newPixel[1] = (byte)(green / pixelCount);
                        newPixel[2] = (byte)(red / pixelCount);
                    }
                }
            }

            originalImage.UnlockBits(originalData);

            BitmapData newData = newImage.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, newImage.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(newImageData, 0, newData.Scan0, newImageData.Length);
            newImage.UnlockBits(newData);

            return newImage;
        }

        private Bitmap DownscaleImageThreaded(Bitmap originalImage, double scaleFactor)
        {
            int newWidth = (int)(originalImage.Width * scaleFactor);
            int newHeight = (int)(originalImage.Height * scaleFactor);
            Bitmap newImage = new Bitmap(newWidth, newHeight, originalImage.PixelFormat);

            BitmapData originalData = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height), ImageLockMode.ReadOnly, originalImage.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(originalImage.PixelFormat) / 8;

            byte[] newImageData = new byte[newWidth * newHeight * bytesPerPixel];

            unsafe
            {
                byte* originalPtr = (byte*)originalData.Scan0;

                byte* newImageDataPtr;
                fixed (byte* ptr = newImageData)
                {
                    newImageDataPtr = ptr;
                }
                Parallel.For(0, newHeight, y =>
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        int startX = x * originalImage.Width / newWidth;
                        int startY = y * originalImage.Height / newHeight;
                        int endX = (x + 1) * originalImage.Width / newWidth;
                        int endY = (y + 1) * originalImage.Height / newHeight;

                        int red = 0;
                        int green = 0;
                        int blue = 0;
                        int pixelCount = 0;

                        for (int i = startX; i < endX; i++)
                        {
                            for (int j = startY; j < endY; j++)
                            {
                                byte* pixel = originalPtr + (j * originalData.Stride) + (i * bytesPerPixel);
                                blue += pixel[0];
                                green += pixel[1];
                                red += pixel[2];
                                pixelCount++;
                            }
                        }

                        byte* newPixel = newImageDataPtr + ((y * newWidth + x) * bytesPerPixel);
                        newPixel[0] = (byte)(blue / pixelCount);
                        newPixel[1] = (byte)(green / pixelCount);
                        newPixel[2] = (byte)(red / pixelCount);
                    }
                });
            }

            originalImage.UnlockBits(originalData);

            BitmapData newData = newImage.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, newImage.PixelFormat);
            System.Runtime.InteropServices.Marshal.Copy(newImageData, 0, newData.Scan0, newImageData.Length);
            newImage.UnlockBits(newData);

            return newImage;
        }


    }
}
