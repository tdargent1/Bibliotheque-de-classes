using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libGesper
{
    public class Service
    {
        decimal budget;
        int capacite;
        int dernierId;
        string designation;
        int id;
        string produit;
        char type;
        List<Employe> lesEmployesDuService;

        public Service(int id, string designation, char type, string produit, int capacite, decimal budget)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.produit = produit;
            this.capacite = capacite;
            this.budget = budget;
            lesEmployesDuService = new List<Employe>();
        }
        public Service()
        {

        }

        public Service(int id, string designation, char type, decimal budget)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.budget = budget;
            lesEmployesDuService = new List<Employe>();
        }

        public Service(int id, string designation, char type, string produit, int capacite)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.produit = produit;
            this.capacite = capacite;
            lesEmployesDuService = new List<Employe>();
        }

        public string ToString()
        {
            return string.Format("ID: {0}. DESIGNATION: {1}. TYPE: {2}. PRODUIT: {3}. CAPACITE: {4}. BUDGET: {5}. DERNIER ID: {6}.", this.id, this.designation, this.type, this.produit, this.capacite, this.budget, this.dernierId);
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public string Designation
        {
            get
            {
                return this.designation;
            }
        }

        public char Type
        {
            get
            {
                return this.type;
            }
        }

        public string Produit
        {
            get
            {
                return this.produit;
            }
        }

        public int Capacite
        {
            get
            {
                return this.capacite;
            }
        }

        public Decimal Budget
        {
            get
            {
                return this.budget;
            }
        }

        public List<Employe> LesEmployesDuService
        {
            get
            {
                return this.lesEmployesDuService;
            }
        }

        public int DernierId
        {
            get
            {
                return this.dernierId;
            }
        }
    }
}
