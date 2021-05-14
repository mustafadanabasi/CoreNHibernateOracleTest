using FluentNHibernate.Mapping;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreNHibernateOracleTest
{
    /*
    CREATE TABLE USER_TEST_TABLE (USER_CODE VARCHAR2(50), USER_DESC VARCHAR2(100), USER_LANG VARCHAR2(5))

    CREATE UNIQUE INDEX PK_USER_CODE ON USER_TEST_TABLE (USER_CODE);

    ALTER TABLE USER_TEST_TABLE ADD CONSTRAINT PK_USER_CODE PRIMARY KEY (USER_CODE);

     */

    public class USER_TEST_TABLE
    {
        public virtual string USER_CODE { get; set; }

        public virtual string USER_DESC { get; set; }

        public virtual string USER_LANG { get; set; }

    }

    public sealed class USER_TEST_TABLE_MAP : ClassMap<USER_TEST_TABLE>
    {
        public USER_TEST_TABLE_MAP()
        {
            Id(u => u.USER_CODE);
            Map(u => u.USER_DESC);
            Map(u => u.USER_LANG);
        }
    }


}
