using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODE_Bagageband.Model
{
	public class Aankomsthal : IObserver<Bagageband>
	{
		// TODO: Hier een ObservableCollection van maken, dan weten we wanneer er vluchten bij de wachtrij bij komen of afgaan.
		public ObservableCollection<Vlucht> WachtendeVluchten { get; private set; }
		public List<Bagageband> Bagagebanden { get; private set; }

		public Aankomsthal()
		{
			WachtendeVluchten = new ObservableCollection<Vlucht>();
			Bagagebanden = new List<Bagageband>();

			Bagagebanden.Add(new Bagageband("Band 1", 30));
			Bagagebanden.Add(new Bagageband("Band 2", 60));
			Bagagebanden.Add(new Bagageband("Band 3", 90));

			foreach (var band in Bagagebanden)
			{
				band.Subscribe(this);
			}
		}

		public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
		{
			Bagageband legeBand = Bagagebanden.FirstOrDefault(b => b.AantalKoffers == 0);
			if (legeBand != null)
			{
				legeBand.HandelNieuweVluchtAf(new Vlucht(vertrokkenVanuit, aantalKoffers));
			}
			else
			{
				WachtendeVluchten.Add(new Vlucht(vertrokkenVanuit, aantalKoffers));
			}
		}

		public void WachtendeVluchtenNaarBand()
		{
			if (WachtendeVluchten.Count() == 0) return;
			Bagageband legeBand = Bagagebanden.FirstOrDefault(bb => bb.AantalKoffers == 0);
			Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault();
			WachtendeVluchten.RemoveAt(0);

			legeBand.HandelNieuweVluchtAf(volgendeVlucht);
			volgendeVlucht.StopWaiting();
		}

		public void OnNext(Bagageband value)
		{
			if (value.AantalKoffers == 0)
			{
				WachtendeVluchtenNaarBand();
			}
		}

		public void OnError(Exception error)
		{
			throw new NotImplementedException();
		}

		public void OnCompleted()
		{
			throw new NotImplementedException();
		}
	}
}
