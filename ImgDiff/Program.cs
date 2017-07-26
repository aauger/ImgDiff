using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImgDiff
{
    class Program
    {
        static void Main(string[] args)
        {
            int diffMult = 20;

            if (args.Length < 3)
                Console.WriteLine("Usage: img1 img2 output [multiplier]");
            if (args.Length == 4)
                diffMult = int.Parse(args[3]);

            Bitmap fInput = (Bitmap)Image.FromFile(args[0]);
            Bitmap sInput = (Bitmap)Image.FromFile(args[1]);
            if (fInput.Width != sInput.Width || fInput.Height != sInput.Height)
                Environment.Exit(1);
            Bitmap result = new Bitmap(fInput.Width, fInput.Height);
            String outputFn = args[2];
            if (!outputFn.ToLower().EndsWith(".png"))
                outputFn += ".png";

            for (int x = 0; x < fInput.Width; x++)
            {
                for (int y = 0; y < fInput.Height; y++)
                {
                    result.SetPixel(x, y, DiffColor(
                        fInput.GetPixel(x, y),
                        sInput.GetPixel(x, y),
                        diffMult));
                }
            }

            result.Save(outputFn, System.Drawing.Imaging.ImageFormat.Png);
        }

        static int RGBClamp(int val)
        {
            if (val < 0)
                return 0;
            if (val > 255)
                return 255;
            return val;
        }

        static Color DiffColor(Color a, Color b, int mult)
        {
            return Color.FromArgb(
                    RGBClamp(Math.Abs(a.R - b.R) * mult),
                    RGBClamp(Math.Abs(a.G - b.G) * mult),
                    RGBClamp(Math.Abs(a.B - b.B) * mult)
                );
        }
    }
}
