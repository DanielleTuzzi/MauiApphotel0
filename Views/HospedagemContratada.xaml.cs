namespace MauiApphotel0.Views;

public partial class HospedagemContratada : ContentPage
{
    public HospedagemContratada(
        string suite,
        int adultos,
        int criancas,
        DateTime checkin,
        DateTime checkout,
        int estadia,
        double valorTotal)
    {
        InitializeComponent();

        lbl_suite.Text = suite;
        lbl_adultos.Text = adultos.ToString();
        lbl_criancas.Text = criancas.ToString();
        lbl_checkin.Text = checkin.ToString("dd/MM/yyyy");
        lbl_checkout.Text = checkout.ToString("dd/MM/yyyy");
        lbl_estadia.Text = estadia + " dias";
        lbl_valor_total.Text = valorTotal.ToString("C2");
    }

    private async void Button_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}