using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KokaiZsofi
{
    enum Kategoria
    {
        Dolgozo,
        Vendeg,
        Mozgasserult
    }


    class Jarmu
    {
        private enum recordFields
        {
            entryIndex,
            plate,
            arrival,
            departure,
            category
        }

        private int ParkolasiIdo
        {
            get => parkolasiIdo; set
            {
                parkolasiIdo = value - this.erkezesiIdo;
                if (parkolasiIdo < 0)
                {
                    parkolasiIdo = 0;
                }
            }
        }
        private int erkezesiIdo { get; set; }
        private string rendszam;
        private int parkolasiIdo;

        private Kategoria kategoria { get; set; }
        public string Rendszam
        {
            get => rendszam; set
            {
                if (Regex.Match(value, "/\\w{3}-\\d{3}/").Success)
                {
                    rendszam = value;
                }
            }
        }

        public Jarmu(string mapEntry)
        {
            string[] fields = mapEntry.Split(' ');
            for (int idx = 0; idx < fields.Length; idx++)
            {
                switch ((recordFields)idx)
                {
                    case recordFields.entryIndex:
                        break;
                    case recordFields.plate:
                        this.Rendszam = fields[idx];
                        break;
                    case recordFields.departure:
                        this.ParkolasiIdo = Convert.ToInt32(fields[idx]);
                        break;
                    case recordFields.arrival:
                        this.erkezesiIdo = Convert.ToInt32(fields[idx]);
                        break;
                    case recordFields.category:
                        switch (fields[idx])
                        {
                            case "D": this.kategoria = Kategoria.Dolgozo; break;
                            case "M": this.kategoria = Kategoria.Mozgasserult; break;
                            case "V": this.kategoria = Kategoria.Vendeg; break;

                        }
                        break;
                    default:
                        break;
                }

            }

        }
        public int ParkolasiDij()
        {
            if (
                this.kategoria != Kategoria.Vendeg
                || this.parkolasiIdo < 900
                )
            {
                return 0;
            }
            int hoursToPay = this.parkolasiIdo / (60 * 60) + (this.parkolasiIdo % (60 * 60) > 0 ? 1 : 0);
            return hoursToPay * 500;
        }
        public override string ToString()
        {
            return String.Format(
                @"{0} Érk: {1}, Tot: {2}, Fiz: {3} Ft",
                this.Rendszam,
                this.erkezesiIdo,
                this.parkolasiIdo,
                this.ParkolasiDij()
                );
        }
    }
}
