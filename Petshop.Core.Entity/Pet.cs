﻿using System;

namespace Petshop.Core.Entity
{
    public class Pet 
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; } 
        public string Color { get; set; }
        public string PreviousOwner { get; set; }
        public int Price { get; set; }
    }
}