﻿using Xamarin.Forms.BehaviorValidationPack.Behaviors;

namespace Xamarin.Forms.BehaviorValidationPack
{
    public class CNPJValidationBehavior : BaseEntryBehavior
    {

        private static Color DefaultColor = Color.Default;

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
            string textValue = ((Entry)sender).ValidatedText();
            bool isValid = IsRequired ? !string.IsNullOrWhiteSpace(textValue) : true;
            IsValid = Validators.CnpjValidator(textValue) && isValid;
            ((Entry)sender).TextColor = IsValid ? DefaultColor : ErrorColor;
        }
    }
}
