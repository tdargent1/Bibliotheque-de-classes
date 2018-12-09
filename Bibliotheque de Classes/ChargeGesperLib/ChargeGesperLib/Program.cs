using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using libGesper;

namespace ChargeGesperLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Donnees lesDonnees = new Donnees();
            lesDonnees.ToutCharger();
            lesDonnees.AfficherDiplomes();
            Console.WriteLine("");
            lesDonnees.AfficherEmployes();
            Console.WriteLine("");
            lesDonnees.AfficherServices();
            Console.WriteLine("");
            lesDonnees.AjouterService(1, "Atelier D", 'P', "Lit", 4000, 2500);
            lesDonnees.AfficherServices();
            lesDonnees.Sauvegarder();

            Console.ReadLine();

        }
    }
}
