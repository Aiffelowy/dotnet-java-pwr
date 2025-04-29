using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImgProcessing
{
    public readonly struct Pixel {
        public readonly int x;
        public readonly int y;
        public readonly Color color;

        public Pixel(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public Pixel clone()
        {
            return new Pixel(this.x, this.y, this.color);
        }

        public Pixel greyscale()
        {
            int gray = (int)(0.3 * this.color.R + 0.59 * this.color.G + 0.11 * this.color.B);
            return new Pixel(this.x, this.y, Color.FromArgb(gray, gray, gray));
        }

        public Pixel transform_color(Color new_color) {
            return new Pixel(this.x, this.y, new_color);
        }

        public Pixel transform(int x, int y)
        {
            return new Pixel(x, y, this.color);
        }

    }


    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public readonly struct BitmapInfo
    {
        public readonly int width;
        public readonly int height; 

        private BitmapInfo(int w, int h) { this.width = w; this.height = h; }

        public static BitmapInfo from_bitmap(in Bitmap bm)
        {
            return new BitmapInfo(bm.Width, bm.Height);
        }
    }
    
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static class Filter
    {
        public static Bitmap apply(Func<Pixel, BitmapInfo, Pixel> filter, Bitmap source) {
            Bitmap result = new Bitmap(source.Width, source.Height);
            BitmapInfo info = BitmapInfo.from_bitmap(source);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color orig = source.GetPixel(x, y);
                    Pixel px = filter(new Pixel(x, y, orig), info);
                    result.SetPixel(px.x, px.y, px.color);
                }
            }

            return result;
        
        }
        public static Pixel grayscale(Pixel orig_pixel, BitmapInfo source_info)
        {
            return orig_pixel.greyscale();
        }

        public static Pixel mirror(Pixel orig_pixel, BitmapInfo source_info)
        {
            int new_x = source_info.width - 1 - orig_pixel.x;
            return orig_pixel.transform(new_x, orig_pixel.y);
        }

        public static Pixel negative(Pixel orig_pixel, BitmapInfo source_info)
        {
            Color inverted = Color.FromArgb(255 - orig_pixel.color.R, 255 - orig_pixel.color.G, 255 - orig_pixel.color.B);
            return orig_pixel.transform_color(inverted);
        }

        public static Bitmap detect_edges(Bitmap source)
        {
            Bitmap grayscale = Filter.apply(Filter.grayscale, source);
            Bitmap result = new Bitmap(source.Width, source.Height);

            int[,] gx = new int[,] {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

            int[,] gy = new int[,] {
            { -1, -2, -1 },
            {  0,  0,  0 },
            {  1,  2,  1 }
        };

            for (int y = 1; y < source.Height - 1; y++)
            {
                for (int x = 1; x < source.Width - 1; x++)
                {
                    int pixelX = 0;
                    int pixelY = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int gray = grayscale.GetPixel(x + j, y + i).R;
                            pixelX += gx[i + 1, j + 1] * gray;
                            pixelY += gy[i + 1, j + 1] * gray;
                        }
                    }

                    int magnitude = (int)Math.Sqrt(pixelX * pixelX + pixelY * pixelY);
                    magnitude = Math.Clamp(magnitude, 0, 255);
                    Color edgeColor = Color.FromArgb(magnitude, magnitude, magnitude);
                    result.SetPixel(x, y, edgeColor);
                }
            }

            return result;
        }
    }
}
