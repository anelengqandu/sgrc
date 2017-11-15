using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL
{
    public partial class db_Client
    {
        public async Task<DBResult> _insert()
        {
            try
            {
                DataAccess.metadata.db_Client.Add(this);
                return new DBResult(true);
            }
            catch (Exception e)
            {
                return DBExceptionHandler.check(e);
            }
        }
        public DBResult _remove()
        {
            try
            {

                DataAccess.metadata.db_Client.Remove(this);

                return new DBResult(true);
            }
            catch (Exception e)
            {
                return DBExceptionHandler.check(e);
            }
        }
        public async Task<DBResult> _update()
        {
            try
            {

                return new DBResult(true);
            }
            catch (Exception e)
            {
                return DBExceptionHandler.check(e);
            }
        }
    }

    public partial class db_Client
    {
        public static db_Client Get(long id)
        {
            try
            {
                var q = DataAccess.metadata.db_Client.Where(row => row.Id == id);

                return q.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static db_Client GetByEmail(string email)
        {
            try
            {


                var q = from row in DataAccess.metadata.db_Client
                        where row.ContactEmail==email
                        select row;

             

                return q.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static db_Client GetBySecurityStampId(string securityStampId)
        {
            try
            {
                var q = DataAccess.metadata.db_Client.Where(row => row.SecurityStampId == securityStampId);

                return q.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
