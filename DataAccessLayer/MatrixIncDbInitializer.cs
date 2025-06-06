﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Look for any customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            // TODO: Hier moet ik nog wat namen verzinnen die betrekking hebben op de matrix.
            // - Denk aan de m3 boutjes, moertjes en ringetjes.
            // - Denk aan namen van schepen
            // - Denk aan namen van vliegtuigen            
            var customers = new Customer[]
            {
                new Customer { Name = "Neo", Address = "123 Elm St" , Active=true},
                new Customer { Name = "Morpheus", Address = "456 Oak St", Active = true },
                new Customer { Name = "Trinity", Address = "789 Pine St", Active = true }
            };
            context.Customers.AddRange(customers);

            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01"), PaymentMethod = "Ideal"},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01"), PaymentMethod = "Ideal"},
                new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01"), PaymentMethod = "Ideal"},
                new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01"), PaymentMethod = "Ideal"}
            };  
            context.Orders.AddRange(orders);

            var products = new Product[]
            {
                new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m},
                new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m},
                new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m}
            };
            context.Products.AddRange(products);

            var parts = new Part[]
            {
                new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen", Img = "/Images/tandwiel.png", Price = 27},
                new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules", Img = "/Images/m5-boutje.png", Price = 25},
                new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen", Img = "/Images/hydraulische-cilinder.png", Price = 250},
                new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen.", Img = "/Images/koelvloeistofpomp.png", Price = 200},
                new Part { Name = "Test", Description = "Dit is een test.", Img = "/Images/test.png", Price = 1}
            };
            context.Parts.AddRange(parts);
            
            var users = new User[]
            {
                new User { Gebruikersnaam = "admin", Wachtwoord = "admin123", Customer = customers[0] },
                new User { Gebruikersnaam = "neo", Wachtwoord = "matrix", Customer = customers[1] },
                new User { Gebruikersnaam = "trinity", Wachtwoord = "trinity", Customer = customers[2]}
            };
            context.Users.AddRange(users);
            
            var orderItems = new OrderItem[]
            {
                new OrderItem { Order = orders[0], Part = parts[0], Aantal = 1 },
                new OrderItem { Order = orders[0], Part = parts[2], Aantal = 2 },
                new OrderItem { Order = orders[1], Part = parts[1], Aantal = 3 },
                new OrderItem { Order = orders[2], Part = parts[0], Aantal = 1 }
            };
            context.OrderItems.AddRange(orderItems);

            context.SaveChanges();

            context.Database.EnsureCreated();
        }
    }
}
