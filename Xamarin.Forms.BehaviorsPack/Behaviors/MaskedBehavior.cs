using System.Collections.Generic;
using Xamarin.Forms.BehaviorValidationPack.Behaviors;

namespace Xamarin.Forms.BehaviorValidationPack
{
    public class MaskedBehavior : BaseEntryBehavior
    {
        public static readonly BindableProperty ClearIfUnmatchProperty =
           BindableProperty.Create(nameof(ClearIfUnmatch), typeof(bool), typeof(MaskedBehavior), false);

        public bool ClearIfUnmatch
        {
            get { return (bool)GetValue(ClearIfUnmatchProperty); }
            set { SetValue(ClearIfUnmatchProperty, value); }
        }

        private string _mask = "";
        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                SetPositions();
            }
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            entry.Unfocused += Entry_Unfocused;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            entry.Unfocused -= Entry_Unfocused;
            base.OnDetachingFrom(entry);
        }

        IDictionary<int, char> _positions;

        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);

            _positions = list;
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            if (ClearIfUnmatch)
            {
                string text = ((Entry)sender).ValidatedText();
                if (text.Length != Mask.Length)
                {
                    ((Entry)sender).Text = string.Empty;
                }
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = sender as Entry;

            var text = entry.Text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return;

            if (text.Length > _mask.Length)
            {
                entry.Text = text.Remove(text.Length - 1);
                return;
            }

            foreach (var position in _positions)
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }

            if (entry.Text != text)
                entry.Text = text;
        }
    }
}
