using MapTest.Data;
using MapTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapTest.BizLogic
{
    /// <summary>
    /// Интерфейс бизнес-логики для получения статистики по регионам
    /// </summary>
    public interface IStatsRepository
    {
        /// <summary>
        /// Получение списка федеральных округов и их субъектов федерации
        /// </summary>
        /// <returns></returns>
        IEnumerable<FederalDistrict> FederalDistricts();

        /// <summary>
        /// Получение статистики из 1 поля БД по коду региона
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FederalSubjectHeaderStat GetFederalSubjectMainStats(string id);
    }

    /// <summary>
    /// Бизнес-логика для получения статистики по регионам
    /// </summary>
    class StatsRepository : IStatsRepository
    {
        private readonly DbContextOptionsBuilder<EFCoreTestContext>
            optionsBuilder = new DbContextOptionsBuilder<EFCoreTestContext>();
        private readonly IConfigurationRoot configurationRoot;

        public StatsRepository()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }

        public IEnumerable<FederalDistrict> FederalDistricts()
        {
            var conn = configurationRoot.GetConnectionString("EFCoreTestContext");
            optionsBuilder.UseSqlServer(conn);

            using var ctx = new EFCoreTestContext(optionsBuilder.Options);
            return ctx.FederalDistricts.Include(x => x.FederalSubjects).ToList();
        }

        public FederalSubjectHeaderStat GetFederalSubjectMainStats(string id)
        {
            var r = new FederalSubjectHeaderStat();

            try
            {
                var conn = configurationRoot.GetConnectionString("EFCoreTestContext");
                optionsBuilder.UseSqlServer(conn);

                //!!! Я бы хранил JSON или XML структуру в 1 поле БД со всей спецификой

                // Все получаем из БД по аналогии с ederalDistricts() 
                Random rnd = new Random();

                r.ObjectsDone = rnd.Next(1, 400);
                r.ObjectTotal = rnd.Next(1, 500);
                r.ObjectsInProcess = rnd.Next(1, 300);
                r.ProjectsTotal = rnd.Next(1, 200);
                r.FederalSubjectName = "Наименование субъекта РФ"; //  полученное из БД
                r.BudjetStatItem.Value1 = rnd.Next(1, 20);
                r.BudjetStatItem.Value2 = rnd.Next(1, 20);
                r.BudjetStatItem.Value3 = rnd.Next(1, 20);

                r.PlansStatItem.Value1 = rnd.Next(1, 30);
                r.PlansStatItem.Value2 = rnd.Next(1, 30);
                r.PlansStatItem.Value3 = rnd.Next(1, 30);
                r.PlansStatItem.Value4 = rnd.Next(1, 30);

                r.VariablesStatItem.Value1 = rnd.Next(1, 20);
                r.VariablesStatItem.Value2 = rnd.Next(1, 20);
                r.VariablesStatItem.Value3 = rnd.Next(1, 20);
                r.VariablesStatItem.Value4 = rnd.Next(1, 20);

                r.ControlPointStatItem.Value1 = rnd.Next(1, 23);
                r.ControlPointStatItem.Value2 = rnd.Next(1, 23);
                r.ControlPointStatItem.Value3 = rnd.Next(1, 23);
                r.ControlPointStatItem.Value4 = rnd.Next(1, 23);
            }
            catch
            {
                //TODO Log it
            }

            return r;
        }
    }
}
