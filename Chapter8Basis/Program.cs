﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrudImplementations;
using Model;

namespace Chapter8Basis
{
    class Program
    {
        static void Main(string[] args)
        {
            Order ord = new Order();
            ord.product = "Candy Canes";
            ord.amount = 12;

            Console.WriteLine("=========CreateSeparateServices=========");
            OrderController sep = CreateSeparateServices();
            sep.CreateOrder(ord);
            

            Console.WriteLine("=========CreateSingleService=========");
            OrderController sing = CreateSingleService();
            sing.CreateOrder(ord);

            Console.WriteLine("=========GenericController<Order>=========");
            GenericController<Order> generic = CreateGenericServices();


            Console.WriteLine("Hit any key to quit");
            Console.ReadKey();
        }

        static OrderController CreateSeparateServices()
        {
            var reader = new Reader<Order>();
            var saver = new Saver<Order>();
            var deleter = new Deleter<Order>();
            return new OrderController(reader, saver, deleter);
        }

        static OrderController CreateSingleService()
        {
            var crud = new Crud<Order>();
            return new OrderController(crud, crud, crud);
        }

        static GenericController<Order> CreateGenericServices()
        {
            var reader = new Reader<Order>();
            var saver = new Saver<Order>();
            var deleter = new Deleter<Order>();
            // This must be declared using reflection...
            GenericController<Order> ctl = (GenericController<Order>)Activator.CreateInstance(typeof(GenericController<Order>), reader, saver, deleter);
            //This does not work 
            //GenericController<Order> ctl = new GenericController(reader, saver, deleter);
            return ctl;
        }

    }
}
