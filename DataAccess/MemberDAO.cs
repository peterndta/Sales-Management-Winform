using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
namespace DataAccess
{
    public class MemberDAO : BaseDAL
    {
        //---------------------------------------------------
        //Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instnaceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instnaceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------------
        public List<MemberObject> GetMemberList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberId, Email, CompanyName, City, Country, Password from Member";
            var members = new List<MemberObject>();
            try
            {
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    members.Add(new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return members;
        }

        //-------------------------------------------------
        public MemberObject GetMemberByID(int memberID)
        {
            MemberObject member = null;
            IDataReader dataReader = null;
            string SQLSelect = "Select MemberId, Email, CompanyName, City, Country, Password from Member where MemberId = @MemberId";
            try
            {
                var param = DataProvider.CreateParameter("@MemberId", 4, memberID, DbType.Int32);
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    member = new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5),
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }
        //-------------------------------------------------
        //Add new a member
        public void AddNew(MemberObject member)
        {
            try
            {
                MemberObject isEmpty = GetMemberByID(member.MemberID);
                if (isEmpty == null)
                {
                    string SQLInsert = "Insert Member values(@MemberId, @Email, @CompanyName, @City, @Country, @Password)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@MemberId", 4, member.MemberID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member is already exist");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        //-------------------------------------------------
        //Update a member
        public void Update(MemberObject member)
        {
            try
            {
                MemberObject isEmpty = GetMemberByID(member.MemberID);
                if (isEmpty != null)
                {
                    string SQLInsert = "Update Member set MemberId = @MemberId, Email = @Email, CompanyName = @CompanyName, City = @City, Country = @Country, Password = @Password" +
                        " where MemberId = @MemberId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@MemberId", 4, member.MemberID, DbType.Int32));
                    parameters.Add(DataProvider.CreateParameter("@Email", 100, member.Email, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@CompanyName", 40, member.CompanyName, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@City", 15, member.City, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@Country", 15, member.Country, DbType.String));
                    parameters.Add(DataProvider.CreateParameter("@Password", 30, member.Password, DbType.String));
                    DataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member is already exist.");
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        //--------------------------------------------------
        //Remove a member
        public void Remove(int memberID)
        {
            try
            {
                MemberObject isEmpty = GetMemberByID(memberID);
                if (isEmpty != null)
                {
                    string SQLDelete = "Delete Member where MemberId = @MemberId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(DataProvider.CreateParameter("@MemberId", 4, memberID, DbType.Int32));
                    DataProvider.Update(SQLDelete, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The member is already exist.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }

        }//end Remove

        //---------------------------------------------------
        //Login
        public MemberObject Login(string email, string password)
        {
            MemberObject member = null;
            IDataReader dataReader = null;
            try
            {
                string SQLSelect = "Select MemberId, Email, CompanyName, City, Country, Password from Member where Email = @Email AND Password=@Password";
                var parameters = new List<SqlParameter>();
                parameters.Add(DataProvider.CreateParameter("@Email", 100, email, DbType.String));
                parameters.Add(DataProvider.CreateParameter("@Password", 30, password, DbType.String));
                dataReader = DataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, parameters.ToArray());

                if (dataReader.Read())
                {
                    member = new MemberObject
                    {
                        MemberID = dataReader.GetInt32(0),
                        Email = dataReader.GetString(1),
                        CompanyName = dataReader.GetString(2),
                        City = dataReader.GetString(3),
                        Country = dataReader.GetString(4),
                        Password = dataReader.GetString(5),
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return member;
        }

        //--------------------------END----------------------
    }
}

