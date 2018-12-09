using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libGesper
{
    public class Employe
    {
        byte cadre;
        int id;
        string nom;
        string prenom;
        decimal salaire;
        string sexe;
        List<Diplome> lesDiplomes;
        Service leService;

        public Employe(int id, string nom, string prenom, string sexe, byte cadre, decimal salaire, Service leService)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.sexe = sexe;
            this.cadre = cadre;
            this.salaire = salaire;
            this.leService = leService;
            lesDiplomes = new List<Diplome>();
        }

        public string ToString()
        {
            return string.Format("ID: {0}. NOM: {1}. PRENOM: {2}. SEXE: {3}. CADRE: {4}. SALAIRE: {5}.", this.id, this.nom, this.prenom, this.sexe, this.cadre, this.salaire);
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }
        }

        public string Prenom
        {
            get
            {
                return this.prenom;
            }
        }

        public string Sexe
        {
            get
            {
                return this.sexe;
            }
        }

        public byte Cadre
        {
            get
            {
                return this.cadre;
            }
        }

        public decimal Salaire
        {
            get
            {
                return this.salaire;
            }
        }

        public List<Diplome> Diplomes
        {
            get
            {
                return this.lesDiplomes;
            }
        }

        public Service Service
        {
            get
            {
                return this.leService;
            }
        }
    }
}
