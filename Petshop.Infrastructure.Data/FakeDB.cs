using Petshop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                    BirthDate = DateTime.Now,
                    SoldDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
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
                    BirthDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                    SoldDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
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
                    BirthDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                    SoldDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                    Color = "Grey/White",
                    PreviousOwner = "None",
                    Price = 100
                });
        }
    }
}
