using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web;

namespace sgrc.DikizaCS.DAL
{

    public static class DataAccess
    {
        public static bool runInDesktopMode = false;

        [ThreadStaticAttribute]
        public static DikizaCSEntities desktopModeMetadata = null;

        private const string DataContextKey = "DataContextKey";

        public static void EF_Fix()
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        private static DikizaCSEntities InternalDataContext
        {
            get
            {
                return (DikizaCSEntities)HttpContext.Current.Items[DataContextKey];
            }

            set
            {
                HttpContext.Current.Items[DataContextKey] = value;
            }
        }

        internal static DikizaCSEntities metadata
        {
            get
            {
                if (runInDesktopMode)
                {
                    // return from Thread Context
                    if (desktopModeMetadata == null)
                    {
                        desktopModeMetadata = new DikizaCSEntities();
                        desktopModeMetadata.Configuration.LazyLoadingEnabled = false;
                    }
                    return desktopModeMetadata;
                }


                // If the context is missing, create a new one
                if (InternalDataContext == null)
                {
                    // Note: in a real app, this should get the connection string from secure storage and pass it to the context constructor.
                    //string connectionString = @"Data Source=(local)\sqlexpress;Initial Catalog=CaveatEmptor;Integrated Security=True;Pooling=False";
                    //InternalDataContext = new DataPropEntities(connectionString);
                    InternalDataContext = new DikizaCSEntities();
                    InternalDataContext.Configuration.LazyLoadingEnabled = false;
                }

                return InternalDataContext;
            }
        }

        internal static DikizaCSEntities getMetadata()
        {
            return metadata;
        }

        public static DikizaCSEntities getDesktopMetadata()
        {
            return new DikizaCSEntities();
        }

        public static void SaveChanges()
        {
            try
            {
                metadata.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw dbEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CleanUp()
        {
            if (InternalDataContext != null)
            {
                InternalDataContext.Dispose();
                InternalDataContext = null;
            }
        }

        public static void CleanUpDesktopMode()
        {
            if (desktopModeMetadata != null)
            {
                desktopModeMetadata.Dispose();
                desktopModeMetadata = null;
            }
        }
    }
}