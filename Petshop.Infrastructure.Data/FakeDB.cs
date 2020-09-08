using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public static class FakeDB
    {
        public static int PetId = 1;
        public static List<Pet> PetTable = new List<Pet>();

        public static void InitData()
        {
            PetTable.Add(
                new Pet
                {
                    Id = PetId++,
                    Name = "Snoopy",
                    Type = PetType.Dog,
                    BirthDate = Convert.ToDateTime("01/01/2000"),
                    SoldDate = Convert.ToDateTime("24/12/2000"),
                    Color = "White",
                    PreviousOwner = "Bob",
                    Price = 50
                });

            PetTable.Add(
                new Pet
                {
                    Id = PetId++,
                    Name = "Garfield",
                    Type = PetType.Cat,
                    BirthDate = Convert.ToDateTime("01/01/2000"),
                    SoldDate = Convert.ToDateTime("24/12/2000"),
                    Color = "Orange",
                    PreviousOwner = "Jon Arbuckle",
                    Price = 10
                });

            PetTable.Add(
                new Pet
                {
                    Id = PetId++,
                    Name = "Bugs Bunny",
                    Type = PetType.Rabbit,
                    BirthDate = Convert.ToDateTime("01/01/2000"),
                    SoldDate = Convert.ToDateTime("24/12/2000"),
                    Color = "Grey/White",
                    PreviousOwner = "None",
                    Price = 100
                });
        }
    }
}
