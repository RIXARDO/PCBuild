using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCbuild_ASP.MVC_.Domain.Concrete;
using PCbuild_ASP.MVC_.Domain.Entities;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;

namespace PCbuild_ASP.MVC_.Tests.RepositoryTest
{
    /// <summary>
    /// Summary description for GenericRepositoryTest
    /// </summary>
    [TestClass]
    public class GenericRepositoryTest
    {
        readonly EFDbContext context;

        EFRepository<CPU> CPURepository;
        EFRepository<GPU> GPURepository;

        public GenericRepositoryTest()
        {
            context = new EFDbContext(TestDBInit.TestDatabaseName);
            context.Database.CreateIfNotExists();
            CPURepository = new EFRepository<CPU>(context);
            GPURepository = new EFRepository<GPU>(context);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Create_CPUEntityAdd_RightEntityReturned()
        {
            //Arrange
            string processorNumber = "9000ki";
            string status = "Good";
            int amount = 100;
            int currency = 0;
            Price price = new Price() { Amount = amount, Сurrency = (Currency)currency };

            SqlParameter parameterProcNumber =
                new SqlParameter("@processorNumber", processorNumber);
            SqlParameter parameterStatus = new SqlParameter("@status", status);
            SqlParameter parameterAmount = new SqlParameter("@amount", amount);
            SqlParameter parameterCurrency = new SqlParameter("@currency", currency);

            string sqlExpressionPNumber =
                "SELECT COUNT(*) FROM CPUs WHERE ProcessorNumber=@processorNumber";
            string sqlExpressionStatus =
                "SELECT COUNT(*) FROM Products WHERE Status=@status";
            string sqlExpressionPrice =
                "SELECT COUNT(*) FROM Prices WHERE Amount=@amount AND Сurrency = @currency";

            CPU cpu = new CPU()
            {
                ProcessorNumber = processorNumber,
                Status = status,
                Price = price
            };
            //Act
            CPURepository.Create(cpu);
            context.SaveChanges();
            //Assert
            object ProductNumberCheck;
            object PriceCheck;
            object ProductStatusCheck;

            using (SqlConnection con = new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpressionPNumber, con);
                command.Parameters.Add(parameterAmount);
                command.Parameters.Add(parameterProcNumber);
                command.Parameters.Add(parameterStatus);
                command.Parameters.Add(parameterCurrency);
                ProductNumberCheck = command.ExecuteScalar();

                command.CommandText = sqlExpressionPrice;
                PriceCheck = command.ExecuteScalar();

                command.CommandText = sqlExpressionStatus;
                ProductStatusCheck = command.ExecuteScalar();

            }

            Assert.AreEqual(1, PriceCheck,  "PriceCheck");
            Assert.AreEqual(1, ProductStatusCheck,  "ProductStatusCheck");
            Assert.AreEqual(1, ProductNumberCheck,  "ProductNumberCheck");
        }

      
        public void AddCPU(
            Guid productGuid, string processorNumber = "9000ki", int numbersOfCores = 8, int numberOfThreads = 16)
        {

            SqlParameter parameterProductGuid =
                new SqlParameter("@productGuid", productGuid);
            SqlParameter parameterProcessorNumber =
                new SqlParameter("@processorNumber", processorNumber);
            SqlParameter parameterNumberOfCores =
                new SqlParameter("@numberOfCores", numbersOfCores);
            SqlParameter parameterNumberOfThreads =
                new SqlParameter("@numberOfThreads", numberOfThreads);
            SqlParameter[] sqlParameters =
                new SqlParameter[] {
                    parameterProcessorNumber,
                    parameterNumberOfCores,
                    parameterProductGuid,
                    parameterNumberOfThreads

                };

            string sqlExpressionAddProduct =
                "INSERT INTO Products (ProductGuid)" +
                "values (@productGuid)";
            string sqlExpressionAddCPU =
                "INSERT INTO CPUs (ProcessorNumber, ProductGuid, " +
                "NumberOfCores,NumberOfThreads," +
                " PBF, Cache, TDP, AverageBench, Manufacture)" +
                "values (@processorNumber, @productGuid, " +
                "@numberOfCores, @numberOfThreads," +
                "10, 10, 10, 100, 'Intel')";

            using (SqlConnection con = new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpressionAddProduct, con);
                command.Parameters.AddRange(sqlParameters);
                int AddCorrect = command.ExecuteNonQuery();
                if (AddCorrect == 0)
                {
                    throw new Exception("Invalid Sql Add Command");
                }
                command.CommandText = sqlExpressionAddCPU;
                AddCorrect = command.ExecuteNonQuery();
                if (AddCorrect == 0)
                {
                    throw new Exception("Invalid Sql Add Command");
                }
            }
        }

        //AddGPU
        public void AddGPU(
           Guid productGuid, 
           string name = "GTX2070Ti", 
           string developer = "Nvidia",
           string manufacture = "Asus",
           int memorySpeed = 20,
           int boostClock=20, 
           int averageBench=100,
           int frameBuffer = 100,
           string architecture= "Pascal"
           )
        {
            SqlParameter parameterProductGuid =
                new SqlParameter("@productGuid", productGuid);
            SqlParameter parameterName =
                new SqlParameter("@name", name);
            SqlParameter parameterDeveloper =
                new SqlParameter("@developer", developer);
            SqlParameter parameterManufacture =
                new SqlParameter("@manufacture", manufacture);
            SqlParameter parameterMemorySpeed =
                new SqlParameter("@memorySpeed", memorySpeed);
            SqlParameter parameterBoostClock =
                new SqlParameter("@boostClock", boostClock);
            SqlParameter parameterAverageBench =
                new SqlParameter("@averageBench", averageBench);
            SqlParameter parameterFrameBuffer =
                 new SqlParameter("@frameBuffer", frameBuffer);
            SqlParameter parameterArchitecture =
                new SqlParameter("@architecture", architecture);
            SqlParameter[] sqlParameters =
                new SqlParameter[] {
                    parameterName,
                    parameterProductGuid,
                    parameterAverageBench,
                    parameterBoostClock,
                    parameterDeveloper,
                    parameterManufacture,
                    parameterMemorySpeed,
                    parameterArchitecture,
                    parameterFrameBuffer
                };

            string sqlExpressionAddProduct =
                "INSERT INTO Products (ProductGuid)" +
                "values (@productGuid)";
            string sqlExpressionAddGPU =
                "INSERT INTO GPUs " +
                "(Name, ProductGuid, AverageBench, " +
                "BoostClock, Developer, Manufacture, " +
                "MemorySpeed, Architecture, FrameBuffer) " +
                "values (@name, @productGuid, @averageBench, " +
                "@boostClock, @developer, @manufacture, " +
                "@memorySpeed, @architecture, @frameBuffer)";

            using (SqlConnection con = 
                new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpressionAddProduct, con);
                command.Parameters.AddRange(sqlParameters);
                int AddCorrect = command.ExecuteNonQuery();
                if (AddCorrect == 0)
                {
                    throw new Exception("Invalid Sql Add Command");
                }
                command.CommandText = sqlExpressionAddGPU;
                AddCorrect = command.ExecuteNonQuery();
                if (AddCorrect == 0)
                {
                    throw new Exception("Invalid Sql Add Command");
                }
            }
        }

        [TestMethod]
        public void Delete_CPUEntityRemove_EntityDeleted()
        {
            //Arrange
            Guid productGuid = Guid.NewGuid();
            string processorNumber = "9000ki";
            int numbersOfCores = 8;
            int numberOfThreads = 16;
            string sqlExpressionSelectFromCPUs =
                "SELECT COUNT(*) FROM CPUs";

            AddCPU(productGuid, processorNumber, numbersOfCores, numberOfThreads);

            //Act
            var CPU = CPURepository.FindById(productGuid);
            CPURepository.Delete(CPU);
            context.SaveChanges();
            //Assert
            object CPUCheck;

            

            using (SqlConnection con = new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpressionSelectFromCPUs, con);
                CPUCheck = command.ExecuteScalar();

            }

            Assert.AreEqual(0, CPUCheck,  "CPU was not deleted");
        }


        [TestMethod]
        public void Delete_GPUEntityRemove_EntityDeleted()
        {
            //Arrange
            Guid productGuid = Guid.NewGuid();
            string name = "GTX2070Ti";
            string sqlExpressionSelectFromCPUs =
                "SELECT COUNT(*) FROM GPUs";

            AddGPU(productGuid, name);

            //Act
            var gpu = GPURepository.FindById(productGuid);
            GPURepository.Delete(gpu);
            context.SaveChanges();
            //Assert
            object GPUCheck;

            using (SqlConnection con = new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpressionSelectFromCPUs, con);
                GPUCheck = command.ExecuteScalar();
            }

            Assert.AreEqual(0, GPUCheck, "GPU was not deleted");
        }


        [TestMethod]
        public void Create_GPUEntityAdd_RightEntityReturned()
        {
            //Arrange
            string sqlExpressionName =
                "SELECT COUNT(*) FROM GPUs WHERE Name='GTX 2070Ti'";
            string sqlExpressionStatus =
                "SELECT COUNT(*) FROM Products WHERE Status='Good'";
            string sqlExpressionPrice =
                "SELECT COUNT(*) FROM Prices WHERE Amount=1000 AND Сurrency = 0";

            GPU gpu = new GPU()
            {
                Name = "GTX 2070Ti",
                Status = "Good",
                Price = new Price() { Amount = 1000, Сurrency = Currency.UAH }
            };
            //Act
            GPURepository.Create(gpu);
            context.SaveChanges();
            //Assert
            object NameCheck;
            object PriceCheck;
            object ProductStatusCheck;

            using (SqlConnection con = 
                new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpressionName, con);
                NameCheck = command.ExecuteScalar();

                command.CommandText = sqlExpressionPrice;
                PriceCheck = command.ExecuteScalar();

                command.CommandText = sqlExpressionStatus;
                ProductStatusCheck = command.ExecuteScalar();

            }

            Assert.AreEqual(1, PriceCheck,  "PriceCheck");
            Assert.AreEqual(1, ProductStatusCheck, "ProductStatusCheck");
            Assert.AreEqual(1, NameCheck, "NameCheck");
        }

        [TestMethod]
        public void Update_CPUEntityUpdate_RightEntityReturned()
        {
            //Arrange
            //Add new item

            Guid productGuid = Guid.NewGuid();
            string processorNumber = "9000ki";

            string processorNumberEdited = "7800hq";


            AddCPU(productGuid, processorNumber);

            //Act
            CPU cpu = CPURepository.FindById(productGuid);
            cpu.ProcessorNumber = processorNumberEdited;
            CPURepository.Update(cpu);
            context.SaveChanges();
            //Assert

            string sqlExpresionSelectCPU = "SELECT COUNT(*) FROM CPUs WHERE ProductGuid=@productGuid AND ProcessorNumber=@processorNumberEdited";
            SqlParameter parameterProductGuid =
                new SqlParameter("productGuid", productGuid);
            SqlParameter parameterProcessorNumber =
                new SqlParameter("processorNumberEdited", processorNumberEdited);
            SqlParameter[] sqlParameters =
                new SqlParameter[]{
                    parameterProductGuid,
                    parameterProcessorNumber };

            object CPUCheck;

            using (SqlConnection con =
                new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpresionSelectCPU, con);
                command.Parameters.AddRange(sqlParameters);
                CPUCheck = command.ExecuteScalar();

            }
            Assert.AreEqual(1, CPUCheck);
        }

        [TestMethod]
        public void Update_GPUEntityUpdate_RightEntityReturned()
        {
            //Arrange
            //Add new item
            Guid productGuid = Guid.NewGuid();
            string name = "GTX2070Ti";
            string nameEdited = "GTX2080";

            AddGPU(productGuid, name);

            //Act
            GPU gpu = GPURepository.FindById(productGuid);
            gpu.Name = nameEdited;
            GPURepository.Update(gpu);
            context.SaveChanges();

            //Assert
            string sqlExpresionSelectGPU = "SELECT COUNT(*) FROM GPUs WHERE ProductGuid=@productGuid AND Name=@nameEdited";
            SqlParameter parameterProductGuid =
                new SqlParameter("productGuid", productGuid);
            SqlParameter parameterName =
                new SqlParameter("name", name);
            SqlParameter parameterNameEdited =
                new SqlParameter("nameEdited", nameEdited);
            SqlParameter[] sqlParameters =
                new SqlParameter[]{
                    parameterProductGuid,
                    parameterName,
                    parameterNameEdited
                };

            object GPUCheck;

            using (SqlConnection con =
                new SqlConnection(TestDBInit.TestDatabaseConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlExpresionSelectGPU, con);
                command.Parameters.AddRange(sqlParameters);
                GPUCheck = command.ExecuteScalar();

            }
            Assert.AreEqual(1, GPUCheck);
        }

        [TestMethod]
        public void FindById_CPUEntityRead_RightEntityReturned()
        {
            //Arrange
            Guid productGuid = Guid.NewGuid();
            string processorNumber = "9000ki";

            AddCPU(productGuid, processorNumber);

            //Act
            CPU result = CPURepository.FindById(productGuid);

            //Assert
            Assert.AreEqual(productGuid, result.ProductGuid);
            Assert.AreEqual(processorNumber, result.ProcessorNumber);
        }

        [TestMethod]
        public void FindById_GPUEntityRead_RightEntityReturned()
        {
            //Arrange
            Guid productGuid = Guid.NewGuid();
            string name = "GTX2070Ti";

            AddGPU(productGuid, name);

            //Act
            GPU result = GPURepository.FindById(productGuid);

            //Assert
            Assert.AreEqual(productGuid, result.ProductGuid);
            Assert.AreEqual(name, result.Name);
        }

        [TestMethod]
        public void Get_CPUsEntitesRead_RightEntitesReturned()
        {
            //Arrange
            Guid productGuid1 = Guid.NewGuid();
            string processorNumber1 = "Proc1";

            Guid productGuid2 = Guid.NewGuid();
            string processorNumber2 = "Proc2";

            Guid productGuid3 = Guid.NewGuid();
            string processorNumber3 = "Proc3";

            AddCPU(productGuid1, processorNumber1);
            AddCPU(productGuid2, processorNumber2);
            AddCPU(productGuid3, processorNumber3);

            //Act
            List<CPU> result = CPURepository.Get().ToList();

            //Assert
            Assert.IsNotNull(result.Find(x => x.ProductGuid == productGuid1));
            Assert.IsNotNull(result.Find(x => x.ProductGuid == productGuid2));
            Assert.IsNotNull(result.Find(x => x.ProductGuid == productGuid3));

            Assert.IsNotNull(result.Find(x => x.ProcessorNumber == processorNumber1));
            Assert.IsNotNull(result.Find(x => x.ProcessorNumber == processorNumber2));
            Assert.IsNotNull(result.Find(x => x.ProcessorNumber == processorNumber3));
        }

        [TestMethod]
        public void Get_GPUsEntitesRead_RightEntitesReturned()
        {
            //Arrange
            Guid productGuid1 = Guid.NewGuid();
            string name1 = "Videocard1";

            Guid productGuid2 = Guid.NewGuid();
            string name2 = "Videocard2";

            Guid productGuid3 = Guid.NewGuid();
            string name3 = "Videocard3";

            AddGPU(productGuid1, name1);
            AddGPU(productGuid2, name2);
            AddGPU(productGuid3, name3);

            //Act
            List<GPU> result = GPURepository.Get().ToList();

            //Assert
            Assert.IsNotNull(result.Find(x => x.ProductGuid == productGuid1));
            Assert.IsNotNull(result.Find(x => x.ProductGuid == productGuid2));
            Assert.IsNotNull(result.Find(x => x.ProductGuid == productGuid3));

            Assert.IsNotNull(result.Find(x => x.Name == name1));
            Assert.IsNotNull(result.Find(x => x.Name == name2));
            Assert.IsNotNull(result.Find(x => x.Name == name3));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            TestDBInit.AssemblyCleanup();
        }
    }
}
