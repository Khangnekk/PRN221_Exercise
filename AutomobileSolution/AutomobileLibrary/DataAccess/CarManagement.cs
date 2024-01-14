﻿using Microsoft.EntityFrameworkCore;

namespace AutomobileLibrary.DataAccess
{
    internal class CarManagement
    {
        //----------------------------------------------------------
        // Using Singleton Pattern
        private static CarManagement instance = null;
        private static readonly object instanceLock = new object();
        private CarManagement() { }
        public static CarManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CarManagement();
                    }
                    return instance;
                }
            }
        }
        //-----------------------------------------------------------
        public IEnumerable<Car> GetCarList()
        {
            List<Car> cars;
            try
            {
                var myStockDB = new MyStockContext();
                cars = myStockDB.Cars.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return cars;
        }
        //-----------------------------------------------------------
        public Car GetCarByID(int CarID)
        {
            Car car = null;
            try
            {
                var myStockDB = new MyStockContext();
                car = myStockDB.Cars.SingleOrDefault(car => car.CarId == CarID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return car;
        }
        //-----------------------------------------------------------
        public void AddNew(Car car)
        {
            try
            {
                Car _car = GetCarByID(car.CarId);
                if (_car == null)
                {
                    var myStockDB = new MyStockContext();
                    myStockDB.Cars.Add(car);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The car is already exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}");
            }
        }
        //-----------------------------------------------------------
        public void Update(Car car)
        {
            try
            {
                Car c = GetCarByID(car.CarId);
                if (c != null)
                {
                    var myStockDB = new MyStockContext();
                    myStockDB.Entry<Car>(car).State = EntityState.Modified;
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The car does not already exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //-----------------------------------------------------------
        public void Remove(Car car)
        {
            try
            {
                Car _car = GetCarByID(car.CarId);
                if (_car != null)
                {
                    var myStockDB = new MyStockContext();
                    myStockDB.Cars.Remove(car);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The car does not already exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
