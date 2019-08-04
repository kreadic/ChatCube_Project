using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConStringProvider
/// </summary>
public class ConStringProvider
{
    public ConStringProvider(string server)
    {
        ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+server+";Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
    }
    public string ConnectionString { get; set; }
}