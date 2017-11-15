using System;
using System.Linq;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL
{
  public  partial class db_User
    {
        public async Task<DBResult> _insert()
        {
            try
            {
                DataAccess.metadata.db_User.Add(this);
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

                DataAccess.metadata.db_User.Remove(this);

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

    public partial class db_User
    {
        public static db_User Get(long id)
        {
            try
            {
                var q = DataAccess.metadata.db_User.Where(row => row.Id == id);
                return q.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static db_User GetByEmail(string email)
        {
            try
            {
                var q = DataAccess.metadata.db_User.Where(row => row.Email == email);
                return q.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
