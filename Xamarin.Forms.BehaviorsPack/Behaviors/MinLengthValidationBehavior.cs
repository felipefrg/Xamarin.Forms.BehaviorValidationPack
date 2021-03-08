using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.BehaviorValidationPack.Behaviors;

namespace Xamarin.Forms.BehaviorValidationPack
{
    public class MinLengthValidationBehavior : BaseEntryBehavior
    {
        private static Color DefaultColor = Color.Default;
        public static readonly BindableProperty MinLengthProperty = 
            BindableProperty.Create("MinLength", typeof(int), typeof(MinLengthValidationBehavior), 0);

        public int MinLength
        {
            get { return (int)GetValue(MinLengthProperty); }
            set { SetValue(MinLengthProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += bindable_TextChanged;
            bindable.Unfocused += Bindable_Unfocused;
            DefaultColor = bindable.TextColor;
        }

        

        private void bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (e.NewTextValue.Length >= MinLength);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= bindable_TextChanged;
            bindable.Unfocused -= Bindable_Unfocused;

        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            string text = ((Entry)sender).ValidatedText();
            bool isValid = text.Length >= MinLength;
            //IsValid = IsValid && isValid;
            IsValid = isValid;
            ((Entry)sender).TextColor = IsValid ? DefaultColor : ErrorColor;
        }

        new private bool IsRequired = false;        
        new private BindableObject IsRequiredProperty = null;

    }
}
