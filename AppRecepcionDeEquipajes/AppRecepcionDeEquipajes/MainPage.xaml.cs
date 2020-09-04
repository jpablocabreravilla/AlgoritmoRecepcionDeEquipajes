using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppRecepcionDeEquipajes
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			lbl_ContadorBodega.Text = Convert.ToString(CapacidadBodega) + " Kg ";
		}

		double CapacidadBodega = 18000, ContadorPeso = 0;
		double BanderPesoBultoPesado = 0, BanderPesoBultoLiviano = 9999, PesoPromBultos = 0, TotalEnPesos = 0, TotalEnDolares = 0;
		int ContadorBultosIngresados = 0;

		private void Button_Clicked_clear(object sender, EventArgs e)
		{
			CapacidadBodega = 18000; ContadorPeso = 0; BanderPesoBultoPesado = 0; BanderPesoBultoLiviano = 9999;
			PesoPromBultos = 0; TotalEnPesos = 0; TotalEnDolares = 0; ContadorBultosIngresados = 0;
			lbl_ContBultos.Text = string.Empty;
			lbl_BultoPesas.Text = string.Empty;
			lbl_BultoLiviano.Text = string.Empty;
			lbl_PromBultos.Text = string.Empty;
			lbl_TotalPesos.Text = string.Empty;
			lbl_TotalDolares.Text = string.Empty;
		}

		private void btn_IngresarBulto_Clicked(object sender, EventArgs e)
		{
			if (entry_Bulto.Text == null || entry_Bulto.Text == string.Empty)
			{
				entry_Bulto.Text = Convert.ToString(0);
			}

			double EntryVlrBultoIn = Convert.ToDouble(entry_Bulto.Text);
			bool validationIn = validacionesIn(EntryVlrBultoIn);

			if (validationIn == true)
			{
				ControlPesoBodega(EntryVlrBultoIn);
				double VlrBulto = CalcularPrecioBultoIndependiente(EntryVlrBultoIn);
				lbl_PrecioBultoIn.Text = Convert.ToString(VlrBulto) + " $ ";
				ContadorVultos();
				BultoMasPesadoYliviano(EntryVlrBultoIn);
				promBultos();
				ConvertPssDll();
			}
		}

		private void btn_FinalizarCarga_Clicked(object sender, EventArgs e)
		{
			lbl_ContBultos.Text = Convert.ToString(ContadorBultosIngresados);
			lbl_BultoPesas.Text = Convert.ToString(BanderPesoBultoPesado);
			lbl_BultoLiviano.Text = Convert.ToString(BanderPesoBultoLiviano);
			lbl_PromBultos.Text = Convert.ToString(PesoPromBultos);
			lbl_TotalPesos.Text = Convert.ToString(ContadorPeso);
			lbl_TotalDolares.Text = Convert.ToString(TotalEnDolares);
		}

		private bool validacionesIn(double BultoIn)
		{
			if (BultoIn > 500)
			{
				DisplayAlert("Accion imposible de realizar", "El limite maximo de peso por bulto es de 500 kg", "Aceptar");
				entry_Bulto.Text = string.Empty;
				entry_Bulto.Focus();
				return false;
			}
			if (BultoIn <= 0)
			{
				DisplayAlert("Accion imposible de realizar", "El peso debe estar entre 0 y 500 kg", "Aceptar");
				entry_Bulto.Text = string.Empty;
				entry_Bulto.Focus();
				return false;
			}
			return true;
		}

		private void ControlPesoBodega(double bulto)
		{
			double contadorpesobulto = ContadorPeso + bulto;

			if (CapacidadBodega >= contadorpesobulto)
			{
				ContadorPeso = ContadorPeso + bulto;

				lbl_ContadorBodega.Text = Convert.ToString(CapacidadBodega - ContadorPeso);
				entry_Bulto.Text = string.Empty;
				entry_Bulto.Focus();
			}
			else
			{
				DisplayAlert("Accion imposible de realizar", "este bulto ya no cabe en la bodega", "Aceptar");
			}
		}

		private double CalcularPrecioBultoIndependiente(double bulto)
		{
			double ValorBulto;
			if (bulto <= 25)
			{
				ValorBulto = 0;
				return ValorBulto;
			}
			if (bulto > 25 && bulto <= 300)
			{
				ValorBulto = 1500;
				return ValorBulto;
			}
			if (bulto > 300 && bulto <= 500)
			{
				ValorBulto = 2500;
				return ValorBulto;
			}

			return ValorBulto = 0;
		}

		private void ContadorVultos()
		{
			ContadorBultosIngresados++;
		}

		private void BultoMasPesadoYliviano(double bulto)
		{

			if (BanderPesoBultoPesado < bulto)
			{
				BanderPesoBultoPesado = bulto;
			}


			if (BanderPesoBultoLiviano >= bulto)
			{
				BanderPesoBultoLiviano = bulto;
			}
		}

		private void promBultos()
		{
			PesoPromBultos = ContadorPeso / ContadorBultosIngresados;
		}

		private void ConvertPssDll()
		{
			TotalEnDolares = Math.Round((ContadorPeso / 3690.19), (2)); // Dolar al dia de hoy 4 sep 2020
		}

	}
}
