using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreNHibernateOracleTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly NHibernate.ISession _session;

        public UserController(NHibernate.ISession session)
        {
            _session = session;
        }

        [HttpGet]
        public IList<USER_TEST_TABLE> Get()
        {
            IList<USER_TEST_TABLE> userList = null;

            try
            {
                userList = _session.Query<USER_TEST_TABLE>().ToList().OrderBy(x => x.USER_CODE).ToList();
                //userList = _session.Query<USER_TEST_TABLE>().ToList().Where(x => x.USER_CODE == "TEST1").ToList();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return userList;
        }


        [HttpPost]
        public bool Add(USER_TEST_TABLE userModel)
        {
            bool result = false;

            try
            {
                USER_TEST_TABLE currentUser = GetUser(userModel.USER_CODE);

                if (currentUser == null)
                {
                    _session.Save(userModel);
                    _session.Flush();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return result;
        }

        [HttpPut]
        public bool Update(USER_TEST_TABLE userModel)
        {
            bool result = false;

            try
            {
                USER_TEST_TABLE currentUser = GetUser(userModel.USER_CODE);

                if (currentUser != null)
                {
                    currentUser.USER_DESC = userModel.USER_DESC;
                    currentUser.USER_LANG = userModel.USER_LANG;
                    _session.Update(currentUser);
                    _session.Flush();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return result;
        }


        [HttpDelete]
        public bool Detele(USER_TEST_TABLE userModel)
        {
            bool result = false;

            try
            {
                USER_TEST_TABLE currentUser = GetUser(userModel.USER_CODE);

                USER_TEST_TABLE deletetUser = currentUser;

                if (deletetUser != null)
                {
                    _session.Delete(deletetUser);
                    _session.Flush();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return result;
        }


        USER_TEST_TABLE GetUser(string userCode)
        {
            USER_TEST_TABLE currentUser = null;
            IList<USER_TEST_TABLE> userList = _session.Query<USER_TEST_TABLE>().ToList().Where(x => x.USER_CODE == userCode).ToList();

            if (userList != null && userList.Count > 0)
            {
                currentUser = userList.FirstOrDefault();
            }

            return currentUser;
        }
    }
}
