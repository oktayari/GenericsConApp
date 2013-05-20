using System;
using System.Globalization;
using System.Linq.Expressions;

namespace GenericsConApp
{
    internal class Program
    {
        private static void Main()
        {
            #region Example 1

            var g = new Generic<string>();
            g.Field = "A string";
            g.PrintTypesValues();

            var b = new Generic<int>();
            b.Field = 5;
            b.PrintTypesValues();


            var gd = new GenericType<string, int>("ALI", 20);
            gd.PrintTypesValues();

            #endregion

            #region Example 2

            var factory = new Factory<Cat>();
            Cat cat = factory.Create();
            cat.MakeVoice();

            var factorya = new Factory<Dog>();
            Dog dog = factorya.Create();
            dog.MakeVoice();

            // Type Safety
            // var factoryb = new Factory<Monkey>();

            #endregion

            #region Example 3

            var europa = new AnimalWorld<Europe>();

            europa.RunFoodChain();

            #endregion

            #region Example 4

            #endregion

            #region Example 5

            var test = new TestDeleagte();

            test.TestDelegateMulticatsMultipleInstances();
            test.TestDelegateMultiCastsSingleInstance();

            #endregion

            #region Example 6

            var asd = new AnonymousTest();
            asd.TestAnonymous01();
            // asd.NewCarEvent += asd_NewCarEvent;
            asd.CarsInGarage = 5;

            // anonymous delegates
            asd.NewCarEvent += (numCars, message) => Console.WriteLine(numCars + "  " + message);
            int cars = asd.CarsInGarage;

            asd.CreateEvent(7);

            #endregion

            #region Example 7

            Func<Double, Double> circleArea = r => 3.12*r*r;
            double area = circleArea(20);

            Action<string> myAction = Console.Write;
            myAction("Hello");

            Predicate<string> checkGraterThan5 = x => x.Length > 5;
            bool ddd = checkGraterThan5("ABCDEFG");


            BinaryExpression b1 = Expression.MakeBinary(ExpressionType.Add,
                                                        Expression.Constant(20),
                                                        Expression.Constant(10));

            BinaryExpression b2 = Expression.MakeBinary(ExpressionType.Add,
                                                        Expression.Constant(20),
                                                        Expression.Constant(10));

            BinaryExpression b3 = Expression.MakeBinary(ExpressionType.Subtract, b1, b2);

            int result = Expression.Lambda<Func<int>>(b3).Compile()();

            #endregion

            #region Example 8

            var sbs = new Subscriber();
            //sbs.Added += sbs_Added;

            Console.WriteLine("\r\n");
            sbs.Added += (sender, e) => Console.WriteLine(sender + "  " + e.EventName);
            sbs.Add(5);

            #endregion

            #region Example 9

            var gc = new GenericClass();
            gc.DummyOper(12, "abc");
            gc.DummyOper(12, 4);

            gc.Swap(12, 12);
            //gc.Swap(12,"dd");

            #endregion

            #region Example 10

            var dg = new Dog();
            dg.getColor(ConsoleColor.Black);

            #endregion
        }

        private static void sbs_Added(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString());
        }

        private static void asd_NewCarEvent(string numberOfCars, string message)
        {
            Console.WriteLine(numberOfCars + "  " + message);
        }
    }

    #region Example 1

    public class Generic<T>
    {
        public T Field;

        public void PrintTypesValues()
        {
            Console.WriteLine("Generic.Field.GetType() = {0} ,  Value ={1}", Field.GetType().FullName, Field);
        }
    }


    public class GenericType<T, TU>
    {
        private readonly T Fielda;
        private readonly TU Fieldb;

        public GenericType(T typea, TU typeb)
        {
            Fielda = typea;
            Fieldb = typeb;
        }


        public void PrintTypesValues()
        {
            Console.WriteLine("Generic.Field.GetType() = {0} , Value = {1}", Fielda.GetType().FullName, Fielda);
            Console.WriteLine("Generic.Field.GetType() = {0} , Value = {1}", Fieldb.GetType().FullName, Fieldb);
        }
    }

    #endregion

    #region Example 2

    public class Factory<T> where T : IAnimal, new()
    {
        public T Create()
        {
            return new T();
        }
    }

    public interface IAnimal
    {
        void MakeVoice();
    }

    public class Cat : IAnimal
    {
        public void MakeVoice()
        {
            Console.WriteLine("I am Cat");
        }

        public void CatVoice()
        {
        }
    }

    public partial class Dog : IAnimal
    {
        public void MakeVoice()
        {
            Console.WriteLine("I am dog");
        }

        public void DogVoice()
        {
        }
    }

    public class Monkey
    {
    }

    #endregion

    #region Example 3

    public interface IAnimalWorld
    {
        void RunFoodChain();
    }

    public class AnimalWorld<T> : IAnimalWorld where T : IContinentFactory, new()
    {
        private readonly ICarnivore _carnivore;
        private readonly T _factory;
        private readonly IHerbivore _herbivore;


        public AnimalWorld()
        {
            _factory = new T();

            _carnivore = _factory.CreateCarnivore();
            _herbivore = _factory.CreateHerbivore();
        }


        public void RunFoodChain()
        {
            _carnivore.Eat(_herbivore);
        }
    }

    public interface ICarnivore
    {
        void Eat(IHerbivore herbivore);
    }

    public interface IHerbivore
    {
    }

    public class Herbivore : IHerbivore
    {
    }

    public class Carnivore : ICarnivore
    {
        public void Eat(IHerbivore herbivore)
        {
            Console.WriteLine("I am eating {0}", herbivore);
        }
    }


    public interface IContinentFactory
    {
        ICarnivore CreateCarnivore();
        IHerbivore CreateHerbivore();
    }

    public class Europe : IContinentFactory
    {
        public IHerbivore CreateHerbivore()
        {
            return new Herbivore();
        }

        public ICarnivore CreateCarnivore()
        {
            return new Carnivore();
        }
    }

    #endregion

    #region Example 4

    public interface ICacheProvider
    {
    }

    public class BaseCacheProvider : ICacheProvider
    {
    }


    public class DerivedCacheProvider : BaseCacheProvider
    {
    }

    public interface IStorageProvider<TCacheProvider> where TCacheProvider : ICacheProvider
    {
    }

    public abstract class BaseStorageProvider<TCacheProvider> : IStorageProvider<TCacheProvider>
        where TCacheProvider : ICacheProvider
    {
    }

    public class DerivedStorageProvider : BaseStorageProvider<DerivedCacheProvider>, IStorageProvider<ICacheProvider>
    {
    }

    public interface IResourceInfo<TStorageProvider> where TStorageProvider : IStorageProvider<ICacheProvider>
    {
    }

    public abstract class ResourceInfo<TStorageProvider> : IResourceInfo<TStorageProvider>
        where TStorageProvider : IStorageProvider<ICacheProvider>
    {
    }

    public abstract class BaseFileInfo<TStorageProvider> : ResourceInfo<TStorageProvider>
        where TStorageProvider : IStorageProvider<ICacheProvider>
    {
    }

    public class DerivedFileInfo : BaseFileInfo<DerivedStorageProvider>
    {
    }

    #endregion

    #region Example5

    public class TestDeleagte
    {
        public void TestDelegateMulticatsMultipleInstances()
        {
            SampleDelegate sd2, sd3, sd4, sd5;

            SampleDelegate sd1 = TestMethod00;
            sd2 = TestMethod01;
            sd3 = TestMethod02;
            sd4 = TestMethod03;

            sd5 = sd1 + sd2 + sd3 + sd4;
            sd5();
        }

        public void TestDelegateMultiCastsSingleInstance()
        {
            SampleDelegate del = TestMethod00;
            del += TestMethod01;
            del += TestMethod02;
            del += TestMethod03;

            del();
        }

        private void TestMethod00()
        {
            Console.WriteLine("Delegate TestMethod00");
        }

        private void TestMethod01()
        {
            Console.WriteLine("Delegate TestMethod01");
        }

        private void TestMethod02()
        {
            Console.WriteLine("Delegate TestMethod02");
        }

        private void TestMethod03()
        {
            Console.WriteLine("Delegate TestMethod03");
        }

        private delegate void SampleDelegate();
    }

    #endregion

    #region Example6

    public class AnonymousTest
    {
        public delegate void NewCarDelegate(string numberOfCars, string message);

        private int _carsInGarage;

        public AnonymousTest()
        {
            CarsInGarage = 0;
        }

        public int CarsInGarage
        {
            get { return _carsInGarage; }
            set
            {
                if (NewCarEvent != null)
                {
                    _carsInGarage = value;
                    NewCarEvent(value.ToString(CultureInfo.InvariantCulture), " cars in the garage.");
                }
            }
        }

        public event NewCarDelegate NewCarEvent;

        public void TestAnonymous01()
        {
            var testClass = new {Name = "Selami", LastName = "Yazgan"};
            Console.WriteLine(testClass.Name + " " + testClass.LastName);
            //testClass.Name = "Ahmet";
            // testClass.LastName = "Veli";
            Console.WriteLine(testClass.Name + " " + testClass.LastName);
        }


        public void CreateEvent(int car)
        {
            if (NewCarEvent != null)
            {
                _carsInGarage = car;
                NewCarEvent(_carsInGarage.ToString(CultureInfo.InvariantCulture), " cars in the garage.");
            }
        }
    }

    #endregion

    #region Example 8

    public class Subscriber
    {
        public delegate void MyEventHandler(object sender, MyCustomEventArgs e);

        public event MyEventHandler Added;

        protected virtual void OnAdded(MyCustomEventArgs e)
        {
            MyEventHandler handler = Added;
            if (handler != null) handler(this, e);
        }

        public int Add(int number)
        {
            int b = number;


            string eventName = string.Format("Test Event  {0} Added ", b);

            OnAdded(new MyCustomEventArgs(eventName));
            return b;
        }
    }

    public class MyCustomEventArgs : EventArgs
    {
        public MyCustomEventArgs(string eventName)
        {
            EventName = eventName;
        }

        public string EventName { get; set; }
    }

    #endregion

    #region Example 9

    public class GenericClass
    {
        public void Swap<T>(T a, T b)
        {
            T Temp;

            Temp = a;
        }


        public void DummyOper<T, U>(T a, U b)
        {
            T Tempa = a;
            U Tempb = b;

            Console.WriteLine(Tempa.GetType() == Tempb.GetType() ? "Same" : "Different");
        }
    }

    #endregion

    #region Example 10

    public interface IAnimal<T>
    {
        void getColor(T a);
    }

    public partial class Dog : IAnimal<ConsoleColor>
    {
        public void getColor(ConsoleColor a)
        {
            Console.WriteLine(a.ToString());
        }
    }

    #endregion
}