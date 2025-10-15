namespace Licznik.Views;

public partial class AddCounter : ContentPage
{
    public Action<string, int>? OnCounterAdded;
    public AddCounter()
    {
        InitializeComponent();
    }

    private async void OnConfirm(object sender, EventArgs e)
    {
        string name = Name.Text ?? "";
        int initialValue = int.TryParse(InitialValue.Text, out int val) ? val : 0;
        //int slider1 = R.Value;
        //int slider2 = G.Value;
        //int slider3 = B.Value;

        OnCounterAdded?.Invoke(name, initialValue);

        await Navigation.PopModalAsync();
    }
}