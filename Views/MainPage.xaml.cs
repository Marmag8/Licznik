using Licznik.Backend;
using System.Collections.Generic;
using System.Linq;

namespace Licznik.Views
{
    public partial class MainPage : ContentPage
    {
        readonly List<Counter> counters = [];
        int counterIndex = 1;

        public MainPage()
        {
            InitializeComponent();
            (List<Counter> counters, int index) res = Utils.FromXML();
            counters = res.counters;
            counterIndex = res.index;

            foreach (var counter in counters.ToList())
                AddCounterToUI(counter);
        }

        private void OnCounterChanged()
        {
            Utils.ToXML(counters, counterIndex);
        }
        
        private async void OnAddCounterClicked(object sender, EventArgs e)
        {
            AddCounter addCounterPage = new AddCounter
            {
                OnCounterAdded = (name, initialValue, r, g, b) =>
                {
                    if (String.IsNullOrEmpty(name.Trim()))
                    {
                        counterIndex++;
                        name = $"counter{counterIndex}";
                    }
                    Counter counter = new Counter(initialValue, name, r, g, b);
                    counters.Add(counter);
                    AddCounterToUI(counter);
                    OnCounterChanged();
                }
            };
            await Navigation.PushModalAsync(addCounterPage);
        }

        private void AddCounterToUI(Counter counter)
        {
            Color color = Color.FromRgb(counter.r, counter.g, counter.b);
            Label label = new Label
            {
                Text = $"{counter.name}: {counter.count}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20, 0, 0),
                TextColor = color
            };

            Button plusBtn = new Button
            {
                Text = "+",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5, 5, 5, 5),
                BackgroundColor = color
            };

            plusBtn.Clicked += (s, e) =>
            {
                counter.Increment();
                label.Text = $"{counter.name}: {counter.count}";
                OnCounterChanged();
            };

            Button minusBtn = new Button
            {
                Text = "-",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5, 5, 5, 5),
                BackgroundColor = color
            };

            minusBtn.Clicked += (s, e) =>
            {
                counter.Decrement();
                label.Text = $"{counter.name}: {counter.count}";
                OnCounterChanged();
            };

            Button resetBtn = new Button
            {
                Text = "Resetuj",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5, 5, 5, 5),
                BackgroundColor = color
            };

            resetBtn.Clicked += (s, e) =>
            {
                counter.count = counter.initialCount;
                label.Text = $"{counter.name}: {counter.count}";
                OnCounterChanged();
            };

            Button deleteBtn = new Button
            {
                Text = "Usuń",
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5, 5, 5, 5),
                BackgroundColor = color
            };

            HorizontalStackLayout btns = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = { plusBtn, minusBtn, resetBtn, deleteBtn }
            };

            deleteBtn.Clicked += (s, e) =>
            {
                counters.Remove(counter);
                CountersLayout.Children.Remove(label);
                CountersLayout.Children.Remove(btns);
                OnCounterChanged();
            };

            CountersLayout.Children.Add(label);
            CountersLayout.Children.Add(btns);
        }
    }
}