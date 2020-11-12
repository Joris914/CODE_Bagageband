using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CODE_Bagageband.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace CODE_Bagageband.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// <para>
	/// You can also use Blend to data bind with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class MainViewModel : ViewModelBase
	{
		#region Properties to bind to
		private string _nieuweVluchtVanaf;
		public string NieuweVluchtVanaf
		{
			get { return _nieuweVluchtVanaf; }
			set { _nieuweVluchtVanaf = value; RaisePropertyChanged("NieuweVluchtVanaf"); }
		}

		private int _nieuweVluchtAantalKoffers;
		public int NieuweVluchtAantalKoffers
		{
			get { return _nieuweVluchtAantalKoffers; }
			set { _nieuweVluchtAantalKoffers = value; RaisePropertyChanged("NieuweVluchtAantalKoffers"); }
		}

		public BagagebandViewModel Band1 { get; set; }
		public BagagebandViewModel Band2 { get; set; }
		public BagagebandViewModel Band3 { get; set; }
		public RelayCommand NieuweVluchtCommand { get; set; }

		public ObservableCollection<VluchtViewModel> WachtendeVluchten { get; set; }
		#endregion Properties to bind to

		private Aankomsthal _aankomsthal;

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public MainViewModel(Aankomsthal aankomsthal)
		{
			NieuweVluchtCommand = new RelayCommand(AddNieuweVlucht);
			WachtendeVluchten = new ObservableCollection<VluchtViewModel>();

			NieuweVluchtAantalKoffers = 5;

			_aankomsthal = aankomsthal;
			// TODO: Hier kijken naar _aankomsthal.WachtendeVluchten.CollectionChanged en verversWachtendeVluchten weghalen.
			_aankomsthal.WachtendeVluchten.CollectionChanged += new NotifyCollectionChangedEventHandler(VerversWachtendeVluchten);

			Band1 = new BagagebandViewModel(_aankomsthal.Bagagebanden[0]);
			Band2 = new BagagebandViewModel(_aankomsthal.Bagagebanden[1]);
			Band3 = new BagagebandViewModel(_aankomsthal.Bagagebanden[2]);

			InitializeDefaultVluchten();
		}

		private void InitializeDefaultVluchten()
		{
			_aankomsthal.NieuweInkomendeVlucht("New York", 7);
			_aankomsthal.NieuweInkomendeVlucht("Paris", 2);
			_aankomsthal.NieuweInkomendeVlucht("Beijing", 8);
			_aankomsthal.NieuweInkomendeVlucht("London", 6);
			_aankomsthal.NieuweInkomendeVlucht("Barcelona", 4);
			_aankomsthal.NieuweInkomendeVlucht("Sydney", 9);
			_aankomsthal.NieuweInkomendeVlucht("Moskow", 1);
			_aankomsthal.NieuweInkomendeVlucht("Rio de Janeiro", 9);
			_aankomsthal.NieuweInkomendeVlucht("Cape Town", 7);
			_aankomsthal.NieuweInkomendeVlucht("Tokyo", 3);
		}

		private void VerversWachtendeVluchten(object sender, NotifyCollectionChangedEventArgs e)
		{
			// Code snippet bij CollectionChanged:

			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				WachtendeVluchten.Add(new VluchtViewModel(e.NewItems[0] as Vlucht));
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				WachtendeVluchten.RemoveAt(e.OldStartingIndex);
			}
		}

		private void AddNieuweVlucht()
		{
			if (!String.IsNullOrWhiteSpace(NieuweVluchtVanaf))
			{
				_aankomsthal.NieuweInkomendeVlucht(NieuweVluchtVanaf, NieuweVluchtAantalKoffers);

				NieuweVluchtAantalKoffers = 5;
				NieuweVluchtVanaf = null;
			}
		}
	}
}
