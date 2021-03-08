using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.BehaviorValidationPack.Behaviors;

namespace Xamarin.Forms.BehaviorValidationPack
{
    public class DateStringValidationBehavior : BaseEntryBehavior
    {
        private static Color DefaultColor = Color.Default;

        protected override void OnAttachedTo(Entry datepicker)
        {
            datepicker.Unfocused += Datepicker_Unfocused;
            DefaultColor = datepicker.TextColor;
            base.OnAttachedTo(datepicker);
        }

        protected override void OnDetachingFrom(Entry datepicker)
        {
            datepicker.Unfocused += Datepicker_Unfocused;
            base.OnDetachingFrom(datepicker);
        }

        private void Datepicker_Unfocused(object sender, FocusEventArgs e)
        {
            string textValue = ((Entry)sender).ValidatedText();
            bool isValid = IsRequired ? !string.IsNullOrWhiteSpace(textValue) : true;
            IsValid = Validators.DateStringValidator(textValue) && isValid;
            ((Entry)sender).TextColor = IsValid ? DefaultColor : ErrorColor;      
        }        
    }
}
