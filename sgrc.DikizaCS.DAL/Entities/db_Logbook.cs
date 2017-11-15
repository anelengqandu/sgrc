using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sgrc.DikizaCS.DAL.Utils;

namespace sgrc.DikizaCS.DAL
{
    public partial class db_LogBook
    {
        public async Task<DBResult> _insert()
        {
            try
            {
                DataAccess.metadata.db_LogBook.Add(this);
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

    public partial class db_LogBook
    {
        public static db_LogBook Get(long id)
        {
            try
            {
                var q = DataAccess.metadata.db_LogBook.Where(row => row.Id == id);

                return q.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
