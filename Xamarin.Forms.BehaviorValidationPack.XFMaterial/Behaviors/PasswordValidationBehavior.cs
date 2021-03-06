﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using XF.Material.Forms.UI;

namespace Xamarin.Forms.BehaviorValidationPack.XFMaterial
{ 
    //at least 8 characters
    //at least 1 numeric character
    //at least 1 lowercase letter
    //at least 1 uppercase letter
    //at least 1 special character
public class PasswordValidationBehavior : Behavior<MaterialTextField>
{
    const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";

    private static Color DefaultColor = Color.Default;

    protected override void OnAttachedTo(MaterialTextField bindable)
    {
        bindable.Unfocused += Bindable_Unfocused;
        DefaultColor = bindable.TextColor;
        base.OnAttachedTo(bindable);
    }

    void Bindable_Unfocused(object sender, FocusEventArgs e)
    {
        bool IsValid = false;
        IsValid = Validators.PasswordValidator(((MaterialTextField)sender).ValidatedText());
        ((MaterialTextField)sender).TextColor = IsValid ? DefaultColor : Color.Red;
    }

    protected override void OnDetachingFrom(MaterialTextField bindable)
    {
        bindable.Unfocused -= Bindable_Unfocused;
        base.OnDetachingFrom(bindable);
    }
}
}
