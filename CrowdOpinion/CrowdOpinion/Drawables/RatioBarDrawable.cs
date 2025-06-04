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
        public Color Color2 { get; set; } = Colors.Blue;
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

            var width1 = (float)((float)a / total * dirtyRect.Width);
            var width2 = (float)((float)b / total * dirtyRect.Width);

            // Draw first value
            canvas.FillColor = Color1;
            canvas.FillRoundedRectangle(0, 0, width1, dirtyRect.Height, CornerRadius);

            // Draw second value
            canvas.FillColor = Color2;
            canvas.FillRoundedRectangle(width1, 0, width2, dirtyRect.Height, CornerRadius);
        }
    }
}

