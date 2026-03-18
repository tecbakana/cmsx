using System;
using System.Collections.Generic;
using System.Data;


namespace ICMSX
{
    public interface IDataFactory
    {
        int NumParms { get; set; }
        KeyValuePair<IDbConnection, KeyValuePair<IDbCommand, IDataParameter[]>> GetConnection(string banco,int parms);
    }
}
