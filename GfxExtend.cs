using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkits.Drawing {
    public static class GfxExtend {

        public static void DrawRoundedRectangle(this Graphics gfx, Pen pen, int x, int y, int width, int height, int radius) {
            GraphicsPath gfxPath = new GraphicsPath();

            //gfx.DrawRectangle(Pens.Red, x, y, width, height);
            /* Left-Top */
            var ltRect = new Rectangle(x, y, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, ltRect);
            gfxPath.AddArc(ltRect, -180, 90);

            ///* Right-Top */
            var rtRect = new Rectangle(x + width - 2 * radius, y, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, rtRect);
            gfxPath.AddArc(rtRect, -90, 90);

            ///* Bottom-Right */
            var brRect = new Rectangle(x + width - 2 * radius, y + height - 2 * radius, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, brRect);
            gfxPath.AddArc(brRect, 0, 90);

            ///* Bottom-Left */
            var blRect = new Rectangle(x, y + height - 2 * radius, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, blRect);
            gfxPath.AddArc(blRect, 90, 90);

            gfxPath.CloseAllFigures();
            gfx.DrawPath(pen, gfxPath);
        }

        public static void FillRoundedRectangle(this Graphics gfx, Brush brush, float x, float y, float width, float height, float radius) {
            GraphicsPath gfxPath = new GraphicsPath();

            //gfx.DrawRectangle(Pens.Red, x, y, width, height);
            /* Left-Top */
            var ltRect = new RectangleF(x, y, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, ltRect);
            gfxPath.AddArc(ltRect, -180, 90);

            ///* Right-Top */
            var rtRect = new RectangleF(x + width - 2 * radius, y, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, rtRect);
            gfxPath.AddArc(rtRect, -90, 90);

            ///* Bottom-Right */
            var brRect = new RectangleF(x + width - 2 * radius, y + height - 2 * radius, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, brRect);
            gfxPath.AddArc(brRect, 0, 90);

            ///* Bottom-Left */
            var blRect = new RectangleF(x, y + height - 2 * radius, 2 * radius, 2 * radius);
            //gfx.DrawRectangle(Pens.Red, blRect);
            gfxPath.AddArc(blRect, 90, 90);

            gfxPath.CloseAllFigures();
            gfx.FillPath(brush, gfxPath);
        }
        
        public static Image Scale(this Image image, int width, int height) {
            if (null != image) {
                try {
                    var bitmap = new Bitmap((int)width, (int)height);
                    bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                    using (var gfx = Graphics.FromImage(bitmap)) {
                        gfx.CompositingMode = CompositingMode.SourceCopy;
                        gfx.CompositingQuality = CompositingQuality.HighQuality;
                        gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        gfx.SmoothingMode = SmoothingMode.HighQuality;
                        gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        var destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                        using (var wrapMode = new ImageAttributes()) {
                            wrapMode.SetWrapMode(WrapMode.Tile);
                            gfx.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                        }
                    }

                    return bitmap;

                } catch (Exception ex) {
                }
            }

            return null;
        }
    }
}
