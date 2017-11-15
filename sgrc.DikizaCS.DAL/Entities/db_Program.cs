using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL
{
    public partial class db_Program
    {
        public async Task<DBResult> _insert()
        {
            try
            {
                DataAccess.metadata.db_Program.Add(this);
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

                DataAccess.metadata.db_Program.Remove(this);

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

    public partial class db_Program
    {
        public static db_Program Get(long id)
        {
            try
            {
                 var q = DataAccess.metadata.db_Program.Where(row => row.Id == id);
        
                return q.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
