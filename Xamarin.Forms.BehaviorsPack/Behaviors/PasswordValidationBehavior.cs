using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms.BehaviorValidationPack.Behaviors;

namespace Xamarin.Forms.BehaviorValidationPack
{
    //at least 8 characters
    //at least 1 numeric character
    //at least 1 lowercase letter
    //at least 1 uppercase letter
    //at least 1 special character
    public class PasswordValidationBehavior : BaseEntryBehavior
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

        private static Color DefaultColor = Color.Default;

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;
            DefaultColor = bindable.TextColor;
            base.OnAttachedTo(bindable);
        }

        void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            string textValue = ((Entry)sender).ValidatedText();
            bool isValid = IsRequired ? !string.IsNullOrWhiteSpace(textValue) : true;
            IsValid = Validators.PasswordValidator(textValue) && isValid;
            ((Entry)sender).TextColor = IsValid ? DefaultColor : ErrorColor;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= Bindable_Unfocused;
            base.OnDetachingFrom(bindable);
        }
    }
}
