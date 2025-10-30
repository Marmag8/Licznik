namespace Licznik.Views;

public partial class AddCounter : ContentPage
{
    public Action<string, int, int, int, int>? OnCounterAdded;
    public AddCounter()
    {
        InitializeComponent();
    }

    private void OnRGBValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (sender is Slider slider)
        {
            string labelName = null;

            if (ReferenceEquals(slider, R))
                labelName = "RValue";
            else if (ReferenceEquals(slider, G))
                labelName = "GValue";
            else if (ReferenceEquals(slider, B))
                labelName = "BValue";

            if (labelName != null)
            {
                Label label = this.FindByName<Label>(labelName);
                if (label != null)
                    label.Text = ((int)e.NewValue).ToString();
            }
        }
    }

    private async void OnConfirm(object sender, EventArgs e)
    {
        string name = Name.Text ?? "";
        int initialValue = int.TryParse(InitialValue.Text, out int val) ? val : 0;
        int r = (int)R.Value;
        int g = (int)G.Value;
        int b = (int)B.Value;

        OnCounterAdded?.Invoke(name, initialValue, r, g, b);

        await Navigation.PopModalAsync();
    }
}