using System;
using System.Data;
namespace PharmacyConsoleApp
{
    class Drug
    {
        public int ID { get; set; }
        private static int _ID = 1;
        public string Name { get; set; }
        public double Count { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public DrugType drugType { get; set; }
        public Drug( )
        {
            ID = _ID;
            ++_ID;
             
        }
      
        public enum DrugType
        {
            Powder,
            Syrop,
            Tablet
        }

    }
}
