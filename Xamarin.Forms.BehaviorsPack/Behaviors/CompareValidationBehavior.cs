using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.BehaviorValidationPack.Behaviors;

namespace Xamarin.Forms.BehaviorValidationPack
{
    public class CompareValidationBehavior : BaseEntryBehavior
    {

        private static Color DefaultColor = Color.Default;

        public static BindableProperty TextProperty =
            BindableProperty.Create<CompareValidationBehavior, string>(tc => tc.Text, string.Empty, BindingMode.TwoWay);

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;
            DefaultColor = bindable.TextColor;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= Bindable_Unfocused;
            base.OnDetachingFrom(bindable);
        }

        void Bindable_Unfocused(object sender, FocusEventArgs e)
        {            
            IsValid = ((Entry)sender).ValidatedText() == Text;
            ((Entry)sender).TextColor = IsValid ? DefaultColor : ErrorColor;
        }
    }
    
}
