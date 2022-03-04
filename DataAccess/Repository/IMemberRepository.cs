using BusinessObject;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        void InsertMember(MemberObject member);
        void DeleteMember(int memberId);
        void UpdateMember(MemberObject member);
        MemberObject Login(string email, string password);

    }
}
