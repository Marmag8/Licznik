using Licznik.Backend;
using System.Collections.Generic;

namespace Licznik.Views
{
    public partial class MainPage : ContentPage
    {
        readonly List<Counter> counters = new List<Counter>();
        int counterIndex = 1;

        public MainPage()
        {
            InitializeComponent();
            AddCounter("counter1", 0);
        }

        private async void OnAddCounterClicked(object sender, EventArgs e)
        {
            counterIndex++;
            string name = $"counter{counterIndex}";

            string result = await DisplayPromptAsync(
                "Nowy licznik",
                $"Podaj wartość początkową dla licznika {name}:",
                "OK",
                "Anuluj",
                "0",
                keyboard: Keyboard.Numeric);

            if (int.TryParse(result, out int startValue)) AddCounter(name, startValue);
            else AddCounter(name, 0);
        }

        private void AddCounter(string name, int startValue)
        {
            var counter = new Counter(startValue, name);
            counters.Add(counter);

            var label = new Label
            {
                Text = $"{name}: {counter.count}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };

            var plusBtn = new Button
            {
                Text = "+",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5, 5, 5, 5)
            };

            plusBtn.Clicked += (s, e) =>
            {
                counter.Increment();
                label.Text = $"{name}: {counter.count}";
            };

            var minusBtn = new Button
            {
                Text = "-",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5, 5, 5, 5)
            };
            minusBtn.Clicked += (s, e) =>
            {
                counter.Decrement();
                label.Text = $"{name}: {counter.count}";
            };

            HorizontalStackLayout btns = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = { plusBtn, minusBtn }
            };

            CountersLayout.Children.Add(label);
            CountersLayout.Children.Add(btns);
        }
    }
}