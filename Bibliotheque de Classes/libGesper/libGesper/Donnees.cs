using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace libGesper
{
    public class Donnees
    {
        private MySqlConnection cnx;
        List<Diplome> lesDiplomes;
        List<Employe> lesEmployes;
        List<Service> lesServices;

        public Donnees()
        {
            this.cnx = Connection.GetConnection();
            lesDiplomes = new List<Diplome>();
            lesEmployes = new List<Employe>();
            lesServices = new List<Service>();
        }

        public void AfficherDiplomes()
        {
            for (int i = 0; i < lesDiplomes.Count; i++)
            {
                Console.WriteLine(lesDiplomes[i].ToString());
            }
        }
        public void AfficherEmployes()
        {

            for (int i = 0; i < lesEmployes.Count; i++)
            {
                Console.WriteLine(lesEmployes[i].ToString());
            }
        }
        public void AfficherServices()
        {
            for (int i = 0; i < lesServices.Count; i++)
            {
                Console.WriteLine(lesServices[i].ToString());
            }
        }

        public void ChargerDiplomes()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "SELECT * FROM diplome";
            cmdSql.CommandType = CommandType.Text;
            this.cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                Diplome dip = new Diplome(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]));
                lesDiplomes.Add(dip);
            }
            this.cnx.Close();
        }

        public void ChargerEmployes()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "SELECT * FROM employe";
            cmdSql.CommandType = CommandType.Text;
            this.cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                Service ser1 = null;
                for (int i = 0; i < lesServices.Count; i++)
                {
                    if (lesServices[i].Id == Convert.ToInt32(reader[6])){
                        ser1 = lesServices[i];
                    }
                }
                Employe emp = new Employe(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2]), Convert.ToString(reader[3]), Convert.ToByte(reader[4]), Convert.ToDecimal(reader[5]), ser1);
                lesEmployes.Add(emp);
            }
            this.cnx.Close();
        }

        public void ChargerServices()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            cmdSql.CommandText = "SELECT * FROM service";
            cmdSql.CommandType = CommandType.Text;
            this.cnx.Open();
            MySqlDataReader reader = cmdSql.ExecuteReader();
            while (reader.Read())
            {
                if ((string)reader[2] == "P")
                {
                    Service ser = new Service(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToChar(reader[2]), Convert.ToString(reader[3]), Convert.ToInt32(reader[4]));
                    lesServices.Add(ser);
                }
                if ((string)reader[2] == "A")
                {
                    Service ser = new Service(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToChar(reader[2]), Convert.ToDecimal(reader[5]));
                    lesServices.Add(ser);
                }
            }
            this.cnx.Close();
        }

        public void ChargerLesDiplomesDesEmployes()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            MySqlDataReader reader;
            this.cnx.Open();
            Diplome unDiplome;
            int i;
            foreach (Employe emp in lesEmployes)
            {
                cmdSql.CommandText = "SELECT * FROM posseder WHERE pos_employe = " + emp.Id + ";";
                cmdSql.CommandType = CommandType.Text;
                reader = cmdSql.ExecuteReader();
                while (reader.Read())
                {
                    i = 0;
                    unDiplome = null;
                    while (i < lesDiplomes.Count && lesDiplomes[i].Id != Convert.ToInt32(reader[0]))
                    {
                        i++;
                    }
                    if (i < lesDiplomes.Count && lesDiplomes[i].Id == Convert.ToInt32(reader[0]))
                    {
                        unDiplome = lesDiplomes[i];
                        emp.Diplomes.Add(unDiplome);
                    }
                }
                reader.Close();
            }
            this.cnx.Close();
        }

        public void ChargerLesEmployesDesServices()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            MySqlDataReader reader;
            int i;
            Employe unEmploye;
            this.cnx.Open();
            foreach (Service serv in lesServices)
            {
                cmdSql.CommandText = "SELECT * FROM employe WHERE emp_service = " + serv.Id + ";";
                cmdSql.CommandType = CommandType.Text;
                reader = cmdSql.ExecuteReader();
                while (reader.Read())
                {
                    i = 0;
                    unEmploye = null;
                    while (i < lesEmployes.Count && lesEmployes[i].Id != Convert.ToInt32(reader[0]))
                    {
                        i++;
                    }
                    if (i < lesEmployes.Count && lesEmployes[i].Id == Convert.ToInt32(reader[0]))
                    {
                        unEmploye = lesEmployes[i];
                    }
                    serv.LesEmployesDuService.Add(unEmploye);
                }
                reader.Close();
            }
            this.cnx.Close();
        }

        public void ChargerLesEmployesTitulaireDesDiplomes()
        {
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            MySqlDataReader reader;
            Employe unEmploye;
            int i;
            this.cnx.Open();
            foreach (Diplome dip in lesDiplomes)
            {
                cmdSql.CommandText = "SELECT * FROM posseder WHERE pos_diplome = " + dip.Id + ";";
                cmdSql.CommandType = CommandType.Text;
                reader = cmdSql.ExecuteReader();
                while (reader.Read())
                {
                    i = 0;
                    unEmploye = null;
                    while (i < lesEmployes.Count && lesEmployes[i].Id != Convert.ToInt32(reader[1]))
                    {
                        i++;
                    }
                    if (i < lesEmployes.Count && lesEmployes[i].Id == Convert.ToInt32(reader[1]))
                    {
                        unEmploye = lesEmployes[i];
                        dip.LesEmployes.Add(unEmploye);
                    }
                }
                reader.Close();
            }
            this.cnx.Close();
        }

        public void ToutCharger()
        {
            ChargerServices();
            ChargerEmployes();
            ChargerLesEmployesDesServices();
            ChargerDiplomes();
            ChargerLesDiplomesDesEmployes();
            ChargerLesEmployesTitulaireDesDiplomes();
        }

        public void AjouterService(int id, string nom, char type, string produit, int capacite, decimal budget)
        {
            Service newSer;
            newSer = new Service(id, nom, type, produit, capacite, budget);
            lesServices.Add(newSer);
        }

        public void Sauvegarder()
        {
            this.cnx.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "RemiseAZero";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            //Insérer les Services dans la table Service
            MySqlCommand cmdSql = new MySqlCommand();
            cmdSql.Connection = cnx;
            MySqlParameter idSer = new MySqlParameter("idSer", MySqlDbType.Int32);
            cmdSql.Parameters.Add(idSer);
            MySqlParameter designationSer = new MySqlParameter("designationSer", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(designationSer);
            MySqlParameter typeSer = new MySqlParameter("typeSer", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(typeSer);
            MySqlParameter produitSer = new MySqlParameter("produitSer", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(produitSer);
            MySqlParameter capaciteSer = new MySqlParameter("capaciteSer", MySqlDbType.Int32);
            cmdSql.Parameters.Add(capaciteSer);
            MySqlParameter budgetSer = new MySqlParameter("budgetSer", MySqlDbType.Decimal);
            cmdSql.Parameters.Add(budgetSer);
            for (int i = 0; i < lesServices.Count; i++)
            {
                idSer.Value = lesServices[i].Id;
                designationSer.Value = lesServices[i].Designation;
                typeSer.Value = lesServices[i].Type;
                produitSer.Value = lesServices[i].Produit;
                capaciteSer.Value = lesServices[i].Capacite;
                budgetSer.Value = lesServices[i].Budget;
                if ((char)lesServices[i].Type == 'P')
                {
                    cmdSql.CommandText = "INSERT INTO Service(ser_designation, ser_type, ser_produit, ser_capacite, ser_budget) VALUES(@designationSer, @typeSer, @produitSer, @capaciteSer, 0)";
                    cmdSql.CommandType = CommandType.Text;
                    cmdSql.Prepare();
                    cmdSql.ExecuteNonQuery();
                }
                if ((char)lesServices[i].Type == 'A')
                {
                    cmdSql.CommandText = "INSERT INTO Service(ser_designation, ser_type, ser_produit, ser_capacite, ser_budget) VALUES(@designationSer, @typeSer, 0, 0, @budgetSer)";
                    cmdSql.CommandType = CommandType.Text;
                    cmdSql.Prepare();
                    cmdSql.ExecuteNonQuery();
                }
            }

            //Insérer les Diplomes dans la table Diplôme
            MySqlParameter libelleDip = new MySqlParameter("libelleDip", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(libelleDip);
            MySqlParameter idDip = new MySqlParameter("idDip", MySqlDbType.Int32);
            cmdSql.Parameters.Add(idDip);
            for (int i = 0; i < lesDiplomes.Count; i++)
            {
                idDip.Value = lesDiplomes[i].Id;
                libelleDip.Value = lesDiplomes[i].Libelle;
                cmdSql.CommandText = "INSERT INTO Diplome(dip_id, dip_libelle) VALUES(@idDip, @libelleDip)";
                cmdSql.CommandType = CommandType.Text;
                cmdSql.Prepare();
                cmdSql.ExecuteNonQuery();
            }

            //Insérer les Employers dans la table Employe
            MySqlParameter idEmp = new MySqlParameter("idEmp", MySqlDbType.Int32);
            cmdSql.Parameters.Add(idEmp);
            MySqlParameter nomEmp = new MySqlParameter("nomEmp", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(nomEmp);
            MySqlParameter prenomEmp = new MySqlParameter("prenomEmp", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(prenomEmp);
            MySqlParameter sexeEmp = new MySqlParameter("sexeEmp", MySqlDbType.VarChar);
            cmdSql.Parameters.Add(sexeEmp);
            MySqlParameter cadreEmp = new MySqlParameter("cadreEmp", MySqlDbType.Byte);
            cmdSql.Parameters.Add(cadreEmp);
            MySqlParameter salaireEmp = new MySqlParameter("salaireEmp", MySqlDbType.Decimal);
            cmdSql.Parameters.Add(salaireEmp);
            MySqlParameter serEmp = new MySqlParameter("serEmp", MySqlDbType.Int32);
            cmdSql.Parameters.Add(serEmp);
            for (int i = 0; i < lesEmployes.Count; i++)
            {
                idEmp.Value = lesEmployes[i].Id;
                nomEmp.Value = lesEmployes[i].Nom;
                prenomEmp.Value = lesEmployes[i].Prenom;
                sexeEmp.Value = lesEmployes[i].Sexe;
                cadreEmp.Value = lesEmployes[i].Cadre;
                salaireEmp.Value = lesEmployes[i].Salaire;
                if (lesEmployes[i].Service != null)
                {
                    serEmp.Value = lesEmployes[i].Service.Id;
                }
                else
                {
                    serEmp.Value = null;
                }
                serEmp.Value = lesEmployes[i].Service.Id;
                cmdSql.CommandText = "INSERT INTO employe(emp_id, emp_nom, emp_prenom, emp_sexe, emp_cadre, emp_salaire, emp_service) VALUES(@idEmp, @nomEmp, @prenomEmp, @sexeEmp, @cadreEmp, @salaireEmp, @serEmp)";
                cmdSql.CommandType = CommandType.Text;
                cmdSql.Prepare();
                cmdSql.ExecuteNonQuery();


                //Insérer les occurrences de la table posseder
                MySqlCommand cmdPos = new MySqlCommand();
                cmdPos.Connection = cnx;
                cmdPos.CommandText = "INSERT INTO posseder(pos_employe,pos_diplome) VALUES(@EmployePoss, @DiplomePoss)";
                cmdPos.CommandType = CommandType.Text;
                MySqlParameter DiplomePoss = new MySqlParameter("DiplomePoss", MySqlDbType.Int32);
                cmdPos.Parameters.Add(DiplomePoss);
                MySqlParameter EmployePoss = new MySqlParameter("EmployePoss", MySqlDbType.Int32);
                cmdPos.Parameters.Add(EmployePoss);
                foreach (Employe e in lesEmployes)
                {
                    foreach (Diplome d in e.Diplomes){
                        EmployePoss.Value = e.Id;
                        DiplomePoss.Value = d.Id;
                        cmdPos.ExecuteNonQuery();
                    }
                }
            }
            this.cnx.Close();
        }
        
    }
}
