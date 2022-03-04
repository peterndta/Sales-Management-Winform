using BusinessObject;
using System.Collections.Generic;


namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        
        IEnumerable<MemberObject> IMemberRepository.GetMembers() => MemberDAO.Instance.GetMemberList();

        void IMemberRepository.InsertMember(MemberObject member)
        {
            MemberDAO.Instance.AddNew(member);
        }

        void IMemberRepository.DeleteMember(int memberId)
        {
            MemberDAO.Instance.Remove(memberId);
        }

        void IMemberRepository.UpdateMember(MemberObject member)
        {
            MemberDAO.Instance.Update(member);
        }

        MemberObject IMemberRepository.Login(string email, string password) => MemberDAO.Instance.Login(email, password);
       
    }
}
