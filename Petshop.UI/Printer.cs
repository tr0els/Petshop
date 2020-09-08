using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Petshop.Core.ApplicationServices;
using Petshop.Core.Entity;

namespace Petshop.UI.ConsoleApp
{
    class Printer : IPrinter
    {
        private IPetService _petService;

        readonly string[] menuItems =
        {
            "Show list of all Pets",
            "Search Pets by Type",
            "Create a new Pet",
            "Delete Pet",
            "Update a Pet",
            "Sort Pets By Price",
            "Get 5 cheapest available Pets",
            "Exit"
        };

        public Printer(IPetService petService)
        {
            _petService = petService;
        }

        public void ShowMenu()
        {

            ListMenuItems();
            int selection = GetValidMenuSelection();
            HandleMenuSelection(selection);
        }

        public void ListMenuItems()
        {
            System.Console.WriteLine("Welcome to the Petshop!\n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                System.Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }
        }

        private int GetValidMenuSelection()
        {
            int selection;

            do
            {
                System.Console.Write("\nPlease select a menu option: ");
            } while (!int.TryParse(System.Console.ReadLine(), out selection)
                     || selection < 0
                     || selection > menuItems.Length);

            return selection;
        }

        public void HandleMenuSelection(int selection)
        {
            while (selection != menuItems.Length)
            {
                switch (selection)
                {
                    case 1:
                        ListPets();
                        break;
                    case 2:
                        SearchPetsByType();
                        break;
                    case 3:
                        CreatePet();
                        break;
                    case 4:
                        DeletePet();
                        break;
                    case 5:
                        UpdatePet();
                        break;
                    case 6:
                        SortPetsByPrice(_petService.ReadAll(), "DESC");
                        break;
                    case 7:
                        Top5Cheapest(_petService.ReadAll());
                        break;
                    default:
                        break;
                }

                selection = GetValidMenuSelection();
            }
        }

        private void ListPets()
        {
            System.Console.WriteLine("List of all pets\n");
            ListPets(_petService.ReadAll());
        }

        private void ListPets(IEnumerable<Pet> pets)
        {
            foreach (Pet pet in pets)
            {
                System.Console.WriteLine($"Id: {pet.Id} Name: {pet.Name} Type: {pet.Type}  Price: {pet.Price} Type: {pet.Type}");
            }
        }

        private void SearchPetsByType()
        {
            Console.Write("Type the type of pet you want to search for: ");
            var petType = Console.ReadLine();

            var pets = _petService.SearchByType(petType);
            ListPets(pets);
        }

        private void CreatePet()
        {
            // todo: input check + exception handling

            Console.WriteLine("Create new pet:\n");
            var petToCreate = AskPetDetails();
            var pet = _petService.Create(petToCreate);
            Console.WriteLine($"Pet with Id: {pet.Id} is created");
        }

        private Pet AskPetDetails()
        {
            Console.Write("Type name: ");
            var name = Console.ReadLine();

            var petTypes = string.Join(", ", Enum.GetNames(typeof(PetType)));
            Console.Write($"Type pet-type ({petTypes}): ");
            var petTypeString = Console.ReadLine();
            Enum.TryParse(petTypeString, out PetType petType);

            Console.Write("Type birthdate (dd/MM-yyyy): ");
            var birthDateString = Console.ReadLine();
            DateTime birthDate = DateTime.ParseExact(birthDateString, "dd/MM-yyyy", CultureInfo.InvariantCulture);

            Console.Write("Type pet sold date (dd/MM-yyyy): ");
            var soldDateString = Console.ReadLine();
            DateTime soldDate = DateTime.ParseExact(soldDateString, "dd/MM-yyyy", CultureInfo.InvariantCulture);

            Console.Write("Type pet color: ");
            var color = Console.ReadLine();

            Console.Write("Type pet previous owner: ");
            var previousOwner = Console.ReadLine();

            Console.Write("Type pet price: ");
            int price;
            int.TryParse(Console.ReadLine(), out price);

            Pet pet = new Pet
            {
                Name = name,
                Type = petType,
                BirthDate = birthDate,
                SoldDate = soldDate,
                Color = color,
                PreviousOwner = previousOwner,
                Price = price
            };

            return pet;
        }

        private void DeletePet()
        {
            Console.Write("Type id of the pet to delete: ");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            _petService.Delete(id);
            Console.Write($"Pet with id: {id} was deleted");
        }

        private void UpdatePet()
        {
            Console.Write("Type id of the pet to update: ");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            var petToUpdate = AskPetDetails();
            petToUpdate.Id = id;
            var pet = _petService.Update(petToUpdate);
            Console.WriteLine($"Pet with Id: {pet.Id} is updated");
        }

        private void SortPetsByPrice(IEnumerable<Pet> pets, string order)
        {
            Console.WriteLine($"List of pets sorted by price (order: {order}):\n");
            var petsSorted = _petService.SortByPrice(pets, order);
            ListPets(petsSorted);
        }

        private void Top5Cheapest(IEnumerable<Pet> pets)
        {
            Console.WriteLine($"List of the 5 cheapest available pets:\n");
            var petsCheapest = _petService.TopXCheapest(_petService.ReadAll(), 5);
            ListPets(petsCheapest);
        }
    }
}