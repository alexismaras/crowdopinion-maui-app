using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdOpinion.Drawables
{
    public class RatioGraphicsView : GraphicsView
    {
        public int Value1
        {
            get => (int)GetValue(Value1Property);
            set => SetValue(Value1Property, value);
        }
        public int Value2
        {
            get => (int)GetValue(Value2Property);
            set => SetValue(Value2Property, value);
        }

        public static readonly BindableProperty Value1Property = BindableProperty.Create(nameof(Value1), typeof(int), typeof(RatioGraphicsView), propertyChanged: Value1PropertyChanged);

        public static readonly BindableProperty Value2Property = BindableProperty.Create(nameof(Value2), typeof(int), typeof(RatioGraphicsView), propertyChanged: Value2PropertyChanged);
        private static void Value1PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Console.WriteLine("XXXXX");
            if (bindable is not RatioGraphicsView { Drawable: RatioBarDrawable drawable } view)
            {
                return;
            }

            drawable.Value1 = (int)newValue;
            view.Invalidate();
        }

        private static void Value2PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not RatioGraphicsView { Drawable: RatioBarDrawable drawable } view)
            {
                return;
            }

            drawable.Value2 = (int)newValue;
            view.Invalidate();
        }
    }
}
