using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    class ConcurrentProgrammingQuestions
    {
        public static Random rand = new Random();

        public void Run()
        {
            Console.WriteLine($"1. Dining Philosopher");
            Console.WriteLine($"2. Reader-Writer Lock");
            Console.WriteLine($"3. Producer-Consumer");
            Console.WriteLine($"4. Sleeping barber");
            Console.Write($"Enter choice: ");
            int choice = Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            switch (choice)
            {
                case 1:
                    DiningPhilosopher dp = new DiningPhilosopher();
                    dp.Start();
                    break;

                case 2:
                    DataBase db = new DataBase();
                    Reader r1 = new Reader(1, db);
                    Writer w1 = new Writer(1, db);

                    Task t1 = Task.Factory.StartNew(() => r1.Run());  
                    Task t2 = Task.Factory.StartNew(() => w1.Run());
                    Task.WaitAll(t1, t2);

                    break;

                case 3:

                    Pipe p = new Pipe();
                    Producer pr = new Producer(p);
                    Consumer cn = new Consumer(p);
                    t1 = Task.Factory.StartNew(() => pr.Run());
                    t2 = Task.Factory.StartNew(() => cn.Run());
                    Task.WaitAll(t1, t2);
                    break;

                case 4:

                    SleepingBarber sb = new SleepingBarber();
                    sb.Run();

                    break;

                default:
                    throw new InvalidOperationException("No choice match found");
            }
        }

        #region Dining Philosopher

        public class Fork
        {
            SemaphoreSlim Sem = new SemaphoreSlim(1, 1);
            public int Id;

            public Fork(int id)
            {
                this.Id = id;
            }

            public void PickUp()
            {
                Sem.Wait();
            }

            public void Release()
            {
                Sem.Release();
            }
        }

        public class Philosopher
        {
            public int Id;
            private Fork Lfork;
            private Fork Rfork;

            public Philosopher(int id, Fork a, Fork b)
            {
                this.Id = id;
                Lfork = a;
                Rfork = b;
            }

            public void Run()
            {
                Console.WriteLine($"Philosopher {Id} started with Fork {Lfork.Id}, {Rfork.Id}");
                while (true)
                {
                    eat();
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                }
            }

            private void eat()
            {
                Console.WriteLine($"Philosopher {Id} is Thinking.");
                int sec = rand.Next(2, 8);
                Thread.Sleep(TimeSpan.FromSeconds(sec));
                Lfork.PickUp();
                Console.WriteLine($"Philosopher {this.Id} is Hungry after {sec} seconds.");
                Rfork.PickUp();
                Console.WriteLine($"Philosopher {Id} is eating");
                Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                Rfork.Release();
                Lfork.Release();
                Console.WriteLine($"Philosopher {Id} has released Fork.");
            }
        }

        public class DiningPhilosopher
        {
            private Fork[] forks;

            private Philosopher[] philosophers;

            public DiningPhilosopher()
            {
                forks = new[]
                {
                    new Fork(1),
                    new Fork(2),
                    new Fork(3),
                    new Fork(4),
                    new Fork(5),
                };

                philosophers = new Philosopher[5];

                for (int i = 0; i < philosophers.Length; i++)
                {
                    if (i == philosophers.Length - 1)
                    {
                        philosophers[i] = new Philosopher(i + 1, forks[i], forks[0]);
                    }
                    else
                    {
                        philosophers[i] = new Philosopher(i + 1, forks[i], forks[i + 1]);
                    }
                }
            }

            public void Start()
            {
                Parallel.ForEach(philosophers, philosopher => { philosopher.Run(); });
            }

        }

        #endregion

        #region Reader Writer Lock

        public class DataBase
        {
            private int readerCount;
            private SemaphoreSlim readLock;
            private SemaphoreSlim wrtLock;

            public DataBase()
            {
                readerCount = 0;
                readLock = new SemaphoreSlim(1);
                wrtLock = new SemaphoreSlim(1);
            }

            public void AcquireReadLock(int id)
            {
                readLock.Wait();

                readerCount++;
                if (readerCount == 1)
                {
                    wrtLock.Wait();
                }

                Console.WriteLine($"Reader {id} started reading. Reader Count {readerCount}");
                readLock.Release();
            }

            public void ReleaseReadLock(int id)
            {
                readLock.Wait();

                readerCount--;
                Console.WriteLine($"Reader {id} has finished reading. Reader Count {readerCount}");
                if (readerCount == 0)
                {
                    wrtLock.Release();
                }

                readLock.Release();
            }

            public void AcquireWriteLock(int id)
            {
                wrtLock.Wait();
                Console.WriteLine($"Writer {id} has started writing.");
            }

            public void ReleaseWriteLock(int id)
            {
                wrtLock.Release();
                Console.WriteLine($"Writer {id} has finished writing.");
            }
        }

        public class Reader
        {
            private int Id;
            private DataBase db;

            public Reader(int id, DataBase db)
            {
                Id = id;
                this.db = db;
            }

            public void Run()
            {
                while (true)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                    Console.WriteLine($"Reader {Id} wants to read.");
                    db.AcquireReadLock(Id);
                    Console.WriteLine($"Reader {Id} is reading ...");
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                    db.ReleaseReadLock(Id);
                }
            }
        }

        public class Writer
        {
            private int Id;
            private DataBase db;

            public Writer(int id, DataBase db)
            {
                Id = id;
                this.db = db;
            }

            public void Run()
            {
                while (true)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                    Console.WriteLine($"Writer {Id} wants to write.");
                    db.AcquireWriteLock(Id);
                    Console.WriteLine($"Writer {Id} is Writing ...");
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                    db.ReleaseWriteLock(Id);
                }
            }
        }

        #endregion

        #region Producer consumer

        class Pipe
        {
            private int item;
            private SemaphoreSlim semProd;
            private SemaphoreSlim semCon;

            public Pipe()
            {
                semProd = new SemaphoreSlim(1);
                semCon = new SemaphoreSlim(0);
            }

            public void Put(int i)
            {
                semProd.Wait();
                item = i;
                Console.WriteLine($"Put {item}");
                semCon.Release();
            }

            public void Get()
            {
                semCon.Wait();
                Console.WriteLine($"Get {item}");
                semProd.Release();
            }
        }
         
        class Producer
        {
            private Pipe p;
            public Producer(Pipe pipe)
            {
                p = pipe;
            }

            public void Run()
            {
                while (true)
                {
                    p.Put(rand.Next(2, 50));
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                }
            }
        }

        class Consumer
        {
            private Pipe p;

            public Consumer(Pipe pipe)
            {
                p = pipe;
            }

            public void Run()
            {
                while (true)
                {
                    p.Get();
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 5)));
                }
            }
        }

        #endregion

        #region Sleeping Barber

        class Barber
        {
            private object syncObj;
            private Queue<int> queue;

            public Barber(object syncObj, Queue<int> q)
            {
                this.syncObj = syncObj;
                this.queue = q;
            }

            public void Run()
            {
                while (true)
                {
                    lock (syncObj)
                    {
                        Console.WriteLine($"Barber is sleeping....");
                        Monitor.Wait(syncObj);
                        Console.WriteLine("Waking up.....");
                        while (queue.Any())
                        {
                            int cid = queue.Dequeue();
                            Console.WriteLine($"Cutting hair of customer {cid}");
                            Thread.Sleep(TimeSpan.FromSeconds(rand.Next(1, 3)));
                        }
                        Console.WriteLine($"Barber going to sleep...");
                        Monitor.PulseAll(syncObj);
                        Thread.Sleep(TimeSpan.FromSeconds(rand.Next(2,4)));
                    }
                }
            }

        }

        class Customer
        {
            private object syncObj;
            private Queue<int> queue;

            public Customer(object syncObj, Queue<int> q)
            {
                this.syncObj = syncObj;
                queue = q;
            }

            public void Run()
            {
                int id = 1;
                while (true)
                {
                    AddCustomer(id++);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    AddCustomer(id++);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    AddCustomer(id++);
                    Thread.Sleep(TimeSpan.FromSeconds(rand.Next(6,10)));
                }
            }

            private void AddCustomer(int id)
            {
                lock (syncObj)
                {
                    queue.Enqueue(id);
                    if (queue.Count == 1) Monitor.PulseAll(syncObj);
                }
            }
        }

        class SleepingBarber
        {
            private object syncObj;
            private Queue<int> cq;
            private Customer c;
            private Barber b;

            public SleepingBarber()
            {
                syncObj = new object();
                cq = new Queue<int>();
                c = new Customer(syncObj, cq);
                b = new Barber(syncObj, cq);
            }

            public void Run()
            {
                Task t1 = Task.Factory.StartNew(() => c.Run());
                Task t2 = Task.Factory.StartNew(() => b.Run());

                Task.WaitAll(t1, t2);
            }
        }
        #endregion
    }
}
