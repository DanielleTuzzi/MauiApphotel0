using MauiApphotel0.Models;

namespace MauiApphotel0.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current!;

        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;
        pck_quarto.ItemDisplayBinding = new Binding("Descricao");

        dtpck_Checkin.MinimumDate = DateTime.Today;
        dtpck_Checkin.MaximumDate = DateTime.Today.AddMonths(6);

        DateTime dataCheckin = dtpck_Checkin.Date.GetValueOrDefault(DateTime.Today);

        dtpck_checkout.MinimumDate = dataCheckin.AddDays(1);
        dtpck_checkout.MaximumDate = dataCheckin.AddMonths(6);
        dtpck_checkout.Date = dataCheckin.AddDays(1);
    }

    private void dtpck_Checkin_DateSelected(object? sender, DateChangedEventArgs e)
    {
        DateTime novaData = e.NewDate.GetValueOrDefault(DateTime.Today);

        dtpck_checkout.MinimumDate = novaData.AddDays(1);
        dtpck_checkout.MaximumDate = novaData.AddMonths(6);

        DateTime dataCheckout = dtpck_checkout.Date.GetValueOrDefault(DateTime.Today);

        if (dataCheckout <= novaData)
        {
            dtpck_checkout.Date = novaData.AddDays(1);
        }
    }

    private async void BtnSobre_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new Sobre());
    }

    private async void BtnAvancar_Clicked(object? sender, EventArgs e)
    {
        if (pck_quarto.SelectedItem == null)
        {
            await DisplayAlert("Atenção", "Selecione uma suíte.", "OK");
            return;
        }

        Quarto quartoSelecionado = (Quarto)pck_quarto.SelectedItem;

        int adultos = Convert.ToInt32(stp_adultos.Value);
        int criancas = Convert.ToInt32(stp_criancas.Value);

        DateTime checkin = dtpck_Checkin.Date.GetValueOrDefault(DateTime.Today);
        DateTime checkout = dtpck_checkout.Date.GetValueOrDefault(DateTime.Today.AddDays(1));

        int estadia = (checkout - checkin).Days;

        if (estadia <= 0)
        {
            await DisplayAlert("Atenção", "O check-out deve ser depois do check-in.", "OK");
            return;
        }

        double valorTotal =
            ((adultos * quartoSelecionado.ValorDiariaAdulto) +
             (criancas * quartoSelecionado.ValorDiariaCrianca)) * estadia;

        await Navigation.PushAsync(new HospedagemContratada(
            quartoSelecionado.Descricao,
            adultos,
            criancas,
            checkin,
            checkout,
            estadia,
            valorTotal
        ));
    }
}