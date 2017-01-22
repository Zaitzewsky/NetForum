using Data.Context;
using Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            var initializer = new MigrateDatabaseToLatestVersion<ForumContext, Configuration>();
            Database.SetInitializer(initializer);

            var context = new ForumContext();
            context.Database.Initialize(true);
        }
    }
}
