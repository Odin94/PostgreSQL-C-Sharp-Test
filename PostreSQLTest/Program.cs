using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostreSQLTest
{
    class Program
    { 
        static void Main(string[] args)
        {
            DatabaseHelper dbh = new DatabaseHelper();
            dbh.Insert(24, "testdata");
            foreach (var data in dbh.SelectAll())
            {
                Debug.WriteLine(data.data);
            }
            Debug.WriteLine("donnezzo");
        }
    }
}
