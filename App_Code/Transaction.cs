using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Transaction
{
    public int teacherID { get; set; }
    public string teacherName { get; set; }
    public int chemicalID { get; set; }
    public string chemicalName { get; set; }

    public double amountTakenDatabaseUnits { get; set; }
    public double amountTakenFieldUnits { get; set; }
    public double amountLeftDatabaseUnits { get; set; }
    public string databaseUnits { get; set; }
    public string fieldUnits { get; set; }

    public string checksum { get; set; }

    public Transaction(int teacherID, string teacherName, int chemicalID, string chemicalName, 
        double amountTakenDatabaseUnits, double amountTakenFieldUnits, double amountLeftDatabaseUnits, 
        string databaseUnits, string fieldUnits, string checksum)
	{
        this.teacherID = teacherID;
        this.teacherName = teacherName;
        this.chemicalID = chemicalID;
        this.chemicalName = chemicalName;
        
        this.amountTakenDatabaseUnits = amountTakenDatabaseUnits;
        this.amountTakenFieldUnits = amountTakenFieldUnits;
        this.amountLeftDatabaseUnits = amountLeftDatabaseUnits;
        this.databaseUnits = databaseUnits;
        this.fieldUnits = fieldUnits;

        this.checksum = checksum;
	}
}