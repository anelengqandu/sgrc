using System;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;

namespace sgrc.DikizaCS.DAL.Utils
{
    public static class DBExceptionHandler
    {
        public static DBResult check(Exception e)
        {
            if (e.GetType() == typeof(EntityCommandExecutionException))
                return new DBResult("EntityCommandExecutionException: An Error Occurred While Trying To Connect to the Database.", "Error", null);

            if (e.GetType() == typeof(InvalidOperationException))
                return new DBResult("InvalidOperationException: An Error Occurred Trying To Perform An Operation", "Error", null);

            if (e.GetType() == typeof(OptimisticConcurrencyException))
                return new DBResult("OptimisticConcurrencyException: An Error Occurred Trying To Perform An Operation", "Error", null);

            if (e.GetType() == typeof(UpdateException))
                return new DBResult("UpdateException: An Error Occurred Trying To Perform An Operation", "Error", null);

            if (e.GetType() == typeof(DbEntityValidationException))
                return new DBResult("UpdateException: An Error Occurred Trying To Perform An Operation", "Error", null);

            return new DBResult($"{e.GetType().Name}:An Error Occurred Trying To Perform An Operation", "Error", null);
        }
    }
}

