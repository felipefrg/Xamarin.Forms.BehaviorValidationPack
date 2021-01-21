using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.BehaviorValidationPack.Behaviors;

namespace Xamarin.Forms.BehaviorValidationPack
{
    public class NumberValidationBehavior : BaseEntryBehavior
    {
        private static Color DefaultColor = Color.Default;

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            entry.Unfocused += Entry_Unfocused;
            DefaultColor = entry.TextColor;
            base.OnAttachedTo(entry);
        }       

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            entry.Unfocused -= Entry_Unfocused;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            int result;
            IsValid = int.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = IsValid ? DefaultColor : ErrorColor;           
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            string textValue = ((Entry)sender).ValidatedText();
            bool isValid = IsRequired ? !string.IsNullOrWhiteSpace(textValue) : true;
            isValid = IsValid && isValid;
            ((Entry)sender).TextColor = IsValid ? DefaultColor : ErrorColor;
        }
    }
}
