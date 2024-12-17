﻿namespace ExamenAPIEFC.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }

        //FK
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
