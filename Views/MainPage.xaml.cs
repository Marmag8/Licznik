using Licznik.Backend;
using System.Collections;

namespace Licznik.Views
{
    public partial class MainPage : ContentPage
    {
        List<Counter> counters = new List<Counter>();
        

        public MainPage()
        {
            InitializeComponent();
            counters.Add(new Counter(0, "counter1"));
        }

        private void OnPlusBtnClicked(object? sender, EventArgs e)
        {
            Counter counter = counters.Find(x => x.name == "counter1");
            counter.Increment();
            CounterLabel.Text = counter.count.ToString();
        }

        private void OnMinusBtnClicked(object? sender, EventArgs e)
        {
            Counter counter = counters.Find(x => x.name == "counter1");
            counter.Decrement();
            CounterLabel.Text = counter.count.ToString();
        }
    }
}
