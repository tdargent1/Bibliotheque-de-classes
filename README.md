## GESPER – LIAISON DE DONNEES - ADO.NET

On utilisera une bibliothèque de classes libGesper.
Chaque table de la base de données Gesper donnera lieu à la création d’une classe.
Chaque occurrence de table de la base de données Gesper sera instanciée sous forme d’objet et stockée dans une collection.

Les données seront stockées dans la base de données gesper.

![Capture.png](https://image.noelshack.com/fichiers/2019/13/3/1553699476-capture.png)


Schéma relationnel

SERVICE   (ser_id, ser_designation, ser-type, ser_produit, ser-capacité, ser_budget)
EMPLOYE   (emp_id, emp_nom, emp_prenom, emp_sexe,emp_cadre, emp_salaire, #emp_service)
DIPOME   (dip_id, dip_libelle)
POSSEDER   (#pos_diplome , #pos_employe)

