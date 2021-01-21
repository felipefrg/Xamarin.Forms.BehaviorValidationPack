using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.BehaviorValidationPack.Behaviors
{
    public class BaseEntryBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty IsValidProperty =
           BindableProperty.Create(nameof(IsValid)
               , typeof(bool)
               , typeof(BaseEntryBehavior)
               , false
               , BindingMode.TwoWay);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public static readonly BindableProperty IsRequiredProperty =
          BindableProperty.Create(nameof(IsRequired)
              , typeof(bool)
              , typeof(BaseEntryBehavior)
              , true
              , BindingMode.TwoWay);

        public bool IsRequired
        {
            get { return (bool)GetValue(IsRequiredProperty); }
            set { SetValue(IsRequiredProperty, value); }
        }

        public static readonly BindableProperty ErrorColorProperty =
          BindableProperty.Create(nameof(ErrorColor)
              , typeof(Color)
              , typeof(BaseEntryBehavior)
              , Color.Red
              , BindingMode.TwoWay);

        public Color ErrorColor
        {
            get { return (Color)GetValue(ErrorColorProperty); }
            set { SetValue(ErrorColorProperty, value); }
        }
    }
}
