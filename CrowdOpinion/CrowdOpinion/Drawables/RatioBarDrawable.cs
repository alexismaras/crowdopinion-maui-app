using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdOpinion.Drawables
{
    public class RatioBarDrawable : IDrawable
    {

        public int Value1 { get; set; } = 0;
        public int Value2 { get; set; } = 0;
        public Color Color1 { get; set; } = Colors.Red;
        public Color Color2 { get; set; } = Color.FromRgba(255, 0, 150, 255);
        public Color Color3 { get; set; } = Color.FromRgba(150, 0, 255, 255);
        public Color Color4 { get; set; } = Colors.Blue;
        public float CornerRadius { get; set; } = 0f;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            int a = Value1;
            int b = Value2;
            int total = a + b;
            if (total <= 0)
            {
                a = 1;
                b = 1;
                total = 2;
            }

            float point1 = a / (float)total - 0.1f;
            float point2 = a / (float)total + 0.1f;

            var gradient1 = new LinearGradientPaint
            {
                GradientStops =
                [
                    new PaintGradientStop(0, Color1),
                    new PaintGradientStop(point1, Color2),
                    new PaintGradientStop(point2, Color3),
                    new PaintGradientStop(1, Color4)
                ],
            };


            var width1 = (float)((float)a / total * dirtyRect.Width);
            var width2 = (float)((float)b / total * dirtyRect.Width);

            // Draw first value
            canvas.SetFillPaint(gradient1, new RectF(0, 0, dirtyRect.Width, dirtyRect.Height));
            canvas.FillRoundedRectangle(0, 0, dirtyRect.Width, dirtyRect.Height, CornerRadius);

            //// Draw second value
            //canvas.SetFillPaint(gradient2, new RectF(width1, 0, width2, dirtyRect.Height));
            //canvas.FillRoundedRectangle(width1, 0, width2, dirtyRect.Height, CornerRadius);
        }
    }
}

