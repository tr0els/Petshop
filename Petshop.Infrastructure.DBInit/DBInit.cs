using Petshop.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using Petshop.Core.Entity;
using System.Linq;


namespace Petshop.Infrastructure.DBInit
{
    public class DBInit
    {
        private readonly IPetRepository _petRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserRepository _userRepository;

        public DBInit(IPetRepository petRepository, IOwnerRepository ownerRepository, IUserRepository userRepository)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
            _userRepository = userRepository;
        }

        public void InitData()
        {
            // Only init data if repository is empty
            if (_petRepository.ReadAll().List.Any())
                return;

            _petRepository.Create(new Pet()
            {
                Name = "Garfield",
                Type = PetType.Cat,
                BirthDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                SoldDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                Color = "Orange",
                PreviousOwner = "Jon Arbuckle",
                Price = 10
            });

            _petRepository.Create(new Pet()
            {
                Name = "Snoopy",
                Type = PetType.Dog,
                BirthDate = DateTime.Now,
                SoldDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                Color = "White",
                PreviousOwner = "Bob",
                Price = 50
            });

            _petRepository.Create(new Pet()
            {
                Name = "Bugs Bunny",
                Type = PetType.Rabbit,
                BirthDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                SoldDate = DateTime.Parse("12 Juni 2008", new CultureInfo("da-DK")),
                Color = "Grey/White",
                PreviousOwner = "None",
                Price = 100
            });

            _ownerRepository.Create(new Owner()
            {
                FirstName = "Blondie",
                LastName = "The Good",
                Address = "Sad Hill Cemetery The Unmarked Grave",
                Phone = "555-GoldAndGuns",
                Email = "spaghettiwestern@sergioleone.it"
            });

            _userRepository.Create(new User()
            {
                Username = "UserJoe",
                Password = "1234",
                IsAdmin = false
            });

            _userRepository.Create(new User()
            {
                Username = "AdminAnn",
                Password = "1234", 
                IsAdmin = true
            });
        }
    }
}
