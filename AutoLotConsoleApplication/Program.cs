using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotConsoleApplication.EF;
using static System.Console;

namespace AutoLotConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Fun with ADO.NET EF\n");
            int carId = AddNewRecord();
            WriteLine(carId);
            FunWithLinqQueries();
            ReadLine();
        }

        static int AddNewRecord()
        {
            //Добавление записи в таблицу Inventory базы данных AutoLot
            using (var context = new AutoLotEntities())
            {
                try
                {
                    //В целях тестирования жестко закодировать данные для новой записи
                    var car = new Car { Make = "Yugo", Color = "Brown", CarNickName = "Brownie" };
                    context.Cars.Add(car);
                    context.SaveChanges();
                    //В случае успешного сохранения EF заполняет поле идентификатора значением, сгенерированным базой данных.
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException?.Message);
                    return 0;
                }
            }
        }

        static void AddNewRecords(IEnumerable<Car> carToAdd)
        {
            using (var context = new AutoLotEntities())
            {
                context.Cars.AddRange(carToAdd);
                context.SaveChanges();
            }
        }

        static void PrintAllInventory()
        {
            //Выбрать все элементы из таблицы Inventory бд AutoLot
            //и вывести данные с применением специального метода ToString()
            //сущностного класса Car
            using (var context = new AutoLotEntities())
            {
                foreach (var item in context.Cars)
                {
                    WriteLine(item);
                }
            }
        }

        static void PrintAllInventoryUseSql()
        {
            using (var context = new AutoLotEntities())
            {
                foreach (var item in context.Cars.SqlQuery("Select CarId, Make, Color, " +
                    "PetName as CarNickName from Inventory where Make=@p0", "BMW"))
                {
                    WriteLine(item);
                }
            }
        }

        static void PrintAllInventoryUseLinq()
        {
            using (var context = new AutoLotEntities())
            {
                foreach (Car car in context.Cars.Where(c => c.Make == "BMW"))
                {
                    WriteLine(car);
                }
            }
        }

        static void FunWithLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                //Получить проекцию новых данных
                var colorMakes = from item in context.Cars select new { item.Color, item.Make };
                foreach (var item in colorMakes)
                {
                    WriteLine(item);
                }

                //Получить только элементы в которых цвет черный
                var blackCars = from item in context.Cars where item.Color == "Black" select item;
                foreach (Car car in blackCars)
                {
                    WriteLine(car);
                }
            }
        }
    }
}
