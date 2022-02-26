using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingDataWithSQLClient.Models
{
    public class HighestInvoice
    {

        public int Customer_Id { get; set; }
        public string Customer_Name { get; set;}    
        public string Last_Name { get; set;}
        public decimal Invoice_total { get; set; }

    }
}
