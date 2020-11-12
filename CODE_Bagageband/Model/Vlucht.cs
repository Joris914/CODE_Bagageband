using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DPINT_Wk3_Observer.Model;

namespace CODE_Bagageband.Model
{
    public class Vlucht : Observable<Vlucht>
    {
        public Vlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            VertrokkenVanuit = vertrokkenVanuit;
            AantalKoffers = aantalKoffers;
            _waitingTimer = new Timer();
            _waitingTimer.Interval = 1000;
            _waitingTimer.Tick += (sender, args) => TimeWaiting = TimeWaiting.Add(new TimeSpan(0, 0, 1));
            _waitingTimer.Tick += (sender, args) => Notify(this);
            _waitingTimer.Start();
        }

        private string _vertrokkenVanuit;
        public string VertrokkenVanuit
        {
            get { return _vertrokkenVanuit; }
            set { _vertrokkenVanuit = value; } // TODO: Kunnen we hier straks net zoiets doen als RaisePropertyChanged?
        }

        private int _aantalKoffers;
        public int AantalKoffers
        {
            get { return _aantalKoffers; }
            set { _aantalKoffers = value; } // TODO: Kunnen we hier straks net zoiets doen als RaisePropertyChanged?
        }

        private Timer _waitingTimer;
        public TimeSpan TimeWaiting { get; set; }

        public void StopWaiting()
		{
            _waitingTimer.Stop();
            _waitingTimer.Dispose();
		}
    }
}
